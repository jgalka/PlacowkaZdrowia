using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlacowkaZdrowia.Models;
using PlacowkaZdrowia.DAL;

namespace PlacowkaZdrowia.Controllers
{
    public class ZabiegController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /zabieg/

        public ActionResult Index(int? SelectedDzial)
        {
            var dzialy = unitOfWork.DzialRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.SelectedDzial = new SelectList(dzialy, "DzialID", "Name", SelectedDzial);

            int dzialID = SelectedDzial.GetValueOrDefault();
            return View(unitOfWork.ZabiegRepository.Get(
                filter: d => !SelectedDzial.HasValue || d.DzialID == dzialID,
                orderBy: q => q.OrderBy(d => d.ZabiegID),
                includeProperties: "Dzial"));
        }

        //
        // GET: /zabieg/Details/5

        public ActionResult Details(int id)
        {
            var query = "SELECT * FROM Zabieg WHERE ZabiegID = @p0";
            return View(unitOfWork.ZabiegRepository.GetWithRawSql(query, id).Single());
        }

        //
        // GET: /zabieg/Create

        public ActionResult Create()
        {
            PopulateDzialyDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Zabieg zabieg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ZabiegRepository.Insert(zabieg);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDzialyDropDownList(zabieg.DzialID);
            return View(zabieg);
        }

        public ActionResult Edit(int id)
        {
            Zabieg zabieg = unitOfWork.ZabiegRepository.GetByID(id);
            PopulateDzialyDropDownList(zabieg.DzialID);
            return View(zabieg);
        }

        [HttpPost]
        public ActionResult Edit(Zabieg zabieg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ZabiegRepository.Update(zabieg);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDzialyDropDownList(zabieg.DzialID);
            return View(zabieg);
        }

        private void PopulateDzialyDropDownList(object selectedDzial = null)
        {
            var dzialyQuery = unitOfWork.DzialRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DzialID = new SelectList(dzialyQuery, "DzialID", "Name", selectedDzial);
        }

        //
        // GET: /zabieg/Delete/5

        public ActionResult Delete(int id)
        {
            Zabieg zabieg = unitOfWork.ZabiegRepository.GetByID(id);
            return View(zabieg);
        }

        //
        // POST: /zabieg/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Zabieg zabieg = unitOfWork.ZabiegRepository.GetByID(id);
            unitOfWork.ZabiegRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateZabiegKoszt(int? multiplier)
        {
            if (multiplier != null)
            {
                ViewBag.RowsAffected = unitOfWork.ZabiegRepository.UpdateZabiegKoszt(multiplier.Value);
            }
            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}