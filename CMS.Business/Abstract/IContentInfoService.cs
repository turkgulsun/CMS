using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IContentInfoService
    {
        List<ContentInfo> GetAll();
        ContentInfo Get(Expression<Func<ContentInfo, bool>> filter); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        ContentInfo GetById(int id);
        void Add(ContentInfo contentInfo);
        void Update(ContentInfo contentInfo);
        void Delete(int contentInfoID);
    }
}
