﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreightTech.App.Controllers
{
    public class RequestController : Controller
    {
        // GET: Customer
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}