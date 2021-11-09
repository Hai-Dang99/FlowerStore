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
    public class TaiKhoanKhachHangsController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

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
                var user = db.TaiKhoanKhachHangs.Where(u => u.Username.Equals(email) && u.Password.Equals(matkhau));
                if (user.Count() > 0)
                {
                    Session["User"] = user.FirstOrDefault().Username;
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
        public ActionResult DangKy([Bind(Include = "Username,Password,HoTen,Email,SDT")] TaiKhoanKhachHang taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoanKhachHangs.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("DangNhap");
            }

            return View(taiKhoan);
        }

        public ActionResult ChiTiet()
        {
            String username = (string)Session["User"];
            TaiKhoanKhachHang user = db.TaiKhoanKhachHangs.Where(u => u.Username == username).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChiTiet([Bind(Include = "username,password,HoTen,Email,IsAdmin,SDT")] TaiKhoanKhachHang taiKhoan)
        {
            if (ModelState.IsValid)
            {
                String username = (string)Session["User"];
                TaiKhoanKhachHang user = db.TaiKhoanKhachHangs.Where(u => u.Username == username).FirstOrDefault();
                //user.username = taiKhoan.username;
                user.Password = taiKhoan.Password;
                user.HoTen = taiKhoan.HoTen;
                user.Email = taiKhoan.Email;
                user.SDT = taiKhoan.SDT;
                db.SaveChanges();
                ViewBag.Ok = "Cập nhập thành công";
                Session["HoTen"] = taiKhoan.HoTen;
                return View(taiKhoan);
            }
            return View(taiKhoan);
        }


    }
}
