using MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Filters
{
    public class UserAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            string users = AppSettings.ConfigReader("AuthorizedUsers");
            if (!string.IsNullOrWhiteSpace(users) && user != null)
            {
                if (users.Contains(user.Identity.Name))
                {
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}