using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.ListsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.MenusVM
{
    public class MenusVM
    {
        public Menus Menus { get; set; }
        public MenuInfo MenuInfo { get; set; }
        public List<ListsList> ListType { get; set; }
    }
}