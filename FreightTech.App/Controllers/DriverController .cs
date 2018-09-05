using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreightTech.App.Controllers
{
        [Authorize]
    public class DriverController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Track(int id)
        {
            ViewBag.DriverId = id;
            return View();
        }
    }
}