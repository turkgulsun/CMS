using CMS.Core.Entities;
using CMS.Entities.ValidationRules.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("UserTypes")]
    public class UserTypes : IEntity
    {
        [Key]
        public int UserTypeID { get; set; }
        public string Role { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
