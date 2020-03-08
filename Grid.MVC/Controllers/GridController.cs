using Grid.MVC.Models;
using Grid.MVC.Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grid.MVC.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        MyDatabaseContext db = new MyDatabaseContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonalList()
        {
            List<Personal> personals = db.Personals.ToList();
            List<Address> addresses = db.Addresses.ToList();
            List<Country> countries = db.Countries.ToList();
            foreach (var address in addresses)
            {
                address.Country = countries.Find(i => i.Id == address.Country.Id);
            }
            for (int i = 0; i < personals.Count; i++)
            {
                personals[i].Address = addresses[i];
            }
            return View(personals);
        }

        public ActionResult AddPersonal()
        {
            List<SelectListItem> countries =
                (from i in db.Countries.ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();
            ViewBag.countries = countries;
            return View();
        }

        [HttpPost]
        public ActionResult AddPersonal(PersonalWithAddressAndCountry personal)
        {
            Country country = db.Countries.FirstOrDefault(i => i.Id == personal.Country.Id);
            Address address = new Address()
            {
                Name = personal.Address,
                Country = country
            };
            Personal person = new Personal()
            {
                Name = personal.Name,
                Surname = personal.Surname,
                Age = personal.Age,
                Address = address
            };

            db.Personals.Add(person);
            db.SaveChanges();
            return RedirectToAction("PersonalList");
        }

        public ActionResult EditPersonal(int id)
        {
            Personal person = db.Personals.FirstOrDefault(i => i.Id == id);
            List<Address> addresses = db.Addresses.ToList();
            Address address = addresses.Find(i => i.Id == person.Address.Id);
            List<Country> tempcountries = db.Countries.ToList();
            PersonalWithAddressAndCountry personal = new PersonalWithAddressAndCountry()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Age = person.Age,
                Address = address.Name,
                Country = tempcountries.Find(i => i.Id == address.Country.Id)
            };
            List<SelectListItem> countries =
                (from i in db.Countries.ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();
            ViewBag.countries = countries;
            return View(personal);
        }
        [HttpPost]
        public ActionResult EditPersonal(PersonalWithAddressAndCountry personal)
        {
            Personal person = db.Personals.FirstOrDefault(i => i.Id == personal.Id);
            List<Address> addresses = db.Addresses.ToList();
            Address address = addresses.Find(i => i.Id == person.Address.Id);
            Country country = db.Countries.FirstOrDefault(i => i.Id == personal.Country.Id);
            person.Name = personal.Name;
            person.Surname = personal.Surname;
            person.Age = personal.Age;
            address.Name = personal.Address;
            address.Country = country;
            person.Address = address;

            db.SaveChanges();

            return RedirectToAction("PersonalList");
        }

        public ActionResult DeletePersonal(int? id)
        {
            if (id != null)
            {
                Personal personal = db.Personals.FirstOrDefault(i => i.Id == id);
                if (personal != null)
                {
                    db.Personals.Remove(personal);
                    int result = db.SaveChanges();
                    if(result > 0)
                    {

                    }
                    else
                    {

                    }
                }
            }

            return RedirectToAction("PersonalList");
        }
    }
}