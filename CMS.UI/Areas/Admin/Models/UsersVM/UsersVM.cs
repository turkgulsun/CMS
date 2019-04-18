using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.UsersVM
{
    public class UsersVM
    {
        public Users User { get; set; }
        public List<UserTypes> UserTypes { get; set; }
    }
}