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
    public class ChuongTrinhKhuyenMaisController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/ChuongTrinhKhuyenMais
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoBatdau = String.IsNullOrEmpty(sortOrder) ? "ngaybd_desc" : "";
            ViewBag.SaptheoKetthucs = String.IsNullOrEmpty(sortOrder) ? "ngaykt_desc" : "";

            var CTKM = db.ChuongTrinhKhuyenMais.Select(p => p);

            switch (sortOrder)
            {
                case "ngaybd_desc":
                    CTKM = CTKM.OrderByDescending(s => s.NgayBatDau);
                    break;
                case "ngaykt_desc":
                    CTKM = CTKM.OrderByDescending(s => s.NgayKetThuc);
                    break;
                default:
                    CTKM = CTKM.OrderBy(s => s.MaChuongTrinh);
                    break;
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return View(CTKM.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Admin/ChuongTrinhKhuyenMais/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Find(id);
        //    if (chuongTrinhKhuyenMai == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(chuongTrinhKhuyenMai);
        //}

        // GET: Admin/ChuongTrinhKhuyenMais/Create
        public ActionResult Create()
        {
            ViewBag.MaChuongTrinh = new SelectList(db.SanPhamKhuyenMais, "MaChuongTrinh", "MaSP");
            return View();
        }

        // POST: Admin/ChuongTrinhKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            if (checkKey(chuongTrinhKhuyenMai.MaChuongTrinh) == true)
            {
                ViewBag.Flag = 1;
                return View(chuongTrinhKhuyenMai);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    db.ChuongTrinhKhuyenMais.Add(chuongTrinhKhuyenMai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(chuongTrinhKhuyenMai);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(chuongTrinhKhuyenMai);
            }
        }

        // GET: Admin/ChuongTrinhKhuyenMais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Find(id);
            if (chuongTrinhKhuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuongTrinh = new SelectList(db.SanPhamKhuyenMais, "MaChuongTrinh", "MaSP", chuongTrinhKhuyenMai.MaChuongTrinh);
            return View(chuongTrinhKhuyenMai);
        }

        // POST: Admin/ChuongTrinhKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuongTrinh,TenChuongTrinh,NgayBatDau,NgayKetThuc,Anh")] ChuongTrinhKhuyenMai chuongTrinhKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuongTrinhKhuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuongTrinh = new SelectList(db.SanPhamKhuyenMais, "MaChuongTrinh", "MaSP", chuongTrinhKhuyenMai.MaChuongTrinh);
            return View(chuongTrinhKhuyenMai);
        }

        // GET: Admin/ChuongTrinhKhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Find(id);
            if (chuongTrinhKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(chuongTrinhKhuyenMai);
        }

        // POST: Admin/ChuongTrinhKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChuongTrinhKhuyenMai chuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Find(id);
            db.ChuongTrinhKhuyenMais.Remove(chuongTrinhKhuyenMai);
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
            return db.DanhMucs.Count(u => u.MaDanhMuc == key) > 0;
        }
    }
}
