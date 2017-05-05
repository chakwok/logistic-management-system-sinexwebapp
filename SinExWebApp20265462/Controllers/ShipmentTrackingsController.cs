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
    public class ShipmentTrackingsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: ShipmentTrackings
        public ActionResult Index()
        {
            var shipmentTracking = db.ShipmentTracking.Include(s => s.Shipment);
            return View(shipmentTracking.ToList());
        }

        // GET: ShipmentTrackings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                var sh = db.ShipmentTracking.Where(s => s.ShipmentTrackingID == id);
                if (!sh.Any()) return View();
                else return View(sh.First());
                
            }
        }

        // GET: ShipmentTrackings/Create
        public ActionResult Create()
        {
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber");
            return View();
        }

        // POST: ShipmentTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipmentTrackingID,WaybillId")] ShipmentTracking shipmentTracking)
        {
            if (ModelState.IsValid)
            {
                db.ShipmentTracking.Add(shipmentTracking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", shipmentTracking.WaybillId);
            return View(shipmentTracking);
        }

        // GET: ShipmentTrackings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipmentTracking shipmentTracking = db.ShipmentTracking.Find(id);
            if (shipmentTracking == null)
            {
                return HttpNotFound();
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", shipmentTracking.WaybillId);
            return View(shipmentTracking);
        }

        // POST: ShipmentTrackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipmentTrackingID,WaybillId")] ShipmentTracking shipmentTracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipmentTracking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", shipmentTracking.WaybillId);
            return View(shipmentTracking);
        }

        // GET: ShipmentTrackings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipmentTracking shipmentTracking = db.ShipmentTracking.Find(id);
            if (shipmentTracking == null)
            {
                return HttpNotFound();
            }
            return View(shipmentTracking);
        }

        // POST: ShipmentTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShipmentTracking shipmentTracking = db.ShipmentTracking.Find(id);
            db.ShipmentTracking.Remove(shipmentTracking);
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
