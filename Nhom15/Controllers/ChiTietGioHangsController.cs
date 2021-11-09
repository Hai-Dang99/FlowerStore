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
    public class ChiTietGioHangsController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: ChiTietGioHangs
        public ActionResult Index()
        {
            var chiTietGioHangs = db.ChiTietGioHangs.Include(c => c.SanPham).Include(c => c.TaiKhoanKhachHang);
            return View(chiTietGioHangs.ToList());
        }

        // GET: ChiTietGioHangs/Details/5
        public ActionResult Add(string id)
        {
            bool dieukien = false;
            ChiTietGioHang g = new ChiTietGioHang();
            do
            {
                g.GioHangID = "GioHang" + Guid.NewGuid();
                foreach (var item in db.ChiTietGioHangs)
                {
                    if (g.GioHangID == item.GioHangID) dieukien = true;
                    else dieukien = false;
                }
            } while (dieukien);
            g.Username = Session["User"].ToString();
            g.MaSP = id;
            g.SoLuong = 1;
            db.ChiTietGioHangs.Add(g);
            db.SaveChanges();
            return RedirectToAction("Index", "ChiTietGioHangs");
        }
        public ActionResult CapNhat(FormCollection form)
        {
            List<ChiTietGioHang> ls = db.ChiTietGioHangs.ToList();
            var remo = ls.Where(s => s.GioHangID == form["GioHangID"]).FirstOrDefault();
            ChiTietGioHang g = new ChiTietGioHang();
            g.GioHangID = form["GioHangID"];
            g.Username = remo.Username;
            g.MaSP = remo.MaSP;
            g.SoLuong = Convert.ToInt32(form["SoLuong"]);
            g.ThanhTien = g.SoLuong * remo.SanPham.DonGia;
            db.ChiTietGioHangs.Remove(ls.Where(s => s.GioHangID == form["GioHangID"]).FirstOrDefault());
            db.ChiTietGioHangs.Add(g);
            db.SaveChanges();
            return RedirectToAction("Index", "ChiTietGioHangs");
        }
        public ActionResult Xoa(String id)
        {
            List<ChiTietGioHang> ls = db.ChiTietGioHangs.ToList();
            db.ChiTietGioHangs.Remove(ls.Where(s => s.GioHangID == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index", "ChiTietGioHangs");
        }
    }
}
