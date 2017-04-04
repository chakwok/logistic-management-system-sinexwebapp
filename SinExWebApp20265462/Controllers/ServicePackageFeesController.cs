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
using System.Web.WebPages;

namespace SinExWebApp20265462.Controllers
{
    public class ServicePackageFeesController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        // GET: ServicePackageFees
        public ActionResult Index()
        {
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            return View(servicePackageFees.ToList());
        }

        public ActionResult ServicePackageFeesCalculator(string Origin, string Destination, int? ServiceTypeID, int? PackageTypeSizeID, float? Weight,string CurrencyCode)
        {
            var servicePackageFeesCalculator = new ServicePackageFeesCalculatorViewModel();

            servicePackageFeesCalculator.Destinations = PopulateDestinationsDropDownList().ToList();
            servicePackageFeesCalculator.ServiceTypes = PopulateServiceTypesDropDownList().ToList();
            servicePackageFeesCalculator.PackageTypeSizes = PopulatePackageTypeSizesDropDownList().ToList();
            servicePackageFeesCalculator.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();

            if (ServiceTypeID == null && PackageTypeSizeID == null)
            {
                servicePackageFeesCalculator.PackageCost = 0;
                return View(servicePackageFeesCalculator);
            }

            PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(PackageTypeSizeID);
            ServicePackageFee servicePackageFee = db.ServicePackageFees.First(s => (s.ServiceTypeID == ServiceTypeID) && (s.PackageTypeID == packageTypeSize.PackageTypeID));
            float packageCost = 0;
            float weightLimit = db.PackageTypeSizes.Find(PackageTypeSizeID).WeightLimit;

            if (Weight != null) packageCost = (float) Weight * (float) servicePackageFee.Fee;
            if (weightLimit > 0 && Weight > weightLimit)
            {
                packageCost += 500;
            }
            packageCost = (packageCost < (float)servicePackageFee.MinimumFee) ? (float)servicePackageFee.MinimumFee : packageCost;

            servicePackageFeesCalculator.PackageCost = ConvertCurrency(CurrencyCode, (decimal) packageCost);
            return View(servicePackageFeesCalculator);
        }

        // GET: ServicePackageFees/GenerateServicePackageFeesReport
        public ActionResult GenerateServicePackageFeesReport(string CurrencyCode)
        {
            var servicePackageFeeIndex = new ServicePackageFeesIndexViewModel();
            servicePackageFeeIndex.Currency = new ServicePackageFeesCurrenciesListViewModel();

            servicePackageFeeIndex.Currency.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();

            var servicePackageFeeQuery = from s in db.ServicePackageFees
                                         select new ServicePackageFeesListViewModel
                                         {
                                             ServiceType = s.ServiceType,
                                             PackageType = s.PackageType,
                                         };

            servicePackageFeeIndex.ServicePackageFees = servicePackageFeeQuery.ToList();

            foreach(var item in servicePackageFeeIndex.ServicePackageFees.Select(s => s.ServiceType).Distinct())
            {
                foreach(var servicePackageFee in item.ServicePackageFees)
                {
                    servicePackageFee.Fee = ConvertCurrency(CurrencyCode, servicePackageFee.Fee);
                    servicePackageFee.MinimumFee = ConvertCurrency(CurrencyCode, servicePackageFee.MinimumFee);
                }
            }

            return View(servicePackageFeeIndex);
        }


        // GET: ServicePackageFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Create
        public ActionResult Create()
        {
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type");
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type");
            return View();
        }

        // POST: ServicePackageFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.ServicePackageFees.Add(servicePackageFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePackageFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            db.ServicePackageFees.Remove(servicePackageFee);
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
