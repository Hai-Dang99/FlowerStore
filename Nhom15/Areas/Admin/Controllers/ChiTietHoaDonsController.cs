using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;

namespace Nhom15.Areas.Admin.Controllers
{
    public class ChiTietHoaDonsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/ChiTietHoaDons
        public ActionResult Index()
        {
            var chiTietHoaDons = db.ChiTietHoaDons.Include(c => c.HoaDon).Include(c => c.SanPham);
            return View(chiTietHoaDons.ToList());
        }

        // GET: Admin/ChiTietHoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chiTietHoaDon = db.ChiTietHoaDons.Where(ct=>ct.MaHoaDon==id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDons/Create
        public ActionResult ThemMoi()
        {
            ViewBag.MaHoaDon = new SelectList(db.HoaDons, "MaHoaDon", "Username");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoi(string id)
        {
            return View();

            //foreach (var sub_item in db.ChiTietGioHangs)
            //{
            //    if (sub_item.Username == id.Trim())
            //    {
            //        foreach(var item in ds)
            //        {
            //            if (item.Username==id.Trim())
            //            {
            //                ChiTietHoaDon n = new ChiTietHoaDon();
            //                n.MaCTHoaDon = "CTHD" + new Guid();
            //                n.MaHoaDon = item.MaHoaDon;
            //                n.MaSP = sub_item.MaSP;
            //                n.SoLuong = sub_item.SoLuong;
            //                n.DonGia = sub_item.SanPham.DonGia;
            //                n.Username = id.Trim();
            //                db.ChiTietHoaDons.Add(n);
            //            }
            //        }
            //    }
            //}
            //db.SaveChanges();
            //return RedirectToAction("Index","Home");
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
