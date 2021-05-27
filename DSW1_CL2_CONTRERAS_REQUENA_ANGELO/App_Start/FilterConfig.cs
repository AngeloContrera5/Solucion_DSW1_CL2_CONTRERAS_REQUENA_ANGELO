using System.Web;
using System.Web.Mvc;

namespace DSW1_CL2_CONTRERAS_REQUENA_ANGELO
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
