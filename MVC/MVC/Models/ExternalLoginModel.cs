using MVC.Helpers;
using ReadLater.Data;
using ReadLater.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ExternalLoginModel
    {
        private ReadLaterDataContext db = new ReadLaterDataContext();
        public bool Insert(string token)
        {
            bool response = false;

            try
            {
                db.Database.ExecuteSqlCommand($"insert into ExternalLogins (Token, TimeCreated, Expired) values ('{token}', '{DateTime.Now}', 0)");

                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }


            return response;
        }

        public bool CheckToken(string token)
        {
            bool response = false;
            int validMin = Convert.ToInt32(AppSettings.ConfigReader("tokenValidTime"));
            try
            {
                ExternalLogin eLogin = db.ExternalLogins.Where(e => e.Token == token).FirstOrDefault();
                if (eLogin.Expired)
                {
                    response = false;
                }
                else
                {
                    if(eLogin.TimeCreated > DateTime.Now.AddMinutes(-validMin))
                    {
                        response = true;
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand($"update ExternalLogins set Expired = 1 where Token = '{token}'");

                        response = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response = false;
            }

            return response;
        }
    }
}