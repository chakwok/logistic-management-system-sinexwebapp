using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20265462.Models;

namespace SinExWebApp20265462.Controllers
{
    public class PickUpLocationsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: PickUpLocations
        public ActionResult Index()
        {
            return View(db.PickUpLocations.ToList());
        }

        // GET: PickUpLocations/Details/5
      /*  public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpLocation pickUpLocation = db.PickUpLocations.Find(id);
            if (pickUpLocation == null)
            {
                return HttpNotFound();
            }
            return View(pickUpLocation);
        }
        */
        // GET: PickUpLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PickUpLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickUpLocationID,PickUpNickName,PickUpBuilding,PickUpStreet,PickUpCity,PickUpProvinceCode,PickUpPostalCode")] PickUpLocation pickUpLocation)
        {
            if (ModelState.IsValid)
            {
                db.PickUpLocations.Add(pickUpLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickUpLocation);
        }

        // GET: PickUpLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpLocation pickUpLocation = (PickUpLocation)db.PickUpLocations.Find(id);
            if (pickUpLocation == null)
            {
                return HttpNotFound();
            }
            return View(pickUpLocation);
        }

        // POST: PickUpLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickUpLocationID,PickUpNickName,PickUpBuilding,PickUpStreet,PickUpCity,PickUpProvinceCode,PickUpPostalCode")] PickUpLocation pickUpLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickUpLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickUpLocation);
        }

        // GET: PickUpLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickUpLocation pickUpLocation = (PickUpLocation)db.PickUpLocations.Find(id);
            if (pickUpLocation == null)
            {
                return HttpNotFound();
            }
            return View(pickUpLocation);
        }

        // POST: PickUpLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickUpLocation pickUpLocation = (PickUpLocation)db.PickUpLocations.Find(id);
            db.PickUpLocations.Remove(pickUpLocation);
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
