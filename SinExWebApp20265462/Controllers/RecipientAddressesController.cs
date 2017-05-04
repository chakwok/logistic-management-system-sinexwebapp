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
    public class RecipientAddressesController : BaseController
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
            RecipientAddressViewModel recipientAddress = new RecipientAddressViewModel();
            recipientAddress.Cities = PopulateDestinationsDropDownList().ToList();
            return View(recipientAddress);
        }

        // POST: RecipientAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipientAddressID,NickName,RecipientName,RecipientShippingAccountId,CompanyName,DepartmentName,Building,Street,City,ProvinceCode,PostalCode,RecipientPhoneNumber,RecipientEmail")] RecipientAddressViewModel recipientAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                RecipientAddress recipientAddress = new RecipientAddress(recipientAddressViewModel.RecipientAddressID,
                    recipientAddressViewModel.NickName, recipientAddressViewModel.RecipientName, recipientAddressViewModel.RecipientShippingAccountId,
                    recipientAddressViewModel.CompanyName, recipientAddressViewModel.DepartmentName,
                    recipientAddressViewModel.Building, recipientAddressViewModel.Street, recipientAddressViewModel.City, recipientAddressViewModel.ProvinceCode,
                    recipientAddressViewModel.PostalCode, recipientAddressViewModel.RecipientPhoneNumber, recipientAddressViewModel.RecipientEmail
                    );

                recipientAddress.ProvinceCode = GetProvince(recipientAddress.City);
                db.RecipientAddresses.Add(recipientAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipientAddressViewModel);
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
            RecipientAddressViewModel recipientAddressViewModel = new RecipientAddressViewModel(recipientAddress.RecipientAddressID,
                    recipientAddress.NickName, recipientAddress.RecipientName, recipientAddress.RecipientShippingAccountId,
                    recipientAddress.CompanyName, recipientAddress.DepartmentName,
                    recipientAddress.Building, recipientAddress.Street, recipientAddress.City, recipientAddress.ProvinceCode,
                    recipientAddress.PostalCode, recipientAddress.RecipientPhoneNumber, recipientAddress.RecipientEmail);

            recipientAddressViewModel.Cities = PopulateDestinationsDropDownList().ToList();
            return View(recipientAddressViewModel);
        }

        // POST: RecipientAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipientAddressID,NickName,RecipientName,RecipientShippingAccountId,CompanyName,DepartmentName,Building,Street,City,ProvinceCode,PostalCode,RecipientPhoneNumber,RecipientEmail")]  RecipientAddressViewModel recipientAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                RecipientAddress recipientAddress = new RecipientAddress(recipientAddressViewModel.RecipientAddressID,
                    recipientAddressViewModel.NickName, recipientAddressViewModel.RecipientName, recipientAddressViewModel.RecipientShippingAccountId,
                    recipientAddressViewModel.CompanyName, recipientAddressViewModel.DepartmentName,
                    recipientAddressViewModel.Building, recipientAddressViewModel.Street, recipientAddressViewModel.City, recipientAddressViewModel.ProvinceCode,
                    recipientAddressViewModel.PostalCode, recipientAddressViewModel.RecipientPhoneNumber, recipientAddressViewModel.RecipientEmail
                    );

                recipientAddress.ProvinceCode = GetProvince(recipientAddress.City);
                db.Entry(recipientAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipientAddressViewModel);
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
