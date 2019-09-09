using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PersonDirectory.DataAccess;
using PersonDirectory.HelperClasses;
using PagedList;
using PersonDirectory.Filters;

namespace PersonDirectory.Controllers
{
    [ModelStateFilter]
    public class HomeController : ExceptionHandlerController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
       
        private static void ValidatePerson(ModelStateDictionary ModelState, Person person)
        {
            if (String.IsNullOrWhiteSpace(person.PersonalNr))
            {
                ModelState.AddModelError("PersonalNr", "პირადი ნომერი სავალდებულოა");
            }
            else if (person.PersonalNr.Length != 11 || !person.PersonalNr.All(char.IsDigit))
            {
                ModelState.AddModelError("PersonalNr", "პირადი ნომერი უნდა შედგებოდეს 11 ციფრისგან");
            }
            if (String.IsNullOrWhiteSpace(person.FirstName))
            {
                ModelState.AddModelError("FirstName", "სახელი სავალდებულოა");
            }
            else
            {
                if (person.FirstName.Length < 2 || person.FirstName.Length > 50)
                {
                    ModelState.AddModelError("FirstName", "სახელი უნდა შედგებოდეს 2-დან 50-მდე სიმბოლოსაგან");
                }
                if (!Regex.IsMatch(person.FirstName, @"^[a-zA-Z]+$") && !Regex.IsMatch(person.FirstName, @"^[ა-ჰ]+$"))
                {
                    ModelState.AddModelError("FirstName", "სახელი უნდა შედგებოდეს მხოლოდ ქართული ან მხოლოდ ლათინური სიმბოლოებისაგან");
                }

            }
            if (String.IsNullOrWhiteSpace(person.LastName))
            {
                ModelState.AddModelError("LastName", "გვარი სავალდებულოა");
            }
            else
            {
                if (person.LastName.Length < 2 || person.LastName.Length > 50)
                {
                    ModelState.AddModelError("LastName", "გვარი უნდა შედგებოდეს 2-დან 50-მდე სიმბოლოსაგან");
                }
                if (!Regex.IsMatch(person.LastName, @"^[a-zA-Z]+$") && !Regex.IsMatch(person.LastName, @"^[ა-ჰ]+$"))
                {
                    ModelState.AddModelError("LastName", "გვარი უნდა შედგებოდეს მხოლოდ ქართული ან მხოლოდ ლათინური სიმბოლოებისაგან");
                }
            }
            if(person.BirthDate == null)
            {
                ModelState.AddModelError("BirthDate", "დაბადების თარიღი სავალდებულოა");
            }
            else if( person.BirthDate.AddYears(18) > DateTime.Now)
            {
                ModelState.AddModelError("BirthDate", "პირი უნდა იყოს მინიმუმ 18 წლის");
            }
            if (person.CityID == 0)
            {
                ModelState.AddModelError("CityID", "გთხოვთ აირჩიოთ ქალაქი");
            }
            if (person.GenderID == 0)
            {
                ModelState.AddModelError("GenderID", "გთხოვთ აირჩიოთ სქესი");
            }


        }


        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (searchString == null)
            {
                searchString = "";
            }
            ViewBag.CurrentFilter = searchString;
            
