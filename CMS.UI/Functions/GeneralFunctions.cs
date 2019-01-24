using CMS.Business.Abstract;
using CMS.UI.Areas.Admin.Models.ListsVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CMS.UI.Functions
{
    public class GeneralFunctions
    {
        public void CreateDirectory(string dirPath, string dirName)
        {
            if (Directory.Exists(dirPath))
            {
                try
                {
                    Directory.CreateDirectory(dirPath + dirName);
                }
                catch
                {
                }
            }
        }

        private IListsService _listsService;
        private IListInfoService _listInfoService;

        public GeneralFunctions(IListInfoService listInfoService, IListsService listsService)
        {
            _listsService = listsService;
            _listInfoService = listInfoService;
        }

        public List<ListsList> GetListType(string type)
        {

            var _lists = _listsService.GetAll();
            var _listInfo = _listInfoService.GetAll();

            var listTypes = (from l in _lists
                             join li in _listInfo on l.ListID equals li.ListID
                             where l.Type == type
                             select new ListsList { ListID = l.ListID, Type = l.Type, Value = li.Value });

            return listTypes.ToList();

        }
    }


}