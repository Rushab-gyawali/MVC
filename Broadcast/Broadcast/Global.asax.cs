using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Boostrapper.Initiliase();
        }
        protected void Application_Error()
        {
            HttpException err = Server.GetLastError() as HttpException;
            var excep = Server.GetLastError().ToString();
            if (err != null)
            {
                var page = HttpContext.Current.Request.Url.ToString();
                //StaticData.LogError(err, page);
            }

            Server.ClearError();
        }
    }
}
