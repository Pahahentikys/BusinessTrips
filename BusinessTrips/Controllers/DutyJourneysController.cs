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
    public class DutyJourneysController : Controller
    {
        private BusinessTripsContext db = new BusinessTripsContext();

        // GET: DutyJourneys
        public ActionResult Index()
        {
            return View(db.DutyJourneys.ToList());
        }

        // GET: DutyJourneys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DutyJourney dutyJourney = db.DutyJourneys.Find(id);
            if (dutyJourney == null)
            {
                return HttpNotFound();
            }
            return View(dutyJourney);
        }

        // GET: DutyJourneys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DutyJourneys/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkDay,FreeDay,Country,City,Point,Additionally,BeginTrip,FinalTrip")] DutyJourney dutyJourney)
        {
            if (ModelState.IsValid)
            {
                db.DutyJourneys.Add(dutyJourney);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dutyJourney);
        }

        // GET: DutyJourneys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DutyJourney dutyJourney = db.DutyJourneys.Find(id);
            if (dutyJourney == null)
            {
                return HttpNotFound();
            }
            return View(dutyJourney);
        }

        // POST: DutyJourneys/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkDay,FreeDay,Country,City,Point,Additionally,BeginTrip,FinalTrip")] DutyJourney dutyJourney)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dutyJourney).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dutyJourney);
        }

        // GET: DutyJourneys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DutyJourney dutyJourney = db.DutyJourneys.Find(id);
            if (dutyJourney == null)
            {
                return HttpNotFound();
            }
            return View(dutyJourney);
        }

        // POST: DutyJourneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DutyJourney dutyJourney = db.DutyJourneys.Find(id);
            db.DutyJourneys.Remove(dutyJourney);
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
