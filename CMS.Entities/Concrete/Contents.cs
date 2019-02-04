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
        public int ClassID { get; set; }
        public int Review { get; set; }
        [Display(Name = "Sıra")]
        public int Sort { get; set; }
        public DateTime LastModifiedDate { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Yayın")]
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
