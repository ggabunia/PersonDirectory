using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonDirectory.DataAccess;
using PersonDirectory.Filters;

namespace PersonDirectory.Controllers
{
    public class ConnectionsController : ExceptionHandlerController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [ModelStateFilter]
        private List<string> ValidateConnection(PersonConnection connection)
        {
            List<string> errors = new List<string>();
            if (connection.OriginatorID == connection.TargetID)
            {
                errors.Add("საკუთარ თავთან კავშირი დაუშვებელია");
            }
            var existingConnection = unitOfWork.ConnectionRepository.Get(filter:
                    x => (
                    (x.OriginatorID == connection.OriginatorID && x.TargetID == connection.TargetID) ||
                    (x.OriginatorID == connection.TargetID && x.TargetID == connection.OriginatorID))
                    ).FirstOrDefault();
            if (existingConnection != null)
            {
                errors.Add("ამ ორ პიროვნებას შორის კავშირი უკვე არსებობს");
            }
            return errors;
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,OriginatorID,TargetID,TypeID")] PersonConnection connection)
        {
            var errors = ValidateConnection(connection);
            if (ModelState.IsValid && !errors.Any())
            {
                unitOfWork.ConnectionRepository.Insert(connection);

                PersonConnection reverseConnection = new PersonConnection()
                {
                    OriginatorID = connection.TargetID,
                    TargetID = connection.OriginatorID,
                    TypeID = connection.TypeID
                };
                unitOfWork.ConnectionRepository.Insert(reverseConnection);


                unitOfWork.Save();
                return Content("success");
            }
            if (!errors.Any())
            {
                errors.Add("გთხოვთ შეავსოთ ყველა ველი");
            }
            var result = "<ul>";
            foreach (var item in errors)
            {
                result += String.Format("<li>{0}</li>", item);
            }
            result += "</ul>";

            return Content(result);

        }
        public ActionResult Delete(int id)
        {
            PersonConnection connection = unitOfWork.ConnectionRepository.GetByID(id);
            PersonConnection reverseConnection = unitOfWork.ConnectionRepository.Get(filter: c => (c.OriginatorID == connection.TargetID && c.TargetID == connection.OriginatorID)).FirstOrDefault();
            int originatorID = connection.OriginatorID;
            unitOfWork.ConnectionRepository.Delete(id);
            if (reverseConnection != null)
            {
                unitOfWork.ConnectionRepository.Delete(reverseConnection.ID);
            }
            unitOfWork.Save();
            return RedirectToAction("Edit", "Home", new { id = originatorID });
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
