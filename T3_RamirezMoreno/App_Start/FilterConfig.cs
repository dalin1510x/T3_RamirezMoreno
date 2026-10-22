using System.Web;
using System.Web.Mvc;

namespace T3_RamirezMoreno
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
