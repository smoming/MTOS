using MTOS.Extend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTOS.Models
{
    public class Shared
    {
        static private MTDBEntities GetEntity()
        {
            return new MTDBEntities();
        }

        public static List<SelectListItem> ProductList()
        {
            using (var r = GetEntity())
            {
                return r.PRODUCT.AsNoTracking()
                    .Select(s => new SelectListItem() { Value = s.ID, Text = s.NAME })
                    .ToList();
            }
        }

        public static List<SelectListItem> DocTypeList()
        {
            return Enum.GetValues(typeof(DocType)).Cast<DocType>()
                .Select(s => new SelectListItem() { Value = s.ToString(), Text = s.GetDescription() })
                .ToList();
        }
    }
}