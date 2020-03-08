using MTOS.Extend;
using MTOS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class ProductController : BaseController
    {
        private MTService _Service;
        private List<string> _AllowedExtextsion =
            Extensions.GetAppSetting("ProdPicType").Split(',')
            .Select(s => string.Concat(".", s))
            .ToList();

        public ProductController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index(string xSERIES = null)
        {
            if (xSERIES.IsNotNull())
                return View(_Service.GetSERIES(xSERIES));

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

        async public Task<ActionResult> Add(PRODUCT item, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    if (_AllowedExtextsion.Contains(uploadfile.GetFileExtension().ToLower()))
                    {
                        item.EXTENSION = uploadfile.GetFileExtension();
                        DoUploadFile(UploadType.PROD_PIC, uploadfile, getProdPicFileName(item));
                        TempData["message"] = await _Service.AddPRODUCT(item);
                    }
                    else
                    {
                        TempData["message"] = string.Concat("請選擇圖片檔案. ex: ", string.Join(", ", _AllowedExtextsion));
                    }
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

            return RedirectToAction("Index", new { xSERIES = item.SERIES });
        }

        async public Task<ActionResult> Update(PRODUCT item, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    if (_AllowedExtextsion.Contains(uploadfile.GetFileExtension().ToLower()))
                    {
                        DoRemoveFile(UploadType.PROD_PIC, getProdPicFileName(item), item.EXTENSION); //刪除舊的文件
                        item.EXTENSION = uploadfile.GetFileExtension();
                        DoUploadFile(UploadType.PROD_PIC, uploadfile, getProdPicFileName(item)); //新增新的文件
                    }
                    else
                    {
                        TempData["message"] = string.Concat("請選擇圖片檔案. ex: ", string.Join(", ", _AllowedExtextsion));
                        return RedirectToAction("Index");
                    }
                }
                TempData["message"] = await _Service.UpdatePRODUCT(item);
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        async public Task<ActionResult> Delete(string xSERIES, string xID)
        {
            PRODUCT item;
            try
            {
                item = _Service.GetPRODUCT(xSERIES, xID);
                DoRemoveFile(UploadType.PROD_PIC, getProdPicFileName(item), item.EXTENSION);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return Content(await _Service.DeletePRODUCT(_Service.GetPRODUCT(xSERIES, xID)));
        }

        private string getProdPicFileName(PRODUCT item)
        {
            return string.Concat(item.SERIES, "_", item.ID);
        }

        public ActionResult Info(string xSERIES, string xID)
        {
            var doclist = _Service.LookupPRODUCT_DOCUMENT(new ReportQueryViewModel()
            {
                Series = xSERIES,
                ProductID = xID
            });

            return View(Tuple.Create(_Service.GetPRODUCT(xSERIES, xID),
                doclist.Where(w => w.DOCUMENT_TYPE == DocType.research.ToString()).OrderBy(o => o.REPORT_DATE).AsEnumerable(),
                doclist.Where(w => w.DOCUMENT_TYPE == DocType.usecases.ToString()).OrderBy(o => o.REPORT_DATE).AsEnumerable()));
        }
    }
}