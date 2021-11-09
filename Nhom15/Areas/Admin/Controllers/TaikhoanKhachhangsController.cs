using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;
using PagedList;

namespace Nhom15.Areas.Admin.Controllers
{
    public class TaikhoanKhachhangsController : BaseController
    {

        private Nhom15DbContext db = new Nhom15DbContext();
        // GET: Admin/TaikhoanKhachhangs
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var acc = db.TaiKhoanKhachHangs.Select(a => a);

            switch (sortOrder)
            {
                case "name_desc":
                    acc = acc.OrderByDescending(s => s.Username);
                    break;
                default:
                    acc = acc.OrderBy(s => s.Username);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return View(acc.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoanKhachHang taikhoan = db.TaiKhoanKhachHangs.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoanKhachHang taikhoan = db.TaiKhoanKhachHangs.Find(id);
            try
            {
                db.TaiKhoanKhachHangs.Remove(taikhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Flag = 1;
                return View("Delete", taikhoan);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                if (checkKey(model.Username))
                {
                    ViewBag.Flag = 1;
                    return View(model);
                }
                else
                {
                    var acc = new TaiKhoanKhachHang();
                    acc.Username = model.Username;
                    acc.Password = model.Password;
                    acc.HoTen = model.HoTen;
                    acc.Email = model.Email;
                    acc.SDT = model.SDT;
                    db.TaiKhoanKhachHangs.Add(acc);
                    var result = db.SaveChanges();
                    if (result > 0)
                    {
                        ViewBag.Flag = 1;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Flag = 0;
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoanKhachHang tk = db.TaiKhoanKhachHangs.Find(id);

            if (tk == null)
            {
                return HttpNotFound();
            }

            var acc = new AccountModel();
            acc.Username = tk.Username;
            acc.Password = tk.Password;
            acc.ConfirmPassword = tk.Password;
            acc.HoTen = tk.HoTen;
            acc.Email = tk.Email;
            acc.SDT = tk.SDT;

            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var tk = db.TaiKhoanKhachHangs.Find(model.Username);
                tk.Password = model.Password;
                tk.HoTen = model.HoTen;
                tk.Email = model.Email;
                tk.SDT = model.SDT;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private bool checkKey(string key)
        {
            return db.TaiKhoanKhachHangs.Count(u => u.Username == key) > 0;
        }

    }
}