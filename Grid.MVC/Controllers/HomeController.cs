using Grid.MVC.Models;
using Grid.MVC.Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grid.MVC.Controllers
{
    public class HomeController : Controller
    {
        MyDatabaseContext db = new MyDatabaseContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ListPersonals()
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
    }
}