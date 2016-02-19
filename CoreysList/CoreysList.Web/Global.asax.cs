using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoreysList.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            HttpException httpException = exception as HttpException;
            string file_name = System.Web.HttpContext.Current.Server.MapPath(@"~/Logs/errors.txt");

            System.IO.StreamWriter errorWriter;
            errorWriter = new System.IO.StreamWriter(file_name);

            errorWriter.Write(exception.Message);
            errorWriter.Close();
        }
    }
}