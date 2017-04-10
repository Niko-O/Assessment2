' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)

        BundleTable.Bundles.Add(New StyleBundle("~/Content/css").Include("~/Content/css/base-min.css", "~/Content/css/pure-min.css", "~/Content/css/Styles.css"))
        BundleTable.Bundles.Add(New ScriptBundle("~/Content/js").Include("~/Content/js/jquery.min.js", "~/Content/js/CommonScripts.js"))
    End Sub

End Class
