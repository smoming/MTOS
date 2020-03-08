using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class WoundCareController : Controller
    {
        private MTService _Service;

        public WoundCareController()
            : base()
        {
            _Service = new MTService();
        }
        public ActionResult Index()
        {
            ViewBag.SERIES = "WOUNDCARE";
            return View(_Service.LookupPRODUCT(ViewBag.SERIES));
        }
    }
}