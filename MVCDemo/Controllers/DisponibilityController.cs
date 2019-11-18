using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class DisponibilityController : Controller
    {
        // GET: Disponibility
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListDisponibility()
        {
            List<DisponibilityModel> disponibility = new List<DisponibilityModel>();

            // Adds an example for the data
            // disponibility.Add(new DisponibilityModel { OEEDisponibilityID = "1000", MachineID = "1", OEEDisponibilityStatus = "1", FailDictionaryCode = "null", OEEDisponibilityTimeStamp = "Today" });
            disponibility = DisponibilityModel.GetDisponibilityModels();

            return View(disponibility);
        }

        public ActionResult BarDisponibilityFail()
        {
            string[] barChartFailData = DisponibilityModel.GetBarChartFailData();

            ViewBag.FailNameList = barChartFailData[0].Trim();
            ViewBag.FailCountList = barChartFailData[1].Trim();

            return View();
        }
    }
}