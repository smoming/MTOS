using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ContactController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }
    }
}