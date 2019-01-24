using CMS.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CMS.Business.Abstract
{
    public interface IContentsService
    {

        List<Contents> GetAll();
        Contents Get(Expression<Func<Contents, bool>> filter); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        Contents GetById(int id);
        void Add(Contents contents);
        void Update(Contents contents);
        void Delete(int contentID);
    }
}
