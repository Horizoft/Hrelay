﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Horizoft.Relay.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TemperatureDaily1()
        {
            return View();
        }

        public ActionResult TemperatureDaily()
        {
            return View();
        }
    }
}