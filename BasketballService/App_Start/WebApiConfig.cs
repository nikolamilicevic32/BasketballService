﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BasketballService.Interfaces;
using BasketballService.Repository;
using BasketballService.Resolver;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using Unity.Lifetime;

namespace BasketballService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();
            container.RegisterType<IKlubRepository, KlubRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IKosarkasRepository, KosarkasRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
