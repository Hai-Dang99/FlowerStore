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
    public class HoaDonsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/HoaDons
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var hoadons = db.HoaDons.Select(p => p);

            switch (sortOrder)
            {
                case "name_desc":
                    hoadons = hoadons.OrderByDescending(s => s.Username);
                    break;
                default:
                    hoadons = hoadons.OrderBy(s => s.MaHoaDon);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(hoadons.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Changes(string id)
        {
            foreach(var item in db.HoaDons)
            {
                if (item.MaHoaDon == id)
                {
                    item.TrangThai = !item.TrangThai;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index","HoaDons");
        }

        // GET: Admin/HoaDons/Create
        public ActionResult ThemMoi()
        {
            ViewBag.Username = new SelectList(db.TaiKhoanKhachHangs, "Username", "Password");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoi(FormCollection form)
        {
            bool dieukien = false;
            HoaDon hd = new HoaDon();
            do
            {
                hd.MaHoaDon = "HD" + Guid.NewGuid();
                foreach (var check_item in db.HoaDons)
                {
                    if (hd.MaHoaDon == check_item.MaHoaDon) dieukien = true;
                    else dieukien = false;
                }
            } while (dieukien);
            hd.Username = Session["User"].ToString().Trim();
            hd.NgayLap = DateTime.Now;
            hd.ThoiGianGiaoHang = Convert.ToDateTime(form["NgayGiaoHang"]);
            hd.isUsed = true;
            hd.TrangThai = false;
            db.HoaDons.Add(hd);
            db.SaveChanges();
            List<HoaDon> list = db.HoaDons.ToList();
            var ds = list.Where(d => d.Username == Session["User"].ToString());
            bool check = false;
            foreach (var item in ds)
            {
                if (item.Username == Session["User"].ToString().Trim() && item.isUsed==true)
                {
                    foreach (var sub_item in db.ChiTietGioHangs.ToList())
                    {
                        if (sub_item.Username == Session["User"].ToString().Trim())
                        {
                            ChiTietHoaDon n = new ChiTietHoaDon();
                            n.MaCTHoaDon = "CTHD" + Guid.NewGuid();
                            n.MaHoaDon = item.MaHoaDon;
                            n.MaSP = sub_item.MaSP;
                            n.SoLuong = sub_item.SoLuong;
                            n.DonGia = sub_item.SanPham.DonGia;
                            n.Username = Session["User"].ToString().Trim();
                            db.ChiTietHoaDons.Add(n);
                        }
                    }
                    item.isUsed = false;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
