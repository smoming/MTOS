using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTOS.Models
{
    public class ReportQueryViewModel
    {
        public DateTime? TradeDate_S { get; set; }
        public DateTime? TradeDate_E { get; set; }
        public string Series { get; set; }
        public string ProductID { get; set; }
        public string DocumentType { get; set; }
    }

    public class HomeEditViewModel
    {
        public string Name { get; set; }
        public string Title1 { get; set; }
        public string Content1 { get; set; }
        public string Link1 { get; set; }
        public string Title2 { get; set; }
        public string Content2 { get; set; }
        public string Link2 { get; set; }
        public string Title3 { get; set; }
        public string Content3 { get; set; }
        public string Link3 { get; set; }
    }

    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}