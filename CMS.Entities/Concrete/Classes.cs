using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CMS.Entities.Concrete
{
    [Table("Classes")]
    [FluentValidation.Attributes.Validator(typeof(ClassesValidator))]
    public class Classes : IEntity
    {
        [Key]
        public int ClassID { get; set; }
        public int ClassTypeID { get; set; }
        public int ParentID { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Aktif")]
        public bool Active { get; set; }
        [Display(Name = "Sıra")]
        public int Sort { get; set; }
        public DateTime CreationDate { get; set; }
    }
}