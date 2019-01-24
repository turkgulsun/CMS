using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table("ListInfo")]
    public class ListInfo:IEntity
    {
        [Key]
        public int ListInfoID { get; set; }
        public int ListID { get; set; }
        public int LanguageID { get; set; }
        public string Value { get; set; }
    }
}
