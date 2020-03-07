using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ContactController : Controller
    {
        private MTService _Service;

        public ContactController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return PartialView(_Service.LookupCONTACT_US().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new CONTACT_US());
        }

        public ActionResult Edit(string xGUID)
        {
            return PartialView(_Service.GetCONTACT_US(xGUID));
        }

        async public Task<ActionResult> Add(CONTACT_US item)
        {
            return Content(await _Service.AddCONTACT_US(item));
        }

        async public Task<ActionResult> Update(CONTACT_US item)
        {
            return Content(await _Service.UpdateCONTACT_US(item));
        }

        async public Task<ActionResult> Delete(string xGUID)
        {
            return Content(await _Service.DeleteCONTACT_US(_Service.GetCONTACT_US(xGUID)));
        }
    }
}