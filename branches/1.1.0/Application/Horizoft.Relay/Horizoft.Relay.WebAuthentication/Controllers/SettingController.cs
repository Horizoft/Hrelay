using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Horizoft.Relay.Web.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET: WebRelay

        public ActionResult Monitor()
        {
            return View();
        }
        public ActionResult Mail()
        {
            return View();
        }

        public ActionResult Host()
        {
            return View();
        }

    }


}