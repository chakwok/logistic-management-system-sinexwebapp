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

        // GET: ShipmentCostCalculator
        public ActionResult ShipmentCostCalculator()
        {
            var shipmentCostCalculator = new CalculatorShipmentViewModel();
            shipmentCostCalculator.Packages = new CalculatorPackageViewModel[10];

            shipmentCostCalculator.Destinations = PopulateDestinationsDropDownList().ToList();
            shipmentCostCalculator.ServiceTypes = PopulateServiceTypesDropDownList().ToList();
            shipmentCostCalculator.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();

            shipmentCostCalculator.NumberOfPackages = 1;
            shipmentCostCalculator.ShipmentCost = 0;

            for (int i = 0; i < 10; i++)
            {
                shipmentCostCalculator.Packages[i] = new CalculatorPackageViewModel();
                shipmentCostCalculator.Packages[i].PackageTypeSizes = PopulatePackageTypeSizesDropDownList().ToList();
                shipmentCostCalculator.Packages[i].PackageCost = 0;
            }

            return View(shipmentCostCalculator);
        }

        [HttpPost]
        public ActionResult ShipmentCostCalculator(CalculatorShipmentViewModel shipment)
        {

            shipment.Destinations = PopulateDestinationsDropDownList().ToList();
            shipment.ServiceTypes = PopulateServiceTypesDropDownList().ToList();
            shipment.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();

            shipment.ShipmentCost = 0;

            //shipment.Packages = packages;
            foreach(CalculatorPackageViewModel package in shipment.Packages)
            {
                package.PackageTypeSizes = PopulatePackageTypeSizesDropDownList().ToList();

                if (package == null) continue;
                PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(package.PackageTypeSizeID);
                ServicePackageFee servicePackageFee = db.ServicePackageFees.First(s => (s.ServiceTypeID == shipment.ServiceTypeID) && (s.PackageTypeID == packageTypeSize.PackageTypeID));

                decimal packageCost = 0;
                float weightLimit = db.PackageTypeSizes.Find(package.PackageTypeSizeID).WeightLimit;

                packageCost = (decimal) package.Weight * servicePackageFee.Fee;

                if (weightLimit > 0 && package.Weight > weightLimit)
                {
                    packageCost += 500;
                }

                packageCost = (packageCost < servicePackageFee.MinimumFee) ? servicePackageFee.MinimumFee : packageCost;
                packageCost = ConvertCurrency(shipment.CurrencyCode, packageCost);
                shipment.ShipmentCost += packageCost;
            }

            
            return View(shipment);
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
