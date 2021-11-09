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
    public class SanPhamsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/SanPhams
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Saptheogia = sortOrder == "Gia" ? "gia_desc" : "Gia";

            var a = db.TinTucs.Select(s => s);
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);

            switch (sortOrder)
            {
                case "name_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.TenSP);
                    break;
                case "Gia":
                    sanPhams = sanPhams.OrderBy(s => s.DonGia);
                    break;
                case "gia_desc":
                    sanPhams = sanPhams.OrderByDescending(s => s.DonGia);
                    break;
                default:
                    sanPhams = sanPhams.OrderBy(s => s.MaSP);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(sanPhams.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanPham)
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            if (checkKey(sanPham.MaSP) == true)
            {
                ViewBag.Flag = 1;
                return View(sanPham);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    sanPham.Anh = "";
                    var f = Request.Files["ImageFile"];

                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/wwwroot/Anh/" + FileName);
                        f.SaveAs(uploadPath);
                        sanPham.Anh = FileName;
                    }

                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sanPham);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(sanPham);
            }
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonGia,MaDanhMuc,MoTa,Anh")] SanPham sanPham)
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];

                    string MaSP = sanPham.MaSP;

                    if (f != null && f.ContentLength > 0 )
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/wwwroot/Anh/" + FileName);
                        f.SaveAs(uploadPath);
                        sanPham.Anh = FileName;
                        bool kq = Update(sanPham);
                    }
                    else
                    {
                        bool kq = UpdateKhongAnh(sanPham);
                    }
                    
                    return RedirectToAction("Index");
                }
                return View(sanPham);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(sanPham);
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(sanPham).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            //return View(sanPham);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            try
            {
                db.SanPhams.Remove(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Flag=1;
                return View("Delete", sanpham);
            }
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
            return db.SanPhams.Count(u => u.MaSP == key) > 0;
        }

        private bool Update(SanPham entity)
        {
            try
            {
                var sp = db.SanPhams.Find(entity.MaSP);

                sp.TenSP = entity.TenSP;
                sp.DonGia = entity.DonGia;
                sp.MoTa = entity.MoTa;
                sp.Anh = entity.Anh;
                sp.MaDanhMuc = entity.MaDanhMuc;

                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return false;
            }
        }

        private bool UpdateKhongAnh(SanPham entity)
        {
            try
            {
                var sp = db.SanPhams.Find(entity.MaSP);

                sp.TenSP = entity.TenSP;
                sp.DonGia = entity.DonGia;
                sp.MoTa = entity.MoTa;
                sp.MaDanhMuc = entity.MaDanhMuc;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return false;
            }
        }
    }
}
