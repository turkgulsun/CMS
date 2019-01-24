using CMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Entities.Concrete
{

    //ClassTypeInfo nesnesi bir veri tabanı nesnesidir anlamına geliyor.

    [Table("ClassTypeInfo")]
    public class ClassTypeInfo : IEntity
    {
        [Key]
        public int ClassTypeInfoID { get; set; }
        public int ClassTypeID { get; set; }
        public int LanguageID { get; set; }
        public string ClassType { get; set; }

    }
}
