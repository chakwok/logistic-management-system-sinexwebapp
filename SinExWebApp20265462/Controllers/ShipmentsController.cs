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
using X.PagedList;
using Microsoft.AspNet.Identity;

namespace SinExWebApp20265462.Controllers
{
    public class ShipmentsController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();
        // GET: Shipments
        public ActionResult Index()
        {
            return View(db.Shipments.ToList());
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/ArrangeShipment
        public ActionResult ArrangeShipment ()
        {
            var shipment = new ArrangeShipmentViewModel();
            shipment.Packages = new ArrangeShipmentPackageViewModel[10];

            shipment.ServiceTypes = PopulateServiceTypesDropDownList().ToList();
            shipment.Destinations = PopulateDestinationsDropDownList().ToList();
            shipment.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();

            ViewBag.NumberOfPackages = 1;
            shipment.NumberOfPackages = 1;
            shipment.ShipmentCost = 0;

            for (int i = 0; i < 10; i++)
            {
                shipment.Packages[i] = new ArrangeShipmentPackageViewModel();
                shipment.Packages[i].PackageTypeSizes = PopulatePackageTypeSizesDropDownList().ToList();
                shipment.Packages[i].Description = "";
                shipment.Packages[i].Value = 0;
                shipment.Packages[i].PackageCost = 0;
            }


            return View(shipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArrangeShipment(ArrangeShipmentViewModel shipment, int? NumberOfPackages, bool addPackage = false, bool delPackage = false)
        {
            shipment.Destinations = PopulateDestinationsDropDownList().ToList();
            shipment.ServiceTypes = PopulateServiceTypesDropDownList().ToList();
            shipment.CurrencyCodes = PopulateCurrenciesDropDownList().ToList();
            for (int i = 0; i < 10; i++)
            {
                shipment.Packages[i].PackageTypeSizes = PopulatePackageTypeSizesDropDownList().ToList();
            }
            
            shipment.ShipmentCost = 0;

            if (addPackage == true)
            {
                if (shipment.NumberOfPackages < 10)
                {
                    shipment.NumberOfPackages++;
                }
                else
                {
                    ViewBag.PackageStatusMessage = "A shipment should include not more than 10 packages.";
                }
                ViewBag.NumberOfPackages = shipment.NumberOfPackages;
                return View(shipment);
            }
            else if (delPackage == true)
            {
                if (shipment.NumberOfPackages > 1)
                {
                    shipment.NumberOfPackages--;
                }
                else
                {
                    ViewBag.PackageStatusMessage = "A shipment should include at least 1 package.";
                }
                ViewBag.NumberOfPackages = shipment.NumberOfPackages;
                return View(shipment);
            }
            // TODO: Avoid Package.Weight = 0
            

            if (shipment.ShipmentPayer == "Recipient" || shipment.DTPayer == "Recipient")
            {
                if (shipment.RecipientShippingAccountId == null)
                {
                    ViewBag.RecipientStatusMessage = "Recipient Account ID is required to be the payer.";
                    return View(shipment);
                }
            }

            SaveToSessionState("newShipment", shipment);
            return RedirectToAction("ConfirmShipment");
        }

        public ActionResult ConfirmShipment()
        {
            ArrangeShipmentViewModel shipment = (ArrangeShipmentViewModel) Session["newShipment"];
            shipment.ServiceType = db.ServiceTypes.Find(shipment.ServiceTypeID).Type;
            shipment.RecipientProvince = GetProvince(shipment.Destination);

            ViewBag.NumberOfPackages = shipment.NumberOfPackages;
            shipment.ShipmentCost = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i >= shipment.NumberOfPackages || shipment.Packages[i].CustomerWeight == 0)
                {
                    shipment.Packages[i].PackageTypeSizeID = 1;
                    shipment.Packages[i].CustomerWeight = 0;
                    continue;
                }
                PackageTypeSize packageTypeSize = db.PackageTypeSizes.Find(shipment.Packages[i].PackageTypeSizeID);
                ServicePackageFee servicePackageFee = db.ServicePackageFees.First(s => (s.ServiceTypeID == shipment.ServiceTypeID) && (s.PackageTypeID == packageTypeSize.PackageTypeID));
                shipment.Packages[i].PackageTypeSize = packageTypeSize.PackageType.Type + " (" + packageTypeSize.Size + ")";

                decimal packageCost = 0;
                float weightLimit = db.PackageTypeSizes.Find(shipment.Packages[i].PackageTypeSizeID).WeightLimit;

                packageCost = (decimal)shipment.Packages[i].CustomerWeight * servicePackageFee.Fee;

                if (weightLimit > 0 && shipment.Packages[i].CustomerWeight > weightLimit)
                {
                    packageCost += 500;
                }

                packageCost = (packageCost < servicePackageFee.MinimumFee) ? servicePackageFee.MinimumFee : packageCost;
                packageCost = ConvertCurrency(shipment.CurrencyCode, packageCost);
                shipment.Packages[i].PackageCost = packageCost;
                shipment.ShipmentCost += packageCost;
            }

            return View(shipment);
        }


        // GET: Shipments/GenerateHistoryReport
        public ActionResult GenerateHistoryReport(int? CurrentShippingAccountId,
            DateTime? StartShippedDate, DateTime? EndShippedDate,
            string sortOrder, int? page)
        {
            // Instantiate an instance of the ShipmentsReportViewModel and the ShipmentsSearchViewModel.
            var shipmentSearch = new ShipmentsReportViewModel();
            shipmentSearch.Shipment = new ShipmentsSearchViewModel();

            // Code for paging
            ViewBag.CurrentSort = sortOrder;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            
            var name = User.Identity.GetUserName();
            int? ShippingAccountId = db.ShippingAccounts.First(s => s.UserName == name).ShippingAccountId;

            // Retain search condition for sorting
            if (User.IsInRole("Customer"))
            {
                /*
                if (ShippingAccountId == null)
                {
                    ShippingAccountId = CurrentShippingAccountId;
                }
                else
                {
                    page = 1;
                }
                */

                ViewBag.CurrentShippingAccountId = ShippingAccountId;
                shipmentSearch.Shipment.ShippingAccountId = ShippingAccountId.GetValueOrDefault();
                shipmentSearch.Shipment.AccountType = db.ShippingAccounts.First(s => s.UserName == name).AccountType;
                ViewBag.DisplayedShippingAccountId = ShowShippingAccountId(ShippingAccountId.GetValueOrDefault());
            }
            else
            {
                shipmentSearch.Shipment.AccountType = "Employee";
                ShippingAccountId = null;
            }
                


            if (StartShippedDate == null && EndShippedDate == null)
            {
                StartShippedDate = DateTime.MinValue;
                EndShippedDate = DateTime.MaxValue;
            }
            ViewBag.CurrentStartShippedDate = StartShippedDate;
            ViewBag.CurrentEndShippedDate = EndShippedDate;

            /*
            // Populate the ShippingAccountId dropdown list.
            shipmentSearch.Shipment.ShippingAccounts = PopulateShippingAccountsDropdownList().ToList();
            if (CurrentShippingAccountId != null) shipmentSearch.Shipment.ShippingAccountId = (int) CurrentShippingAccountId;*/

            // Initialize the query to retrieve shipments using the ShipmentsListViewModel.
            var shipmentQuery = from s in db.Shipments
                                select new ShipmentsListViewModel
                                {
                                    WaybillId = s.WaybillId,
                                    ServiceType = s.ServiceType,
                                    ShippedDate = s.ShippedDate,
                                    DeliveredDate = s.DeliveredDate,
                                    RecipientName = s.RecipientName,
                                    NumberOfPackages = s.NumberOfPackages,
                                    Origin = s.Origin,
                                    Destination = s.Destination,
                                    ShippingAccountId = s.ShippingAccountId
                                };

            // Code for sorting on properties
            ViewBag.ServiceTypeSortParm = sortOrder == "ServiceType" ? "ServiceType_desc" : "ServiceType";
            ViewBag.ShippedDateSortParm = sortOrder == "ShippedDate" ? "ShippedDate_desc" : "ShippedDate";
            ViewBag.DeliveredDateSortParm = sortOrder == "DeliveredDate" ? "DeliveredDate_desc" : "DeliveredDate";
            ViewBag.RecipientNameSortParm = sortOrder == "RecipientName" ? "RecipientName_desc" : "RecipientName";
            ViewBag.OriginSortParm = sortOrder == "Origin" ? "Origin_desc" : "Origin";
            ViewBag.DestinationSortParm = sortOrder == "Destination" ? "Destination_desc" : "Destination";
            switch (sortOrder)
            {
                case "ServiceType":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.ServiceType);
                    break;
                case "ServiceType_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.ServiceType);
                    break;
                case "ShippedDate":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.ShippedDate);
                    break;
                case "ShippedDate_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.ShippedDate);
                    break;
                case "DeliveredDate":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.DeliveredDate);
                    break;
                case "DeliveredDate_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.DeliveredDate);
                    break;
                case "RecipientName":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.RecipientName);
                    break;
                case "RecipientName_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.RecipientName);
                    break;
                case "Origin":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.Origin);
                    break;
                case "Origin_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.Origin);
                    break;
                case "Destination":
                    shipmentQuery = shipmentQuery.OrderBy(s => s.Destination);
                    break;
                case "Destination_desc":
                    shipmentQuery = shipmentQuery.OrderByDescending(s => s.Destination);
                    break;
                default:
                    shipmentQuery = shipmentQuery.OrderBy(s => s.WaybillId);
                    break;
            }

            //if (User.IsInRole("Customer"))
            //{
                // Add the condition to select a spefic shipping account if shipping account id is not null.
            if (ShippingAccountId != null)
            {
                shipmentQuery = shipmentQuery.Where(s => s.ShippingAccountId == ShippingAccountId);
                //shipmentSearch.Shipments = shipmentQuery.ToList();

                shipmentQuery = shipmentQuery.Where(s => (s.ShippedDate >= StartShippedDate && s.ShippedDate <= EndShippedDate));

            }
            /*else
            {
                // Return an empty result if no shipping account id has been selected.
                //shipmentSearch.Shipments = new ShipmentsListViewModel[0];
                shipmentQuery = shipmentQuery.Where(s => s.ShippingAccountId == 0); ;
            }*/
            //}
            shipmentSearch.Shipments = shipmentQuery.ToPagedList(pageNumber, pageSize);
            return View(shipmentSearch);
        }
        /*
        private SelectList PopulateShippingAccountsDropdownList()
        {
            // TODO: Construct the LINQ query to retrieve the unique list of shipping account ids.
            var shippingAccountQuery = db.Shipments.Select(s => s.ShippingAccountId).Distinct().OrderBy(c => c);
            return new SelectList(shippingAccountQuery);
        }
        */

        // GET: Shipments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WaybillId,ReferenceNumber,ServiceType,ShippedDate,DeliveredDate,RecipientName,NumberOfPackages,Origin,Destination,Status,ShippingAccountId")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WaybillId,ReferenceNumber,ServiceType,ShippedDate,DeliveredDate,RecipientName,NumberOfPackages,Origin,Destination,Status,ShippingAccountId")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            db.Shipments.Remove(shipment);
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
