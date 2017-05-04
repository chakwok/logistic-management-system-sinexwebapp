using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20265462.Models;
using SinExWebApp20265462.ViewModels;

namespace SinExWebApp20265462.Controllers
{
    public class PickupsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: Pickups
        public ActionResult Index()
        {
            return View(db.Pickups.ToList());
        }

        // GET: Pickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // GET: Pickups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupId,Type,Building,Street,City,ProvinceCode,PostalCode,PickupDateTime")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickup);
        }

        // GET: Pickups/ArrangePickup
        public ActionResult ArrangePickup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ArrangePickup(PickupViewModel pickupViewModel)
        {
            return RedirectToAction("ConfirmPickup");
        }

        // GET: Pickups/ArrangePrearrangedPickup
        public ActionResult ArrangePrearrangedPickup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ArrangePrearrangedPickup(int? dummy)
        {
            return RedirectToAction("ConfirmPickup");
        }

        // GET: Pickups/ConfirmPickup
        public ActionResult ConfirmPickup()
        {
            return View();
        }

        // TODO: POST: Pickups/ConfirmPickup

        // GET: Pickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickupId,Type,Building,Street,City,ProvinceCode,PostalCode,PickupDateTime")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.Pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pickup pickup = db.Pickups.Find(id);
            db.Pickups.Remove(pickup);
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
