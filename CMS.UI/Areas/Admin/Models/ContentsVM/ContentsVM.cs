using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.ContentsVM
{
    public class ContentsVM
    {
        public Contents Contents { get; set; }
        public ContentInfo ContentInfo { get; set; }
    }
}