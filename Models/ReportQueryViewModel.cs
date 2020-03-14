﻿using System;
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

    public class UploadPicViewModel
    {
        public string Name { get; set; }
    }

    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}