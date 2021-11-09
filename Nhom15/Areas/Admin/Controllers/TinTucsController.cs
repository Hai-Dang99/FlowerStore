using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;
using PagedList;

namespace Nhom15.Areas.Admin.Controllers
{
    public class TinTucsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/TinTucs
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.username = new SelectList(db.TaiKhoanAdmins, "username", "username");

            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SaptheoNgay = String.IsNullOrEmpty(sortOrder) ? "ngay" : "";

            var tintucs = db.TinTucs.Select(p => p);

            switch (sortOrder)
            {
                case "name_desc":
                    tintucs = tintucs.OrderByDescending(s => s.Title);
                    break;
                case "ngay_desc":
                    tintucs = tintucs.OrderBy(s => s.AddTime);
                    break;
                default:
                    tintucs = tintucs.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return View(tintucs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TinTucs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinTuc,Title,NoiDung,Username,Anh,TomTat,AddTime")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.username = new SelectList(db.TaiKhoanAdmins, "username", "password", tinTuc.TaiKhoanAdmin.Username);
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinTuc,Title,NoiDung,username,Anh,TomTat,AddTime")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.username = new SelectList(db.TaiKhoanAdmins, "username", "password", tinTuc.TaiKhoanAdmin.Username);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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

        private bool checkKey(string key)
        {
            return db.TinTucs.Count(u => u.MaTinTuc == key) > 0;
        }
    }
}
