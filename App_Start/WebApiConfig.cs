﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PhaseTicket
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //formatter.SerializerSettings.ContractResolver =
            //    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
