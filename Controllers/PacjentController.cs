using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlacowkaZdrowia.Models;
using PlacowkaZdrowia.DAL;
using PagedList;

namespace PlacowkaZdrowia.Controllers
{
    public class PacjentController : Controller
    {
        private IPacjentRepository pacjentRepository;


        public PacjentController()
        {
            this.pacjentRepository = new PacjentRepository(new PlacowkaZdrowiaContext());
        }

        public PacjentController(IPacjentRepository pacjentRepository)
        {
            this.pacjentRepository = pacjentRepository;
        }


        //
        // GET: /pacjent/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var pacjenci = from s in pacjentRepository.GetPacjenci()
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pacjenci = pacjenci.Where(s => s.Nazwisko.ToUpper().Contains(searchString.ToUpper())
                                       || s.Imie.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    pacjenci = pacjenci.OrderByDescending(s => s.Nazwisko);
                    break;
                case "Date":
                    pacjenci = pacjenci.OrderBy(s => s.DataRejestracji);
                    break;
                case "Date desc":
                    pacjenci = pacjenci.OrderByDescending(s => s.DataRejestracji);
                    break;
                default:
                    pacjenci = pacjenci.OrderBy(s => s.Nazwisko);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(pacjenci.ToPagedList(pageNumber, pageSize));
        }


        //
        // GET: /pacjent/Details/5

        public ViewResult Details(int id)
        {
            Pacjent pacjent = pacjentRepository.GetPacjentByID(id);
            return View(pacjent);
        }

        //
        // GET: /pacjent/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /pacjent/Create

        [HttpPost]
        public ActionResult Create(Pacjent pacjenci)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pacjentRepository.InsertPacjent(pacjenci);
                    pacjentRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(pacjenci);
        }

        //
        // GET: /pacjent/Edit/5

        public ActionResult Edit(int id)
        {
            Pacjent pacjent = pacjentRepository.GetPacjentByID(id);
            return View(pacjent);
        }

        //
        // POST: /pacjent/Edit/5

        [HttpPost]
        public ActionResult Edit(Pacjent pacjent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pacjentRepository.UpdatePacjent(pacjent);
                    pacjentRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(pacjent);
        }

        //
        // GET: /pacjent/Delete/5

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            Pacjent pacjent = pacjentRepository.GetPacjentByID(id);
            return View(pacjent);
        }


        //
        // POST: /pacjent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Pacjent pacjent = pacjentRepository.GetPacjentByID(id);
                pacjentRepository.DeletePacjent(id);
                pacjentRepository.Save();
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary { 
                { "id", id }, 
                { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            pacjentRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}