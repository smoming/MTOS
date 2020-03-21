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
    public class HomeEditController : BaseController
    {
        private readonly string _PATH = "HomePicSavePath";
        private List<string> _AllowedExtextsion =
            Extensions.GetAppSetting("ProdPicType").Split(',')
            .Select(s => string.Concat(".", s))
            .ToList();

        private MTService _Service;

        public HomeEditController()
            : base()
        {
            _Service = new MTService();
        }

        public ActionResult Index(string xNAME = "")
        {
            string path = Server.MapPath(Extensions.GetAppSetting(_PATH));
            var PicList = Directory.GetFiles(path)
                .Select(s => s.Replace(path, ""))
                .Select(s => new SelectListItem() { Value = s, Text = s });
            ViewBag.HomePic = PicList;
            return View(ToView(xNAME.IsNotNullOrEmpty() ?
                PicList.SingleOrDefault(w => w.Value == xNAME).Value :
                PicList.First().Value));
        }

        async public Task<ActionResult> Update(HomeEditViewModel item, HttpPostedFileBase uploadfile)
        {
            try
            {
                if (uploadfile.IsNotNull())
                {
                    if (_AllowedExtextsion.Contains(uploadfile.GetFileExtension().ToLower()))
                    {
                        string fullname = Path.Combine(Server.MapPath(Extensions.GetAppSetting(_PATH)), item.Name);
                        if (System.IO.File.Exists(fullname))
                        {
                            System.IO.File.Delete(fullname);
                        }
                        uploadfile.SaveAs(fullname);
                    }
                    else
                    {
                        TempData["message"] = string.Concat("請選擇圖片檔案. ex: ", string.Join(", ", _AllowedExtextsion));
                    }
                }
                TempData["message"] = await _Service.UpdateHOMEEDIT(ToHOMEEDIT(item));
            }
            catch (Exception e)
            {
                TempData["message"] = e.Message;
            }

            return RedirectToAction("Index", new { xNAME = item.Name });
        }

        private HOMEEDIT ToHOMEEDIT(HomeEditViewModel v)
        {
            var item = _Service.GetHOMEEDIT();
            item.TITLE1 = v.Title1;
            item.CONTENT1 = v.Content1;
            item.LINK1 = v.Link1;

            item.TITLE2 = v.Title2;
            item.CONTENT2 = v.Content2;
            item.LINK2 = v.Link2;

            item.TITLE3 = v.Title3;
            item.CONTENT3 = v.Content3;
            item.LINK3 = v.Link3;

            return item;
        }

        private HomeEditViewModel ToView(string xNAME)
        {
            var item = new HomeEditViewModel();
            var one = _Service.GetHOMEEDIT();

            item.Name = xNAME;
            item.Title1 = one.TITLE1;
            item.Content1 = one.CONTENT1;
            item.Link1 = one.LINK1;

            item.Title2 = one.TITLE2;
            item.Content2 = one.CONTENT2;
            item.Link2 = one.LINK2;

            item.Title3 = one.TITLE3;
            item.Content3 = one.CONTENT3;
            item.Link3 = one.LINK3;


            return item;
        }
    }
}