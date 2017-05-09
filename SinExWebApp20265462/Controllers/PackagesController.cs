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
    public class PackagesController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        //// GET: Packages
        //public ActionResult Index()
        //{
        //    var packages = db.Packages.Include(p => p.Shipment);
        //    return View(packages.ToList());
        //}

        public ActionResult Index(int? waybillId)
        {
            
            var packages = db.Packages.Include(p => p.Shipment).Where(p=>p.WaybillId == waybillId).AsEnumerable();
            return View(packages.ToList());
        }


        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PackageId,PackageTypeSize,Description,Value,CustomerWeight,ActualWeight,PackageCost,WaybillId")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", package.WaybillId);
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", package.WaybillId);

            if (Session["oldPackageWeight"] == null) Session["oldPackageWeight"] = package.ActualWeight;
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PackageId,PackageTypeSize,Description,Value,CustomerWeight,ActualWeight,PackageCost,WaybillId")] Package package)
        {
            if (ModelState.IsValid)
            {
                decimal oldPackageCost = package.PackageCost;
                if((float)Session["oldPackageWeight"] == 0)
                { 
                    package.PackageCost = package.PackageCost * (decimal)package.ActualWeight / (decimal)package.CustomerWeight;
                }
                else
                {
                    float oldPackage = (float)Session["oldPackageWeight"];
                    package.PackageCost = package.PackageCost * (decimal)package.ActualWeight / (decimal)oldPackage;
                }
                if (Session["oldPackageWeight"] != null) Session.Remove("oldPackageWeight");

                Shipment shipment = db.Shipments.Find(package.WaybillId);
                shipment.ShipmentCost = shipment.ShipmentCost - oldPackageCost + package.PackageCost;
                shipment.Status = "Picked Up";
                shipment.ShippedDate = DateTime.Now;

                //send email when package is picked up
                string rmail = shipment.RecipientEmail;
                string smail = shipment.ShippingAccount.Email;
                if (rmail == smail)
                {
                    if (shipment.NotifyRecipient || shipment.NotifySender)
                        SendInvoiceMail(rmail, shipment.WaybillId);
                }
                else
                {
                    if (shipment.NotifyRecipient)
                        SendInvoiceMail(rmail, shipment.WaybillId);
                    if (shipment.NotifySender)
                        SendInvoiceMail(smail, shipment.WaybillId);
                }

                db.Entry<Shipment>(shipment).State = EntityState.Modified;

                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { waybillId = package.WaybillId });
            }
            ViewBag.WaybillId = new SelectList(db.Shipments, "WaybillId", "ReferenceNumber", package.WaybillId);
            return RedirectToAction("Index","Home");
        }

        // GET: Packages/EditPackages/id
        public ActionResult SelectEditPackage()
        {
            IEnumerable<Shipment> shipments = db.Shipments;
            foreach (var item in shipments)
            {
                // A fast way but not a normal way
                item.ReferenceNumber = ShowWaybillId(item.WaybillId);
            }
            ViewBag.IdList = new SelectList(shipments, "waybillId", "ReferenceNumber", "referenceNumber"); ;
            return View();
        }
        

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
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
