using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ResearchUseController : BaseController
    {
        private MTService _Service;

        public ResearchUseController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index()
        {
            return View(new ReportQueryViewModel() { TradeDate_S = DateTime.Today.AddDays(-7), TradeDate_E = DateTime.Today });
        }

        public ActionResult Grid(ReportQueryViewModel filter)
        {
            return PartialView(_Service.LookupPRODUCT_DOCUMENT(filter));
        }

        public ActionResult Get(string xGUID)
        {
            return PartialView(_Service.GetPRODUCT_DOCUMENT(xGUID));
        }

        public ActionResult Create(string xProductID)
        {
            return PartialView(new PRODUCT_DOCUMENT()
            {
                PRODUCT_ID = xProductID,
                REPORT_DATE = DateTime.Today,
                MODIFY_DATE = DateTime.Today
            });
        }

        public ActionResult Edit(string xGUID)
        {
            return PartialView(_Service.GetPRODUCT_DOCUMENT(xGUID));
        }

        async public Task<ActionResult> Add(PRODUCT_DOCUMENT item)
        {
            return Content(await _Service.AddPRODUCT_DOCUMENT(item));
        }

        async public Task<ActionResult> Update(PRODUCT_DOCUMENT item)
        {
            return Content(await _Service.UpdatePRODUCT_DOCUMENT(item));
        }

        async public Task<ActionResult> Delete(string xGUID)
        {
            return Content(await _Service.DeletePRODUCT_DOCUMENT(_Service.GetPRODUCT_DOCUMENT(xGUID)));
        }
    }
}