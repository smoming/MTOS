using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ProductController : BaseController
    {
        private MTService _Service;

        public ProductController()
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
            return PartialView(_Service.LookupPRODUCT());
        }

        public ActionResult Get(string xID)
        {
            return PartialView(_Service.GetPRODUCT(xID));
        }

        public ActionResult Create()
        {
            return PartialView(new PRODUCT());
        }

        public ActionResult Edit(string xID)
        {
            return PartialView(_Service.GetPRODUCT(xID));
        }

        async public Task<ActionResult> Add(PRODUCT item)
        {
            return Content(await _Service.AddPRODUCT(item));
        }

        async public Task<ActionResult> Update(PRODUCT item)
        {
            return Content(await _Service.UpdatePRODUCT(item));
        }

        async public Task<ActionResult> Delete(string xID)
        {
            return Content(await _Service.DeletePRODUCT(_Service.GetPRODUCT(xID)));
        }
    }
}