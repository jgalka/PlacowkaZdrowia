using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlacowkaZdrowia.DAL;
using PlacowkaZdrowia.Models;
using PlacowkaZdrowia.ViewModels;

namespace PlacowkaZdrowia.Controllers
{
    public class HomeController : Controller
    {
        private PlacowkaZdrowiaContext db = new PlacowkaZdrowiaContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Witamy w placówce zdrowia!";

            return View();
        }

        public ActionResult About()
        {
            //var data = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };
            var query = "SELECT DataRejestracji, COUNT(*) AS PacjentCount "
                + "FROM Osoba "
                + "WHERE DataRejestracji IS NOT NULL "
                + "GROUP BY DataRejestracji";
            var data = db.Database.SqlQuery<RejestracjaDateGroup>(query);
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
