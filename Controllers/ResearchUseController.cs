using MTOS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public JsonResult listProduct(string xSERIES)
        {
            return Json(new { data = _Service.LookupPRODUCT(xSERIES) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(new ReportQueryViewModel()
            {
                TradeDate_S = DateTime.Today.AddMonths(-1),
                TradeDate_E = DateTime.Today
            });
        }

        public ActionResult Grid(ReportQueryViewModel filter)
        {
            return PartialView(_Service.LookupPRODUCT_DOCUMENT(filter));
        }

        public ActionResult Get(string xGUID)
        {
            return PartialView(_Service.GetPRODUCT_DOCUMENT(xGUID));
        }

        public ActionResult Create()
        {
            return PartialView(new PRODUCT_DOCUMENT()
            {
                GUID = Guid.NewGuid().ToString(),
                SERIES = _Service.LookupSERIES().First().ID,
                REPORT_DATE = DateTime.Today,
                MODIFY_DATE = DateTime.Today
            });
        }

        public ActionResult Edit(string xGUID)
        {
            return PartialView(_Service.GetPRODUCT_DOCUMENT(xGUID));
        }

        [HttpPost]
        async public Task<ActionResult> Add(PRODUCT_DOCUMENT item, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    item.EXTENSION = uploadfile.GetFileExtension();
                    item.MODIFY_DATE = DateTime.Now;
                    DoUploadFile(uploadfile, item.GUID);
                    TempData["message"] = await _Service.AddPRODUCT_DOCUMENT(item);
                }
                else
                {
                    TempData["message"] = "請選擇上傳檔案";
                }
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        async public Task<ActionResult> Update(PRODUCT_DOCUMENT item, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    DoRemoveFile(item.GUID, item.EXTENSION); //刪除舊的文件
                    item.EXTENSION = uploadfile.GetFileExtension();
                    DoUploadFile(uploadfile, item.GUID); //新增新的文件
                }

                item.MODIFY_DATE = DateTime.Now;
                TempData["message"] = await _Service.UpdatePRODUCT_DOCUMENT(item);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        async public Task<ActionResult> Delete(string xGUID)
        {
            PRODUCT_DOCUMENT item;
            try
            {
                item = _Service.GetPRODUCT_DOCUMENT(xGUID);
                DoRemoveFile(xGUID, item.EXTENSION);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return Content(await _Service.DeletePRODUCT_DOCUMENT(item));
        }

        public ActionResult DownloadFile(string xGUID)
        {
            PRODUCT_DOCUMENT item = _Service.GetPRODUCT_DOCUMENT(xGUID);
            return DoDownloadFile(item.GUID, item.EXTENSION, string.Concat(item.DOCUMENT_NAME, item.EXTENSION));
        }
    }
}