using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class TimeLessTomatoController : Controller
    {
        private MTService _Service;

        public TimeLessTomatoController()
            : base()
        {
            _Service = new MTService();
        }
        public ActionResult Index()
        {
            ViewBag.SERIES = "TIMELESS";
            return View(_Service.LookupPRODUCT(ViewBag.SERIES));
        }
    }
}