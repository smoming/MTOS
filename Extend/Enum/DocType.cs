using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MTOS.Extend
{
    public enum DocType
    {
        [Description("研究報告")]
        research,
        [Description("使用個案")]
        usecases
    }
}