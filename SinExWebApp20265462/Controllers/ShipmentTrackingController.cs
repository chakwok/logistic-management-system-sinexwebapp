using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SinExWebApp20265462.Controllers
{
    public class ShipmentTrackingController : Controller
    {
        // GET: ShipmentTracking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id) {
            ViewBag.waybill = id;
            return View();
        }
    }
}