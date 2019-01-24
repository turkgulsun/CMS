using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.Linq.Expressions;
using CMS.DataAccess.Abstract;

namespace CMS.Business.Concrete
{
    public class ContentsManager : IContentsService
    {
        public IContentsDal _contentsDal;

        public ContentsManager(IContentsDal contentsDal)
        {
            _contentsDal = contentsDal;
        }
        public void Add(Contents contents)
        {
            _contentsDal.Add(contents);
        }

        public void Delete(int contentID)
        {
            _contentsDal.Delete(new Contents { ContentID = contentID });
        }

        public Contents Get(Expression<Func<Contents, bool>> filter)
        {
            return _contentsDal.Get(filter);
        }

        public List<Contents> GetAll()
        {
            return _contentsDal.GetList();
        }

        public Contents GetById(int id)
        {
            return _contentsDal.Get(x => x.ContentID == id);
        }

        public void Update(Contents contents)
        {
            _contentsDal.Update(contents);
        }
    }
}
