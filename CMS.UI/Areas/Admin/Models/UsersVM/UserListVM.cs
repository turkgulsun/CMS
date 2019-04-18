using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.UsersVM
{
    public class UserListVM
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "E-Posta")]
        public string EMail { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreationDate { get; set; }
    }
}