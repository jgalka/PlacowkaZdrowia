using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlacowkaZdrowia.Models;
using PlacowkaZdrowia.DAL;
using PlacowkaZdrowia.ViewModels;

namespace PlacowkaZdrowia.Controllers
{ 
    public class LekarzController : Controller
    {
        private PlacowkaZdrowiaContext db = new PlacowkaZdrowiaContext();

        //
        // GET: /Instructor/

        public ActionResult Index(Int32? id, Int32? zabiegID)
        {
            var viewModel = new LekarzIndexData();
            viewModel.Lekarze = db.Lekarze
                .Include(i => i.Zabiegi.Select(c => c.Dzial))
                .OrderBy(i => i.Nazwisko);

            if (id != null)
            {
                ViewBag.OsobaID = id.Value;
                viewModel.Zabiegi = viewModel.Lekarze.Where(i => i.OsobaID == id.Value).Single().Zabiegi;
            }


            if (zabiegID != null)
            {
                ViewBag.ZabiegID = zabiegID.Value;

                var selectedZabieg = viewModel.Zabiegi.Where(x => x.ZabiegID == zabiegID).Single();
                db.Entry(selectedZabieg).Collection(x => x.Rejestracje).Load();
                foreach (Rejestracja rejestracja in selectedZabieg.Rejestracje)
                {
                    db.Entry(rejestracja).Reference(x => x.Pacjent).Load();
                }

                viewModel.Rejestracje = selectedZabieg.Rejestracje;
            }

            return View(viewModel);
        }
        
        //
        // GET: /Instructor/Details/5

        public ViewResult Details(int id)
        {
            Lekarz lekarz = db.Lekarze.Find(id);
            return View(lekarz);
        }

        //
        // GET: /Instructor/Create

        public ActionResult Create()
        {
            ViewBag.OsobaID = new SelectList(db.OfficeAssignments, "OsobaID", "Location");
            return View();
        } 

        //
        // POST: /Instructor/Create

        [HttpPost]
        public ActionResult Create(Lekarz lekarz)
        {
            if (ModelState.IsValid)
            {
                db.Lekarze.Add(lekarz);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OsobaID = new SelectList(db.OfficeAssignments, "OsobaID", "Location", lekarz.OsobaID);
            return View(lekarz);
        }
        
        //
        // GET: /Instructor/Edit/5

        public ActionResult Edit(int id)
        {
            Lekarz lekarz = db.Lekarze
                .Include(i => i.Zabiegi)
                .Where(i => i.OsobaID == id)
                .Single();
            PopulateAssignedZabiegData(lekarz);
            return View(lekarz);
        }

        private void PopulateAssignedZabiegData(Lekarz lekarz)
        {
            var allZabiegi = db.Zabiegi;
            var lekarzZabiegi = new HashSet<int>(lekarz.Zabiegi.Select(c => c.ZabiegID));
            var viewModel = new List<AssignedZabiegData>();
            foreach (var zabieg in allZabiegi)
            {
                viewModel.Add(new AssignedZabiegData
                {
                    ZabiegID = zabieg.ZabiegID,
                    Tytul = zabieg.Tytul,
                    Assigned = lekarzZabiegi.Contains(zabieg.ZabiegID)
                });
            }
            ViewBag.Zabiegi = viewModel;
        }

        //
        // POST: /Instructor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedZabiegi)
        {
            var lekarzToUpdate = db.Lekarze
                .Include(i => i.Zabiegi)
                .Where(i => i.OsobaID == id)
                .Single();
            if (TryUpdateModel(lekarzToUpdate, "", null, new string[] { "Zabiegi" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(lekarzToUpdate.OfficeAssignment.Location))
                    {
                        lekarzToUpdate.OfficeAssignment = null;
                    }

                    UpdateLekarzZabiegi(selectedZabiegi, lekarzToUpdate);

                    db.Entry(lekarzToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedZabiegData(lekarzToUpdate);
            return View(lekarzToUpdate);
        }

        private void UpdateLekarzZabiegi(string[] selectedZabiegi, Lekarz lekarzToUpdate)
        {
            if (selectedZabiegi == null)
            {
                lekarzToUpdate.Zabiegi = new List<Zabieg>();
                return;
            }

            var selectedZabiegiHS = new HashSet<string>(selectedZabiegi);
            var lekarzZabiegi = new HashSet<int>
                (lekarzToUpdate.Zabiegi.Select(c => c.ZabiegID));
            foreach (var zabieg in db.Zabiegi)
            {
                if (selectedZabiegiHS.Contains(zabieg.ZabiegID.ToString()))
                {
                    if (!lekarzZabiegi.Contains(zabieg.ZabiegID))
                    {
                        lekarzToUpdate.Zabiegi.Add(zabieg);
                    }
                }
                else
                {
                    if (lekarzZabiegi.Contains(zabieg.ZabiegID))
                    {
                        lekarzToUpdate.Zabiegi.Remove(zabieg);
                    }
                }
            }
        }

        //
        // GET: /Instructor/Delete/5
 
        public ActionResult Delete(int id)
        {
            Lekarz lekarz = db.Lekarze.Find(id);
            return View(lekarz);
        }

        //
        // POST: /Instructor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Lekarz lekarz = db.Lekarze.Find(id);
            db.Lekarze.Remove(lekarz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}