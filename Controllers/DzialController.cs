using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlacowkaZdrowia.Models;
using PlacowkaZdrowia.DAL;
using System.Data.Entity.Infrastructure;

namespace PlacowkaZdrowia.Controllers
{ 
    public class DzialController : Controller
    {
        private PlacowkaZdrowiaContext db = new PlacowkaZdrowiaContext();

        //
        // GET: /dzial/

        public ViewResult Index()
        {
            var dzialy = db.Dzialy.Include(d => d.Administrator);
            return View(dzialy.ToList());
        }

        //
        // GET: /dzial/Details/5

        public ViewResult Details(int id)
        {
            Dzial dzial = db.Dzialy.Find(id);
            return View(dzial);
        }

        //
        // GET: /dzial/Create

        public ActionResult Create()
        {
            ViewBag.OsobaID = new SelectList(db.Lekarze, "OsobaID", "FullName");
            return View();
        } 

        //
        // POST: /dzial/Create

        [HttpPost]
        public ActionResult Create(Dzial dzial)
        {
            if (ModelState.IsValid)
            {
                db.Dzialy.Add(dzial);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.OsobaID = new SelectList(db.Lekarze, "OsobaID", "FullName", dzial.OsobaID);
            return View(dzial);
        }
        
        //
        // GET: /dzial/Edit/5
 
        public ActionResult Edit(int id)
        {
            Dzial dzial = db.Dzialy.Find(id);
            ViewBag.OsobaID = new SelectList(db.Lekarze, "OsobaID", "FullName", dzial.OsobaID);
            return View(dzial);
        }

        //
        // POST: /dzial/Edit/5

        [HttpPost]
        public ActionResult Edit(Dzial dzial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ValidateOneAdministratorAssignmentPerLekarz(dzial);
                }
                if (ModelState.IsValid)
                {
                    db.Entry(dzial).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Dzial)entry.GetDatabaseValues().ToObject();
                var clientValues = (Dzial)entry.Entity;
                if (databaseValues.Name != clientValues.Name)
                    ModelState.AddModelError("Name", "Current value: "
                        + databaseValues.Name);
                if (databaseValues.Budget != clientValues.Budget)
                    ModelState.AddModelError("Budget", "Current value: "
                        + String.Format("{0:c}", databaseValues.Budget));
                if (databaseValues.StartDate != clientValues.StartDate)
                    ModelState.AddModelError("StartDate", "Current value: "
                        + String.Format("{0:d}", databaseValues.StartDate));
                if (databaseValues.OsobaID != clientValues.OsobaID)
                    ModelState.AddModelError("OsobaID", "Current value: "
                        + db.Lekarze.Find(databaseValues.OsobaID).FullName);
                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was canceled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");
                dzial.Timestamp = databaseValues.Timestamp;
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
            }

            ViewBag.OsobaID = new SelectList(db.Lekarze, "OsobaID", "FullName", dzial.OsobaID);
            return View(dzial);
        }

        //
        // GET: /dzial/Delete/5

        public ActionResult Delete(int id, bool? concurrencyError)
        {
            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            Dzial dzial = db.Dzialy.Find(id);
            return View(dzial);
        }

        //
        // POST: /dzial/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Dzial dzial)
        {
            try
            {
                db.Entry(dzial).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary { { "concurrencyError", true } });
            }
            catch (DataException)
            {
                //Log the error (add a variable name after Exception)
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                return View(dzial);
            }
        }

        private void ValidateOneAdministratorAssignmentPerLekarz(Dzial dzial)
        {
            if (dzial.OsobaID != null)
            {
                var duplicateDzial = db.Dzialy
                    .Include("Administrator")
                    .Where(d => d.OsobaID == dzial.OsobaID)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (duplicateDzial != null && duplicateDzial.DzialID != dzial.DzialID)
                {
                    var errorMessage = String.Format(
                        "Instructor {0} {1} is already administrator of the {2} department.",
                        duplicateDzial.Administrator.Imie,
                        duplicateDzial.Administrator.Nazwisko,
                        duplicateDzial.Name);
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}