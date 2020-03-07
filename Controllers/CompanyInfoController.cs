using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class CompanyInfoController : Controller
    {
        private MTService _Service;

        public CompanyInfoController()
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
            return PartialView(_Service.LookupCOMPANY_BASIC_INFO().ToList());
        }

        public ActionResult Create()
        {
            return PartialView(new COMPANY_BASIC_INFO());
        }

        public ActionResult Edit(string xInfoType)
        {
            return PartialView(_Service.GetCOMPANY_BASIC(xInfoType));
        }

        async public Task<ActionResult> Add(COMPANY_BASIC_INFO item)
        {
            return Content(await _Service.AddCOMPANY_BASIC(item));
        }

        async public Task<ActionResult> Update(COMPANY_BASIC_INFO item)
        {
            return Content(await _Service.UpdateCOMPANY_BASIC(item));
        }

        async public Task<ActionResult> Delete(string xInfoType)
        {
            return Content(await _Service.DeleteCOMPANY_BASIC(_Service.GetCOMPANY_BASIC(xInfoType)));
        }
    }
}