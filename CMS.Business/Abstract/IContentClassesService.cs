using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IContentClassesService
    {
        List<ContentClasses> GetAll();
        List<ContentClasses> GetAll(Expression<Func<ContentClasses, bool>> filter);
        ContentClasses Get(Expression<Func<ContentClasses, bool>> filter); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        ContentClasses GetById(int id);
        void Add(ContentClasses contentClasses);
        void Update(ContentClasses contentClasses);
        void Delete(int contentClassID);

    }
}
