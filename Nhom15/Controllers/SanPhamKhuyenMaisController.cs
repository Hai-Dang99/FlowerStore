using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;

namespace Nhom15.Controllers
{
    public class SanPhamKhuyenMaisController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: SanPhamKhuyenMais
        public ActionResult Index()
        {
            var sanPhamKhuyenMais = db.SanPhamKhuyenMais.Include(s => s.ChuongTrinhKhuyenMai).Include(s => s.SanPham);
            return View(sanPhamKhuyenMais.ToList());
        }

        // GET: SanPhamKhuyenMais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKhuyenMai sanPhamKhuyenMai = db.SanPhamKhuyenMais.Find(id);
            if (sanPhamKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKhuyenMai);
        }

        // GET: SanPhamKhuyenMais/Create
        public ActionResult Create()
        {
            ViewBag.MaChuongTrinh = new SelectList(db.ChuongTrinhKhuyenMais, "MaChuongTrinh", "TenChuongTrinh");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: SanPhamKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChuongTrinh,MaSP,PhanTramKhuyenMai")] SanPhamKhuyenMai sanPhamKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.SanPhamKhuyenMais.Add(sanPhamKhuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChuongTrinh = new SelectList(db.ChuongTrinhKhuyenMais, "MaChuongTrinh", "TenChuongTrinh", sanPhamKhuyenMai.MaChuongTrinh);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPhamKhuyenMai.MaSP);
            return View(sanPhamKhuyenMai);
        }

        // GET: SanPhamKhuyenMais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKhuyenMai sanPhamKhuyenMai = db.SanPhamKhuyenMais.Find(id);
            if (sanPhamKhuyenMai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChuongTrinh = new SelectList(db.ChuongTrinhKhuyenMais, "MaChuongTrinh", "TenChuongTrinh", sanPhamKhuyenMai.MaChuongTrinh);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPhamKhuyenMai.MaSP);
            return View(sanPhamKhuyenMai);
        }

        // POST: SanPhamKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChuongTrinh,MaSP,PhanTramKhuyenMai")] SanPhamKhuyenMai sanPhamKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPhamKhuyenMai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChuongTrinh = new SelectList(db.ChuongTrinhKhuyenMais, "MaChuongTrinh", "TenChuongTrinh", sanPhamKhuyenMai.MaChuongTrinh);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", sanPhamKhuyenMai.MaSP);
            return View(sanPhamKhuyenMai);
        }

        // GET: SanPhamKhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKhuyenMai sanPhamKhuyenMai = db.SanPhamKhuyenMais.Find(id);
            if (sanPhamKhuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKhuyenMai);
        }

        // POST: SanPhamKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPhamKhuyenMai sanPhamKhuyenMai = db.SanPhamKhuyenMais.Find(id);
            db.SanPhamKhuyenMais.Remove(sanPhamKhuyenMai);
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
