﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

/// <summary>
/// Summary description for WebApiConfig
/// </summary>
public class WebApiConfig
{
   public static void Register(HttpConfiguration config )
    {

        //Web Api routes
        config.MapHttpAttributeRoutes();

        config.EnableCors();

        config.Routes.MapHttpRoute(
        name: "ProductApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = System.Web.Http.RouteParameter.Optional }
        );

        config.Routes.MapHttpRoute("AddToCart", "api/{controller}/{action}/{id}", new { id = System.Web.Http.RouteParameter.Optional }
         );
    }
}