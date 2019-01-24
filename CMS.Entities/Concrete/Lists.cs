using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("Lists")]
    public class Lists:IEntity
    {
        [Key]
        public int ListID { get; set; }
        public string Type { get; set; }
        public int Sort { get; set; }
    }
}
