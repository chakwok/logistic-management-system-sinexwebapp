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
    public class RecipientAddressesController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: RecipientAddresses
        public ActionResult Index()
        {
            return View(db.RecipientAddresses.ToList());
        }

        // GET: RecipientAddresses/Details/5
     /*   public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipientAddress recipientAddress = db.RecipientAddresses.Find(id);
            if (recipientAddress == null)
            {
                return HttpNotFound();
            }
            return View(recipientAddress);
        }
        */
        // GET: RecipientAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecipientAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipientAddressID,NickName,Building,Street,City,ProvinceCode,PostalCode")] RecipientAddress recipientAddress)
        {
            if (ModelState.IsValid)
            {
                db.RecipientAddresses.Add(recipientAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipientAddress);
        }

        // GET: RecipientAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipientAddress recipientAddress = (RecipientAddress)db.RecipientAddresses.Find(id);
            if (recipientAddress == null)
            {
                return HttpNotFound();
            }
            return View(recipientAddress);
        }

        // POST: RecipientAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipientAddressID,NickName,Building,Street,City,ProvinceCode,PostalCode")] RecipientAddress recipientAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipientAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipientAddress);
        }

        // GET: RecipientAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecipientAddress recipientAddress = (RecipientAddress)db.RecipientAddresses.Find(id);
            if (recipientAddress == null)
            {
                return HttpNotFound();
            }
            return View(recipientAddress);
        }

        // POST: RecipientAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecipientAddress recipientAddress = (RecipientAddress)db.RecipientAddresses.Find(id);
            db.RecipientAddresses.Remove(recipientAddress);
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
