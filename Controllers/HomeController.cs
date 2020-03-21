using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class HomeController : BaseController
    {
        private MTService _Service;

        public HomeController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index()
        {
            return View(_Service.GetHOMEEDIT());
        }
    }
}