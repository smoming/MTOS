using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ProductRSController : Controller
    {
        private MTService _Service;

        public ProductRSController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index(string xSERIES, string xDOCTYPE)
        {
            return View(Tuple.Create(_Service.GetSERIES(xSERIES), xDOCTYPE, _Service.LookupPRODUCT_DOCUMENT(new ReportQueryViewModel()
            {
                Series = xSERIES,
                DocumentType = xDOCTYPE
            }).AsEnumerable()));
        }
    }
}