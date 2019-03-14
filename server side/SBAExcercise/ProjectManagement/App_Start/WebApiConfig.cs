﻿using Newtonsoft.Json.Serialization;
using ProjectManagement.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ProjectManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            config.Filters.Add(new ProjectManagerLogFilter());
            config.Filters.Add(new ProjectManagerExceptionFilter());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
