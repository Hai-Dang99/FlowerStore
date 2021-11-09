using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;
using PagedList;

namespace Nhom15.Areas.Admin.Controllers
{
    public class DanhMucsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/Home
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var danhmucs = db.DanhMucs.Select(p => p);

            switch (sortOrder)
            {
                case "name_desc":
                    danhmucs = danhmucs.OrderByDescending(s => s.TenDanhMuc);
                    break;
                default:
                    danhmucs = danhmucs.OrderBy(s => s.MaDanhMuc);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(danhmucs.ToPagedList(pageNumber, pageSize));
        }

        // GET: DanhMucs/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMuc danhmuc)
        {
            if (checkKey(danhmuc.MaDanhMuc) == true)
            {
                ViewBag.Flag = 1;
                return View(danhmuc);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    db.DanhMucs.Add(danhmuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(danhmuc);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(danhmuc);
            }
        }

        // GET: Admin/Danhmucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DanhMuc danhmuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(danhmuc).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(danhmuc);
            }
            catch (Exception ex)
            {
                ViewBag.Error("Lỗi khi nhập dữ liệu " + ex.Message);
                return View(danhmuc);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            try
            {
                db.DanhMucs.Remove(danhmuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //ViewBag.Error("Không thể xóa bản ghi " + ex.Message);
                ViewBag.Flag = 1;
                return View("Delete", danhmuc);
            }
        }

        private bool checkKey(string key)
        {
            return db.DanhMucs.Count(u => u.MaDanhMuc == key) > 0;
        }
    }
}