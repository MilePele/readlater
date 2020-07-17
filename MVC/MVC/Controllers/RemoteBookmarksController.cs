using MVC.Models;
using ReadLater.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC.Controllers
{

    public class RemoteBookmarksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ExternalLoginModel eLoginModel = new ExternalLoginModel();

        // GET: api/RemoteBookmarks

        public List<Bookmark> Get(string token)
        {
            if (eLoginModel.CheckToken(token))
            {
                return db.Bookmarks.ToList();
            }
            return null;
        }

        // GET: api/RemoteBookmarks/5
        public Bookmark Get(int id, string token)
        {
            if (eLoginModel.CheckToken(token))
            {
                return db.Bookmarks.Where(b => b.ID == id).FirstOrDefault(); ;
            }
            return null;
        }

        // POST: api/RemoteBookmarks
        public bool Post(string url, string description, int categoryId, string token)
        {
            if (eLoginModel.CheckToken(token))
            {
                Bookmark bookmark = new Bookmark
                {
                    URL = url,
                    ShortDescription = description,
                    CategoryId = categoryId,
                    CreateDate = DateTime.Now
                };
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        // PUT: api/RemoteBookmarks/5
        public bool Put(int id, string url, string description, int categoryId, string token)
        {
            if (eLoginModel.CheckToken(token))
            {
                var bookmark = db.Bookmarks.Where(b => b.ID == id).FirstOrDefault();
                bookmark.URL = url;
                bookmark.ShortDescription = description;
                bookmark.CategoryId = categoryId;

                db.SaveChanges();
                return true;
            }
            return false;
        }

        // DELETE: api/RemoteBookmarks/5
        public bool Delete(int id, string token)
        {
            if (eLoginModel.CheckToken(token))
            {
                var bookmark = db.Bookmarks.Where(b => b.ID == id).FirstOrDefault();
                db.Bookmarks.Remove(bookmark);

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
