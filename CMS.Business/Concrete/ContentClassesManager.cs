using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ContentClassesManager : IContentClassesService
    {
        public IContentClassesDal _contentClassesDal;
        public ContentClassesManager(IContentClassesDal contentClassesDal)
        {
            _contentClassesDal = contentClassesDal;
        }

        public void Add(ContentClasses contentClasses)
        {
            _contentClassesDal.Add(contentClasses);
        }

        public void Delete(int contentID)
        {
            _contentClassesDal.Delete(new ContentClasses { ContentID = contentID });
        }

        public ContentClasses Get(Expression<Func<ContentClasses, bool>> filter)
        {
            return _contentClassesDal.Get(filter);
        }

        public List<ContentClasses> GetAll()
        {
            return _contentClassesDal.GetList();
        }

        public ContentClasses GetById(int id)
        {
            return _contentClassesDal.Get(c => c.ContentID == id);
        }

        public void Update(ContentClasses contentClasses)
        {
            _contentClassesDal.Update(contentClasses);
        }
    }
}
