using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    abstract public class BaseController : Controller
    {
        [HttpPost]
        public JsonResult UploadByAjax()
        {
            //取得目前 HTTP 要求的 HttpRequestBase 物件
            string savePath = Server.MapPath(Extensions.GetAppSetting("DocSavePath"));
            if (Directory.Exists(savePath).Not())
            {
                Directory.CreateDirectory(savePath);
            }
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent.IsNotNull() && fileContent.ContentLength > 0)
                {
                    // 取得的檔案是stream
                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file);
                    var path = Path.Combine(savePath, fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            }

            return new JsonResult
            {
                ContentEncoding = Encoding.UTF8,
                ContentType = "text",
                Data = "success"
            };
        }

        protected void DoUploadFile(HttpPostedFileBase file, string filename)
        {
            string savePath = checkPath();
            if (file != null)
            {
                file.SaveAs(Path.Combine(savePath, string.Concat(filename, file.GetFileExtension())));
            }
        }
        protected void DoRemoveFile(string filename, string extension)
        {
            string savePath = checkPath();
            string fullname = Path.Combine(savePath, string.Concat(filename, extension));
            if (System.IO.File.Exists(fullname))
            {
                System.IO.File.Delete(fullname);
            }
        }

        protected FilePathResult DoDownloadFile(string filename, string extension, string downloadfilefullname)
        {
            string savePath = checkPath();
            string fullname = Path.Combine(savePath, string.Concat(filename, extension));
            return File(fullname, "application/octet-stream", downloadfilefullname);
        }

        private string checkPath()
        {
            string savePath = Server.MapPath(Extensions.GetAppSetting("DocSavePath"));
            if (Directory.Exists(savePath).Not())
            {
                Directory.CreateDirectory(savePath);
            }

            return savePath;
        }
    }
}