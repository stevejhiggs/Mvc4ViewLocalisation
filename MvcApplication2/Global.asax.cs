using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;
using System.Globalization;

namespace MvcApplication2
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //replace standard razor view engine with the localisable one
            ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<RazorViewEngine>().First());
            ViewEngines.Engines.Add(new LocalisedRazorViewEngine());

            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("it");
        }
    }
}