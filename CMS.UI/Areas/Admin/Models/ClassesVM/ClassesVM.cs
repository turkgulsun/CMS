using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.ClassesVM
{
    public class ClassesVM
    {
        public Classes Classes { get; set; }
        public ClassInfo ClassInfo { get; set; }

    }
}