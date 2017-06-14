using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessTrips.Models;

namespace BusinessTrips.Controllers
{
    public class PassagesController : Controller
    {
        private BusinessTripsContext db = new BusinessTripsContext();

        // GET: Passages
        public ActionResult Index()
        {
            var passeges = db.Passeges.Include(p => p.DutyJourney);
            return View(passeges.ToList());
        }

        // GET: Passages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passage passage = db.Passeges.Find(id);
            if (passage == null)
            {
                return HttpNotFound();
            }
            return View(passage);
        }

        // GET: Passages/Create
        public ActionResult Create()
        {
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "City");
            return View();
        }

        // POST: Passages/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateofDeparture,DateofArrival,Transport,DestinationPoint,DeparturePoint,DutyJourneyId")] Passage passage)
        {
            if (ModelState.IsValid)
            {
                db.Passeges.Add(passage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "City", passage.DutyJourneyId);
            return View(passage);
        }

        // GET: Passages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passage passage = db.Passeges.Find(id);
            if (passage == null)
            {
                return HttpNotFound();
            }
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "City", passage.DutyJourneyId);
            return View(passage);
        }

        // POST: Passages/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateofDeparture,DateofArrival,Transport,DestinationPoint,DeparturePoint,DutyJourneyId")] Passage passage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DutyJourneyId = new SelectList(db.DutyJourneys, "Id", "City", passage.DutyJourneyId);
            return View(passage);
        }

        // GET: Passages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passage passage = db.Passeges.Find(id);
            if (passage == null)
            {
                return HttpNotFound();
            }
            return View(passage);
        }

        // POST: Passages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passage passage = db.Passeges.Find(id);
            db.Passeges.Remove(passage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
