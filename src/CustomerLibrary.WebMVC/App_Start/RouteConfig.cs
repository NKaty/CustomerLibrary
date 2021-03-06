using System.Web.Mvc;
using System.Web.Routing;

namespace CustomerLibrary.WebMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Another way to define routes
            //routes.MapRoute(
            //    name: "CustomerAddresses",
            //    url: "Customers/{customerId}/Addresses/{Action}/{addressId}",
            //    defaults: new {controller = "Addresses", action = "Index", addressId = UrlParameter.Optional}
            //);

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/page/{page}/{action}/{id}",
                defaults: new {controller = "Customers", action = "Index", page = 1, id = UrlParameter.Optional}
            );
        }
    }
}