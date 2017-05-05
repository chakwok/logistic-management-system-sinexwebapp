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
using System.Collections;

namespace SinExWebApp20265462.Controllers
{
    public class PickupsController : BaseController
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
            PickupViewModel pickupViewModel = new PickupViewModel();
            pickupViewModel.Pickup = new Pickup();

            pickupViewModel.PickUpLocations = PopulatePickUpLocationsDropDownList().ToList();
            if (pickupViewModel.PickUpLocations.Count > 0) ViewBag.ShowPickupLocation = 1;
            else ViewBag.ShowPickupLocation = 0;
            pickupViewModel.Cities = PopulateDestinationsDropDownList().ToList();

            int currentUserId = GetUserId();

            pickupViewModel.ShipmentCheckBoxes = new List<CheckBox>();
            var myShipmentList = new List<Shipment>(db.Shipments.Where(s => s.ShippingAccountId == currentUserId && s.Status == "Pending").ToList());
            for (int i = 0; i < myShipmentList.Count; i++)
            {
                int waybillId = myShipmentList[i].WaybillId;
                pickupViewModel.ShipmentCheckBoxes.Add(new CheckBox(waybillId, false));
            }


            return View(pickupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArrangePickup(PickupViewModel pickupViewModel, bool next =false)
        {
            if (!next)  // Submit Pickuplocation
            {

                PickUpLocation pickupLocation = db.PickUpLocations.Find(pickupViewModel.PickUpLocationID);
                ViewBag.PickUpBuilding = pickupLocation.PickUpBuilding;
                ViewBag.PickUpStreet = pickupLocation.PickUpStreet;
                ViewBag.PickUpCity = pickupLocation.PickUpCity;
                ViewBag.PickUpPostalCode = pickupLocation.PickUpPostalCode;

                pickupViewModel.PickUpLocations = PopulatePickUpLocationsDropDownList().ToList();
                if (pickupViewModel.PickUpLocations.Count > 0) ViewBag.ShowPickupLocation = 1;
                else ViewBag.ShowPickupLocation = 0;
                pickupViewModel.Cities = PopulateDestinationsDropDownList().ToList();

                return View(pickupViewModel);
            }


            if (Session["newPickupView"] != null) Session.Remove("newPickupView");
            SaveToSessionState("newPickupView", pickupViewModel);
            if (Session["newPickup"] != null) Session.Remove("newPickup");
            SaveToSessionState("newPickup", pickupViewModel.Pickup);

            if (pickupViewModel.Pickup.Type == "Prearranged")
            {
                return RedirectToAction("ArrangePrearrangedPickup");
            } else if (pickupViewModel.Pickup.Type == "Immediate")
            {
                return RedirectToAction("ConfirmPickup");
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Pickups/ArrangePrearrangedPickup
        public ActionResult ArrangePrearrangedPickup()
        {
            PickupViewModel pickupViewModel = (PickupViewModel)Session["newPickupView"];
            pickupViewModel.Pickup = (Pickup)Session["newPickup"];

            return View(pickupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArrangePrearrangedPickup(int? dummy, PickupViewModel pickupViewModel)
        {
            DateTime pickupDateTime;

            try
            {
                pickupDateTime = new DateTime(pickupViewModel.PickupYear, pickupViewModel.PickupMonth, pickupViewModel.PickupDay, pickupViewModel.PickupHour, pickupViewModel.PickupMinute, 0);
            } catch
            {
                ViewBag.PickupDateTimeStatusMessage = "Please enter a valid date.";
                return View(pickupViewModel);
            }

            DateTime fiveDaysLater = DateTime.Now.AddDays(5);
            if (pickupDateTime < DateTime.Now || pickupDateTime > fiveDaysLater)
            {
                ViewBag.PickupDateTimeStatusMessage = "Prearranged pickup is only available 5 days in advance.";
                return View(pickupViewModel);
            }

            Pickup pickup = (Pickup)Session["newPickup"];
            pickup.PickupDateTime = new DateTime(pickupViewModel.PickupYear, pickupViewModel.PickupMonth, pickupViewModel.PickupDay, pickupViewModel.PickupHour, pickupViewModel.PickupMinute, 0);


            if (Session["newPickupView"] != null) Session.Remove("newPickupView");
            SaveToSessionState("newPickupView", pickupViewModel);
            if (Session["newPickup"] != null) Session.Remove("newPickup");
            SaveToSessionState("newPickup", pickup);

            return RedirectToAction("ConfirmPickup");
        }

        // GET: Pickups/ConfirmPickup
        public ActionResult ConfirmPickup()
        {
            PickupViewModel pickupViewModel = (PickupViewModel)Session["newPickupView"];
            pickupViewModel.Pickup = (Pickup)Session["newPickup"];

            return View(pickupViewModel);
        }

        // POST: Pickups/ConfirmPickup
        [HttpPost]
        public ActionResult ConfirmPickup(int? dummy)
        {
            PickupViewModel pickupViewModel = (PickupViewModel)Session["newPickupView"];
            Pickup pickup = (Pickup)Session["newPickup"];
            pickup.PickupId = db.Pickups.Count() + 1;
            pickup.ProvinceCode = GetProvince(pickup.City);
            if (pickup.PickupDateTime == DateTime.MinValue) pickup.PickupDateTime = new DateTime(1990, 1, 1);

            foreach (var item in pickupViewModel.ShipmentCheckBoxes)
            {
                if (item.Checked == true)
                {
                    Shipment shipment = db.Shipments.Find(item.Value);
                    shipment.Pickup = pickup.PickupId;
                    shipment.Status = "Confirmed";
                    db.Entry<Shipment>(shipment).State = EntityState.Modified;

                }
            }

            db.Pickups.Add(pickup);
            db.SaveChanges();

            if (Session["newPickupView"] != null) Session.Remove("newPickupView");
            if (Session["newPickup"] != null) Session.Remove("newPickup");
            return RedirectToAction("Index");
        }

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
