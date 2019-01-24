using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ContentInfoManager : IContentInfoService
    {
        public IContentInfoDal _contentInfoDal;

        public ContentInfoManager(IContentInfoDal contentInfoDal)
        {
            _contentInfoDal = contentInfoDal;
        }
        public void Add(ContentInfo contentInfo)
        {
            _contentInfoDal.Add(contentInfo);
        }

        public void Delete(int contentInfoID)
        {
            _contentInfoDal.Delete(new ContentInfo { ContentInfoID = contentInfoID });
        }

        public ContentInfo Get(Expression<Func<ContentInfo, bool>> filter)
        {
            return _contentInfoDal.Get(filter);
        }

        public List<ContentInfo> GetAll()
        {
            return _contentInfoDal.GetList();
        }

        public ContentInfo GetById(int id)
        {
            return _contentInfoDal.Get(x => x.ContentInfoID == id);
        }

        public void Update(ContentInfo contentInfo)
        {
            _contentInfoDal.Update(contentInfo);
        }
    }
}
