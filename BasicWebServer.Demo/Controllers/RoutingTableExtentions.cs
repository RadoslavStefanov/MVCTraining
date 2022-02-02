using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Demo.Controllers
{
    public static class RoutingTableExtentions
    {

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction)
            where TController : Controller
        => routingTable.MapGet(path, request => controllerFunction(
              CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction)
            where TController : Controller
             => routingTable.MapPost(path, request => controllerFunction(
              CreateController<TController>(request)));

        public static TController CreateController<TController>(Request request)
            => (TController)Activator.CreateInstance(typeof(TController), new[]{request});
    }
}
