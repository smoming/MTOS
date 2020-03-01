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
            return View(_Service.LookupSERIES().FirstOrDefault());
        }

        public ActionResult Grid(string xSERIES)
        {
            return PartialView(_Service.LookupPRODUCT(xSERIES));
        }

        public ActionResult Get(string xSERIES, string xID)
        {
            return PartialView(_Service.GetPRODUCT(xSERIES, xID));
        }

        public ActionResult Create(string xSERIES)
        {
            return PartialView(new PRODUCT()
            {
                SERIES = xSERIES
            });
        }

        public ActionResult Edit(string xSERIES, string xID)
        {
            return PartialView(_Service.GetPRODUCT(xSERIES, xID));
        }

        async public Task<ActionResult> Add(PRODUCT item)
        {
            return Content(await _Service.AddPRODUCT(item));
        }

        async public Task<ActionResult> Update(PRODUCT item)
        {
            return Content(await _Service.UpdatePRODUCT(item));
        }

        async public Task<ActionResult> Delete(string xSERIES, string xID)
        {
            return Content(await _Service.DeletePRODUCT(_Service.GetPRODUCT(xSERIES, xID)));
        }
    }
}