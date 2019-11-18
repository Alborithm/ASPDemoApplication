using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            PersonModel p = new PersonModel();

            return View();
        }

        public ActionResult ListPeople()
        {
            List<PersonModel> people = new List<PersonModel>();

            people.Add(new PersonModel { FirstName = "Gerardo", LastName = "Albor", Age = 28, IsAlive = true });
            people.Add(new PersonModel { FirstName = "Martha", LastName = "Hernandez", Age = 28, IsAlive = true });
            people.Add(new PersonModel { FirstName = "Marcela", LastName = "Albor", Age = 26, IsAlive = true });

            return View(people);
        }
    }
}