            var persons = unitOfWork.PersonRepository.Get(
                includeProperties: "City,Gender", 
                filter: p=>(p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.PersonalNr.Contains(searchString))
                );

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(persons.ToPagedList(pageNumber, pageSize));
            
        }

        public ActionResult Search(int? page, DateTime? birthDate, int? genderID, int? cityID, string phoneNr="", string firstName = "", string lastName = "", string personalNr = "")
        {
            var persons = unitOfWork.PersonRepository.Get(
                includeProperties: "City,Gender, Phones", 
                filter: p=>(p.FirstName.Contains(firstName) && p.LastName.Contains(lastName) &&
                p.PersonalNr.Contains(personalNr))
            );
            ViewBag.BirthDate = birthDate;
            ViewBag.GenderID = genderID;
            ViewBag.CityID = cityID;
            ViewBag.PhoneNr = phoneNr;
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            ViewBag.PersonalNr = personalNr;
            ViewBag.Cities = unitOfWork.CityRepository.Get();
            ViewBag.Genders = unitOfWork.GenderRepository.Get();
            if (genderID.HasValue)
            {
                persons = persons.Where(p => p.GenderID == genderID);
            }
            if (cityID.HasValue)
            {
                persons = persons.Where(p => p.CityID == cityID);
            }
            if (birthDate.HasValue)
            {
                persons = persons.Where(p => p.BirthDate.Date == birthDate.Value.Date);
            }
            if (!String.IsNullOrWhiteSpace(phoneNr))
            {
                var phonePersonIDs = unitOfWork.PhoneRepository.Get(filter: p => p.Number.Contains(phoneNr)).Select(p => p.PersonID);
                persons = persons.Where(p => phonePersonIDs.Contains(p.ID));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(persons.ToPagedList(pageNumber, pageSize));
       
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = unitOfWork.PersonRepository.GetByID(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(unitOfWork.CityRepository.Get(), "ID", "Name");
            ViewBag.GenderID = new SelectList(unitOfWork.GenderRepository.Get(), "ID", "GenderName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,GenderID,PersonalNr,BirthDate,CityID")] Person person, HttpPostedFileBase image)
        {

            ValidatePerson(ModelState, person);

            if (ModelState.IsValid)
            {
                person.Photo = FileHandler.SaveFile(image);
                unitOfWork.PersonRepository.Insert(person);
                unitOfWork.Save();
                return RedirectToAction("Details", new { id = person.ID });
            }

            ViewBag.CityID = new SelectList(unitOfWork.CityRepository.Get(), "ID", "Name");
            ViewBag.GenderID = new SelectList(unitOfWork.GenderRepository.Get(), "ID", "GenderName");
            return View(person);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = unitOfWork.PersonRepository.GetByID(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(unitOfWork.CityRepository.Get(), "ID", "Name");
            ViewBag.GenderID = new SelectList(unitOfWork.GenderRepository.Get(), "ID", "GenderName");
            ViewBag.PhoneTypes = unitOfWork.PhoneTypeRepository.Get();
            ViewBag.ConnectionTypes = unitOfWork.ConnectionTypeRepository.Get();
            List<int> connectedIDs = person.PersonConnections.Select(c => c.TargetID).ToList();
            ViewBag.People = unitOfWork.PersonRepository.Get(filter: p => (p.ID != person.ID && !connectedIDs.Contains(p.ID)));
            return View(person);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,GenderID,PersonalNr,BirthDate,CityID,Photo")] Person person, HttpPostedFileBase image)
        {
            ValidatePerson(ModelState, person);
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    person.Photo = FileHandler.SaveFile(image);
                }
                unitOfWork.PersonRepository.Update(person);
                unitOfWork.Save();
                return RedirectToAction("Details", new { id=person.ID});
            }
            ViewBag.CityID = new SelectList(unitOfWork.CityRepository.Get(), "ID", "Name");
            ViewBag.GenderID = new SelectList(unitOfWork.GenderRepository.Get(), "ID", "GenderName");
            return View(person);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = unitOfWork.PersonRepository.GetByID(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
    
            unitOfWork.PersonRepository.Delete(id);
            var personConnections = unitOfWork.ConnectionRepository.Get(filter: c => c.OriginatorID == id || c.TargetID == id);
            foreach(var connection in personConnections)
            {
                unitOfWork.ConnectionRepository.Delete(connection.ID);
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
        {
            var persons = unitOfWork.PersonRepository.Get(includeProperties: "PersonConnections");
            ViewBag.ConnectionTypes = unitOfWork.ConnectionTypeRepository.Get().ToList();
            return View(persons.ToList());
        }
        public ActionResult DownloadReport()
        {
            var persons = unitOfWork.PersonRepository.Get(includeProperties: "PersonConnections");
            var connectionTypes = unitOfWork.ConnectionTypeRepository.Get().ToList();

            string csv = "ID,სახელი,გვარი, პირადი ნომერი,";

            for(int i=0; i<connectionTypes.Count-1; i++)
            {
                csv += connectionTypes[i].Type + ",";
            }
            csv += connectionTypes.Last().Type + "\n";

            foreach(var person in persons)
            {
                csv += String.Format("{0},{1},{2},{3},", person.ID, person.FirstName, person.LastName, person.PersonalNr);
                for (int i = 0; i < connectionTypes.Count - 1; i++)
                {
                    csv += person.PersonConnections.Where(c => c.TypeID == connectionTypes[i].ID).ToList().Count + ",";
                }
              
                csv += person.PersonConnections.Where(c => c.TypeID == connectionTypes.Last().ID).ToList().Count + "\n";
            }         

            return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", String.Format("report_{0}.csv", DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")));
        }


        public ActionResult Error()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
