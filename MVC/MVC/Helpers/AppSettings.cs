using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVC.Helpers
{
    public static class AppSettings
    {
        public static string ConfigReader(string settkey)
        {
            try
            {
                return ConfigurationManager.AppSettings[settkey];
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}