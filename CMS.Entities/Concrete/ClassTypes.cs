using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMS.Entities.Concrete
{
    public class ClassTypes : IEntity
    {
        [Key]
        public int ClassTypeID { get; set; }
        public int ModelType { get; set; }

    }
}
