using CMS.Business.Abstract;
using CMS.Entities.Concrete;
using CMS.UI.Areas.Admin.Models.MenusVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.UI.Functions
{
    public class MenuFunction
    {

        public string ListContentsMenu(string resource, IMenusService menusService, IMenuInfoService menuInfoService)
        {
            string strHtml = "";
            int parentID = 0;

            var menus = menusService.GetAll();
            var menuInfo = menuInfoService.GetAll();

            var model = (from m in menus
                         join mI in menuInfo on m.MenuID equals mI.MenuID
                         where m.ParentID == parentID
                         select new MenuListVM
                         {
                             MenuID = m.MenuID,
                             Menu = mI.Menu
                         }
                           ).ToList();

            foreach (var item in model)
            {
                strHtml += "<li>";
                strHtml += string.Format(@"<input id= ""{0}"" type=""checkbox"" />< label for= ""vicepresident""> {1} </label>", item.MenuID, item.Menu);
                strHtml += "</li>";
            }



            //

            //<li>
            //    <input id="profil" type="checkbox" /><label for="vicepresident">Profil</label>
            //    <ul>
            //        <li><input id="hakkimizda" type="checkbox" /><label for="manager4">Hakkımızda</label></li>
            //        <li><input id="degerlerimiz" type="checkbox" /><label for="manager5">Değerlerimiz</label></li>
            //    </ul>
            //</li>



            return strHtml;
        }
    }
}