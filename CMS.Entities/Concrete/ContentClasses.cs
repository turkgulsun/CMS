using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{
    [Table(name: "ContentClasses")]
    public class ContentClasses : IEntity
    {
        public int ContentID { get; set; }
        public int ClassID { get; set; }
        public int MenuID { get; set; }
    }
}
