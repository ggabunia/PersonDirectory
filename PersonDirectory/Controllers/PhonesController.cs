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
    [ModelStateFilter]
    public class PhonesController : ExceptionHandlerController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private List<string> ValidatePhone(Phone phone)
        {
            List<string> errors = new List<string>();
            if (String.IsNullOrWhiteSpace(phone.Number))
            {
                errors.Add("გთხოვთ ჩაწეროთ ტელეფონის ნომერი");

            }
            else if (phone.Number.Length < 4 || phone.Number.Length > 50)
            {
                errors.Add("ტელეფონის ნომერი უნდა იყოს 4-დან 50 სიმბოლომდე");
            }
            if (unitOfWork.PhoneTypeRepository.GetByID(phone.TypeID) == null)
            {
                errors.Add("გთხოვთ აირჩიოთ ტელეფონის ნომრის ტიპი");
            }

            return errors;

        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,TypeID,PersonID,Number")] Phone phone)
        {
            var errors = ValidatePhone(phone);
            if (ModelState.IsValid && !errors.Any())
            {
                unitOfWork.PhoneRepository.Insert(phone);
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
            Phone phone = unitOfWork.PhoneRepository.GetByID(id);
            int personID = phone.PersonID;
            unitOfWork.PhoneRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Edit", "Home", new { id = personID });
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
