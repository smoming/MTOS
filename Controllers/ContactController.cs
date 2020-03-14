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
            if (item.LAST_NAME.IsNullOrEmpty())
                return Content("請輸入姓氏.");
            if (item.NAME.IsNullOrEmpty())
                return Content("請輸入名子.");
            if (item.EMAIL.IsNullOrEmpty())
                return Content("請輸入電子信箱.");
            if (item.SUBJECT.IsNullOrEmpty())
                return Content("請輸入主題.");
            if (item.MESSAGE.IsNullOrEmpty())
                return Content("請輸入訊息內容.");

            if(Extensions.IsValidEmail(item.EMAIL).Not())
                return Content("電子信箱不符合格式.");

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

        public ActionResult FeedBack()
        {
            ViewBag.companyinfo = _Service.LookupCOMPANY_BASIC_INFO().OrderBy(o => o.ID).AsEnumerable();
            return View(new CONTACT_US());
        }
    }
}