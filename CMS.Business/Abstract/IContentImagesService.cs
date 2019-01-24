using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IContentImagesService
    {
        List<ContentImages> GetAll();
        ContentImages Get(Expression<Func<ContentImages, bool>> filter); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        ContentImages GetById(int id);
        void Add(ContentImages contentImages);
        void Update(ContentImages contentImages);
        void Delete(int contentImageID);
    }
}
