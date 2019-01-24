using CMS.UI.Infrastructure;
using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            //değer tipli değişkenler için required özellğini zorunlu halden çıkaralım.
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            //Tanımlı olan tüm model validatorları devre dışı bıraklım.
            ModelValidatorProviders.Providers.Clear();

            //FluentValidation'ı projeye dahil edelim.
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider
            {
                //değer tipli değişkenler için required özellğini zorunlu halden çıkaralım.
                AddImplicitRequiredValidator = false
            });
        }
    }
}
