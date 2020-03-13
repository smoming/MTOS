using MTOS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class HomeEditController : BaseController
    {
        private List<SelectListItem> PicList = new List<SelectListItem>() {
                new SelectListItem() { Value = "CONTACT.jpg", Text = "CONTACT"},
                new SelectListItem() { Value = "RESEARCHUSE.jpg", Text = "RESEARCHUSE"},
                new SelectListItem() { Value = "TIMELESS.jpg", Text = "TIMELESS"},
                new SelectListItem() { Value = "WOUNDCARE.jpg", Text = "WOUNDCARE"}
            };
        private List<string> _AllowedExtextsion =
            Extensions.GetAppSetting("ProdPicType").Split(',')
            .Select(s => string.Concat(".", s))
            .ToList();

        // GET: HomeEdit
        public ActionResult Index(string xNAME = "")
        {
            ViewBag.HomePic = PicList;
            var item = new UploadPicViewModel() { Name = PicList.First().Value };
            if (xNAME.IsNotNullOrEmpty())
            {
                item.Name = PicList.SingleOrDefault(w => w.Value == xNAME).Value;
            }
            return View(item);
        }

        public ActionResult Update(string Name, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    if (_AllowedExtextsion.Contains(uploadfile.GetFileExtension().ToLower()))
                    {
                        string fullname = Path.Combine(Server.MapPath(Extensions.GetAppSetting("HomePicSavePath")), Name);
                        if (System.IO.File.Exists(fullname))
                        {
                            System.IO.File.Delete(fullname);
                        }
                        uploadfile.SaveAs(fullname);
                        TempData["message"] = "上傳成功";
                    }
                    else
                    {
                        TempData["message"] = string.Concat("請選擇圖片檔案. ex: ", string.Join(", ", _AllowedExtextsion));
                    }
                }
                else
                {
                    TempData["message"] = string.Concat("請選擇圖片檔案.");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
            }

            return RedirectToAction("Index", new { xNAME = Name });
        }
    }
}