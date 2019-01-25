using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table(name: "ContentImages")]
    public class ContentImages : IEntity
    {
        [Key]
        public int ContentImageID { get; set; }
        public int ContentID { get; set; }
        public string Image { get; set; }
        public int Sort { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
