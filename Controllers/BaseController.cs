using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MTOS.Controllers
{
    abstract public class BaseController : Controller
    {
        [HttpPost]
        public JsonResult UploadByAjax()
        {
            //取得目前 HTTP 要求的 HttpRequestBase 物件
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    // 取得的檔案是stream
                    var stream = fileContent.InputStream;
                    var fileName = Path.GetFileName(file);
                    var path = Path.Combine(Server.MapPath("~/DocRepo/"), fileName);
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
    }
}