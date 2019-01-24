using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("Menus")]
    [FluentValidation.Attributes.Validator(typeof(MenusValidator))]
    public class Menus : IEntity
    {
        [Key]
        public int MenuID { get; set; }
        public int MenuListID { get; set; }
        public int ParentID { get; set; }
        public string Target { get; set; }
        [Display(Name ="Resim")]
        public string Image { get; set; }
        public int PageID { get; set; }
        [Display(Name = "Sıra")]
        public int Sort { get; set; }
        [Display(Name = "Yayın")]
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }


    }
}
