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
    [Authorize(Users = "admin@mailSibCemAdmin.ru, kadr@mailSibCemKadr.ru")]
    public class DutyJourneysController : Controller
    {
        private BusinessTripsContext db = new BusinessTripsContext();

        [HttpPost]
        public ActionResult Search(string surname)
        {
            if (surname == "")
            {
                ViewBag.Message = "Некорректные данные для поиска!";
                return PartialView("Search");
            }

            var allEmpls = db.Employees.Where(a => a.Surname.Contains(surname)).ToList();

            if (allEmpls.Count <= 0)
            {
                ViewBag.Message = "Сотрудник с такой фамилией не найден!";
                return PartialView("Search");
            }




            var dutyJourneyListAll = from dj in db.DutyJourneys.ToList()
                                     join hotel in db.Hotels.ToList() on dj.Id equals hotel.DutyJourneyId
                                     join passage in db.Passeges.ToList() on hotel.DutyJourneyId equals passage.DutyJourneyId
                                     join empls in db.Employees on passage.DutyJourneyId equals empls.DutyJourneyId where empls.Surname == surname
                                     select new DutyJourney
                                     {
                                         Point = dj.Point,
                                         BeginTrip = dj.BeginTrip,
                                         FinalTrip = dj.FinalTrip,
                                         WorkDay = dj.WorkDay,
                                         FreeDay = dj.FreeDay,
                                         Country = dj.Country,
                                         City = dj.City,
                                         Additionally = dj.Additionally,
                                         Passages = dj.Passages,
                                         Hotels = dj.Hotels,
                                     };


            return PartialView("_Search", dutyJourneyListAll);

        }

        [Authorize(Users = "kadr@mailSibCemKadr.ru")]
        public ActionResult _Request()
        {
            var dutyJournes = db.DutyJourneys.Include(dj => dj.Employees).Include(dj => dj.Hotels).Include(dj => dj.Passages);
            return View(dutyJournes);
         
        }


        // GET: DutyJourneys
        public ActionResult Index()
        {
            
            return View(db.DutyJourneys.ToList());
        }

        // GET: DutyJourneys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DutyJourneys/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Point,WorkDay,FreeDay,Country,City,Additionally,BeginTrip,FinalTrip")] DutyJourney dutyJourney)
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
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Point,WorkDay,FreeDay,Country,City,Additionally,BeginTrip,FinalTrip")] DutyJourney dutyJourney)
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
        [Authorize(Users = "admin@mailSibCemAdmin.ru")]
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
        [Authorize(Users = "admin@mailSibCemAdmin.ru")]
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
