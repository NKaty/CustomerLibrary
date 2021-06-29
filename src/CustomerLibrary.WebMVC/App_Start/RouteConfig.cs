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
                name: "CustomerNotes",
                url: "Customers/{customerId}/Notes/{Action}/{noteId}",
                defaults: new { controller = "Notes", action = "Index", noteId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}