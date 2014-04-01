namespace VegaWeb

open System
open System.Net.Http
open System.Web
open System.Web.Http
open System.Web.Mvc
open System.Web.Routing
open System.Web.Optimization
open Owin
open Microsoft.AspNet.SignalR
open Microsoft.AspNet.SignalR.Hubs


type BundleConfig() =
    static member RegisterBundles (bundles:BundleCollection) =
        bundles.Add(ScriptBundle("~/bundles/jquery").Include([| "~/Scripts/jquery-{version}.js" |]))
        bundles.Add(ScriptBundle("~/bundles/signalr").Include([| "~/Scripts/jquery.signalR-{version}.js" |] ))
        bundles.Add(ScriptBundle("~/bundles/d3").Include([| "~/Scripts/d3.js" |]))
        bundles.Add(ScriptBundle("~/bundles/topojson").Include([| "~/Scripts/topojson.js" |]))
        bundles.Add(ScriptBundle("~/bundles/vega").Include([| "~/Scripts/vega.js"; "~/Scripts/editor.js" |]))


        // Use the development version of Modernizr to develop with and learn from. Then, when you're
        // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(ScriptBundle("~/bundles/modernizr").Include([|"~/Scripts/modernizr-*"|]))

        bundles.Add(ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"))

        bundles.Add(StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/editor.css"))

/// Route for ASP.NET MVC applications
type Route = { 
    controller : string
    action : string
    id : UrlParameter }

type HttpRoute = {
    controller : string
    id : RouteParameter }

type Global() =
    inherit System.Web.HttpApplication() 

    static member RegisterWebApi(config: HttpConfiguration) =
        // Configure routing
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            "DefaultApi", // Route name
            "api/{controller}/{id}", // URL with parameters
            { controller = "{controller}"; id = RouteParameter.Optional } // Parameter defaults
        ) |> ignore
        // Additional Web API settings

    static member RegisterFilters(filters: GlobalFilterCollection) =
        filters.Add(new HandleErrorAttribute())

    static member RegisterRoutes(routes:RouteCollection) =
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            { controller = "Home"; action = "Index"; id = UrlParameter.Optional } // Parameter defaults
        ) |> ignore

    member x.Application_Start() =
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
        Global.RegisterFilters(GlobalFilters.Filters)
        Global.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles BundleTable.Bundles

        
type MyWebStartUp() =
    member x.Configuration (app :IAppBuilder) =
        app.MapSignalR() |> ignore
        ()

[<assembly: Microsoft.Owin.OwinStartup(typeof<MyWebStartUp>)>]
do()


