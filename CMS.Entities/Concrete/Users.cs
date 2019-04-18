using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("Users")]
    [FluentValidation.Attributes.Validator(typeof(UsersValidator))]
    public class Users : IEntity
    {
        [Key]
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "E-Posta")]
        public string EMail { get; set; }
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
