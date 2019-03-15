using CMS.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS.UI.Areas.Admin.Models.MenusVM
{

    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParenetId { get; set; }
    }

    public class MenuRecursion
    {
        private IMenusService _menusService;
        private IMenuInfoService _menuInfoService;

        List<Menu> allMenuItems;
        public const string OPEN_LIST_TAG = @"<ul class=""checktree"">";
        public const string CLOSE_LIST_TAG = "</ul>";
        public const string OPEN_LIST_ITEM_TAG = @"<li style=""display:block;"">";
        public const string CLOSE_LIST_ITEM_TAG = "</li>";
        public const string OPEN_LIST_ITEM_TAG_1 = "<li>";
        public const string CLOSE_LIST_ITEM_TAG_1 = "</li>";
        public const string OPEN_LIST_TAG_1 = "<ul>";
        public const string CLOSE_LIST_TAG_1 = "</ul>";
        public const string lblOpen = "<label>";
        public const string lblClose = "</label>";

        public MenuRecursion(IMenusService menusService, IMenuInfoService menuInfoService)
        {
            _menusService = menusService;
            _menuInfoService = menuInfoService;
            allMenuItems = GetMenuItems();
        }
        public List<Menu> GetMenuItems()
        {
            var menus = _menusService.GetAll();
            var menuInfo = _menuInfoService.GetAll();

            var model = (from m in menus
                         join mI in menuInfo on m.MenuID equals mI.MenuID
                         select new Menu
                         {
                             Id = m.MenuID,
                             Name = mI.Menu,
                             ParenetId = m.ParentID
                         }
                          ).ToList();

            List<Menu> MenuItmes = new List<Menu>();
            for (int i = 0; i < model.Count; i++)
            {
                var item = new Menu { Id = model[i].Id, Name = model[i].Name, ParenetId = model[i].ParenetId };
                MenuItmes.Add(item);
            }
            return MenuItmes;

        }

        public string GenerateMenuUi(List<int> ids)
        {
            var strBuilder = new StringBuilder();
            List<Menu> parentItems = (from a in allMenuItems where a.ParenetId == 0 select a).ToList();
            strBuilder.Append(OPEN_LIST_TAG);
            strBuilder.Append(OPEN_LIST_ITEM_TAG);
            strBuilder.Append(OPEN_LIST_TAG_1);

            string chkTrue = "checked";
            foreach (var parentcat in parentItems)
            {
                string chkBoxMenu = null;
                bool chk = false;
                if (ids != null)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        if (parentcat.Id.ToString().Contains(Convert.ToString(ids[i])))
                            chk = true;
                    }
                }


                if (chk)
                    chkBoxMenu = @"<input name='ids' value='" + parentcat.Id + "' type='checkbox' " + chkTrue + " />";
                else
                    chkBoxMenu = @"<input name='ids' value='" + parentcat.Id + "' type='checkbox' />";

                strBuilder.Append(OPEN_LIST_ITEM_TAG_1);
                strBuilder.Append(chkBoxMenu);
                strBuilder.Append(lblOpen);
                strBuilder.Append(parentcat.Name);
                strBuilder.Append(lblClose);

                List<Menu> childItems = (from a in allMenuItems where a.ParenetId == parentcat.Id select a).ToList();
                if (childItems.Count > 0)
                    AddChildItem(parentcat, strBuilder, ids);
                strBuilder.Append(CLOSE_LIST_ITEM_TAG_1);
            }
            strBuilder.Append(CLOSE_LIST_TAG_1);
            strBuilder.Append(CLOSE_LIST_ITEM_TAG);
            strBuilder.Append(CLOSE_LIST_TAG);
            return strBuilder.ToString();
        }

        private void AddChildItem(Menu childItem, StringBuilder strBuilder, List<int> ids)
        {
            strBuilder.Append(OPEN_LIST_TAG_1);
            List<Menu> childItems = (from a in allMenuItems where a.ParenetId == childItem.Id select a).ToList();

            string chkTrue = "checked";
            foreach (Menu cItem in childItems)
            {

                string chkBoxMenu = null;
                bool chk = false;
                if (ids != null)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        if (cItem.Id.ToString().Contains(Convert.ToString(ids[i])))
                            chk = true;
                    }
                }

                if (chk)
                    chkBoxMenu = @"<input name='ids' value='" + cItem.Id + "' type='checkbox' " + chkTrue + " />";
                else
                    chkBoxMenu = @"<input name='ids' value='" + cItem.Id + "' type='checkbox' />";

                strBuilder.Append(OPEN_LIST_ITEM_TAG_1);
                strBuilder.Append(chkBoxMenu);
                strBuilder.Append(lblOpen);
                strBuilder.Append(cItem.Name);
                strBuilder.Append(lblClose);
                List<Menu> subChilds = (from a in allMenuItems where a.ParenetId == cItem.Id select a).ToList();
                if (subChilds.Count > 0)
                {
                    AddChildItem(cItem, strBuilder, ids);
                }
                strBuilder.Append(CLOSE_LIST_ITEM_TAG_1);
            }
            strBuilder.Append(CLOSE_LIST_TAG_1);
        }
    }
}
