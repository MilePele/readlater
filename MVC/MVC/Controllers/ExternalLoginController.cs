using Microsoft.AspNet.Identity;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MVC.Controllers
{
    public class ExternalLoginController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        public string Login(string email, string password)
        {
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            if (ph.VerifyHashedPassword(user.PasswordHash, password) != PasswordVerificationResult.Failed)
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(email + DateTime.Now.ToString("YYYYMMddhhmmss"));
                string token = Convert.ToBase64String(data);
                ExternalLoginModel elm = new ExternalLoginModel();
                if (elm.Insert(token))
                {
                    return token;
                }
                else
                {
                    return "Invalid atempt";
                }
            }
            return "Invalid atempt";
        }
    }
}
