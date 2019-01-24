using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ContentImagesManager : IContentImagesService
    {
        public IContentImagesDal _contentImagesDal;
        public ContentImagesManager(IContentImagesDal contentImagesDal)
        {
            _contentImagesDal = contentImagesDal;
        }

        public void Add(ContentImages contentImages)
        {
            _contentImagesDal.Add(contentImages);
        }

        public void Delete(int contentImageID)
        {
            _contentImagesDal.Delete(new ContentImages { ContentImageID = contentImageID });
        }

        public ContentImages Get(Expression<Func<ContentImages, bool>> filter)
        {
            return _contentImagesDal.Get(filter);
        }

        public List<ContentImages> GetAll()
        {
            return _contentImagesDal.GetList();
        }

        public ContentImages GetById(int id)
        {
            return _contentImagesDal.Get(x => x.ContentImageID == id);
        }

        public void Update(ContentImages contentImages)
        {
            _contentImagesDal.Update(contentImages);
        }
    }
}
