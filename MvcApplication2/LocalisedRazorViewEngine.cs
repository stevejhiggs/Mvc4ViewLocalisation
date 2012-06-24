using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace MvcApplication2
{
    public class LocalisedRazorViewEngine : RazorViewEngine
    {
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string overrideViewName = GetPrependedCultureName(viewName, Thread.CurrentThread.CurrentUICulture);
            ViewEngineResult result = base.FindView(controllerContext, overrideViewName, masterName, useCache);

            // If we're looking for a cultured view and couldn't find it try again without modifying the viewname
            if (result == null || result.View == null)
            {
                result = base.FindView(controllerContext, viewName, masterName, useCache);
            }
            return result;
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            string overrideViewName = GetPrependedCultureName(partialViewName, Thread.CurrentThread.CurrentUICulture);

            ViewEngineResult result = base.FindPartialView(controllerContext, overrideViewName, useCache);

            // If we're looking for a cultured view and couldn't find it try again without modifying the viewname
            if (result == null || result.View == null)
            {
                result = base.FindPartialView(controllerContext, partialViewName, useCache);
            }
            return result;
        }

        private string GetPrependedCultureName(string viewName, CultureInfo culture)
        {
            string overrideViewName = "";
            if (viewName.Contains("/"))
            {
                overrideViewName = string.Format("{0}/{1}{2}", viewName.Substring(0, viewName.LastIndexOf("/")), culture.Name, viewName.Substring(viewName.LastIndexOf("/")));
            }
            else
            {
                overrideViewName = string.Format("{0}/{1}", culture.Name, viewName);
            }

            return overrideViewName;
        }
    }
}