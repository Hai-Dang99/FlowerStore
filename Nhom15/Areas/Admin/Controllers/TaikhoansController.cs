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
    public class TaiKhoansController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        // GET: TaiKhoans/Create
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string email, string matkhau)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(u => u.username.Equals(email) && u.password.Equals(matkhau));
                if (user.Count() > 0)
                {
                    Session["HoTen"] = user.FirstOrDefault().HoTen;
                    Session["Email"] = user.FirstOrDefault().Email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Đăng nhập không thành công!";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangKy([Bind(Include = "username,password,HoTen,Email,IsAdmin,SDT")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }

            return View(taiKhoan);
        }

        public ActionResult Test()
        {
            return View();
        }


    }
}
