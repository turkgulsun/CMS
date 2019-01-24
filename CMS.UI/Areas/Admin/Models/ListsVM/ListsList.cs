using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.ListsVM
{
    public class ListsList
    {

        public int ListID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        //public Lists Lists { get; set; }
        //public ListInfo ListInfo { get; set; }
    }
}