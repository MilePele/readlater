using IdentityServer3.AccessTokenValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MVC.Models;
using Owin;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(MVC.Startup))]
namespace MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Container container = new Container();
            //Configure Simple Injector
            ConfigureSimpleInjector(app, container);
        }
    }
}
