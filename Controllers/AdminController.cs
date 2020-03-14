using MTOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Controllers
{
    public class AdminController : Controller
    {
        private string[] _AccPw =
            Extensions.GetAppSetting("AccPw").Split(',');

        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        public ActionResult AdminList(LoginViewModel item)
        {
            string acc = _AccPw[0];
            string pwd = _AccPw[1];
            if (item.Name == acc && item.Password == pwd)
                return View();
            else
            {
                TempData["message"] = "輸入帳號或密碼不正確!";
                return RedirectToAction("Index");
            }
        }
    }
}