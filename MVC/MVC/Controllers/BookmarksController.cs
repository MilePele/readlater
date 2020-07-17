using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Filters;
using MVC.Models;
using ReadLater.Entities;

namespace MVC.Controllers
{
    public class BookmarksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookmarks
        [UserAuthorization]
        public ActionResult Index()
        {
            var bookmarks = db.Bookmarks.Include(b => b.Category);
            return View(bookmarks.ToList());
        }

        // GET: Bookmarks/Details/5
        [UserAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // GET: Bookmarks/Create
        [UserAuthorization]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Bookmarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult Create([Bind(Include = "ID,URL,ShortDescription,CategoryId,CreateDate")] Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", bookmark.CategoryId);
            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        [UserAuthorization]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", bookmark.CategoryId);
            return View(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult Edit([Bind(Include = "ID,URL,ShortDescription,CategoryId,CreateDate")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookmark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", bookmark.CategoryId);
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        [UserAuthorization]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookmark bookmark = db.Bookmarks.Find(id);
            if (bookmark == null)
            {
                return HttpNotFound();
            }
            return View(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = db.Bookmarks.Find(id);
            db.Bookmarks.Remove(bookmark);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
