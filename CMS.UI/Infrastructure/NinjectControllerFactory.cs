using CMS.Business.Abstract;
using CMS.Business.Concrete;
using CMS.DataAccess.Abstract;
using CMS.DataAccess.Concrete.EntityFramework;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;


namespace CMS.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();

            #region ClassTypes and Classes Info
            //Classes
            _kernel.Bind<IClassesService>().To<ClassesManager>();
            _kernel.Bind<IClassesDal>().To<EfClassesDal>();
            _kernel.Bind<IClassInfoService>().To<ClassInfoManager>();
            _kernel.Bind<IClassInfoDal>().To<EfClassInfoDal>();
            //ClassTypes
            _kernel.Bind<IClassTypeInfoService>().To<ClassTypeInfoManager>();
            _kernel.Bind<IClassTypeInfoDal>().To<EfClassTypeInfoDal>();
            //Banners
            _kernel.Bind<IBannerService>().To<BannerManager>();
            _kernel.Bind<IBannersDal>().To<EfBannersDal>();
            _kernel.Bind<IBannerInfoService>().To<BannerInfoManager>();
            _kernel.Bind<IBannerInfoDal>().To<EfBannerInfoDal>();
            //Lists
            _kernel.Bind<IListsService>().To<ListsManager>();
            _kernel.Bind<IListsDal>().To<EfListsDal>();
            _kernel.Bind<IListInfoService>().To<ListInfoManager>();
            _kernel.Bind<IListInfoDal>().To<EfListInfoDal>();
            //Menus
            _kernel.Bind<IMenusService>().To<MenuManager>();
            _kernel.Bind<IMenusDal>().To<EfMenusDal>();
            _kernel.Bind<IMenuInfoService>().To<MenuInfoManager>();
            _kernel.Bind<IMenuInfoDal>().To<EfMenuInfoDal>();
            //ContentClasses
            _kernel.Bind<IContentClassesService>().To<ContentClassesManager>();
            _kernel.Bind<IContentClassesDal>().To<EfContentClassesDal>();
            //ContentImages
            _kernel.Bind<IContentImagesService>().To<ContentImagesManager>();
            _kernel.Bind<IContentImagesDal>().To<EfContentImagesDal>();
            //ContentInfo
            _kernel.Bind<IContentInfoService>().To<ContentInfoManager>();
            _kernel.Bind<IContentInfoDal>().To<EfContentInfoDal>();
            //Contents
            _kernel.Bind<IContentsService>().To<ContentsManager>();
            _kernel.Bind<IContentsDal>().To<EfContentsDal>();

            #endregion


        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}