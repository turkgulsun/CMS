using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{

    [Table(name:"Contents")]
    public class Contents: IEntity
    {
        [Key]
        public int ContentID { get; set; }
        public int Review { get; set; }
        public int Sort { get; set; }
        public DateTime LastMotifiedDate { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
