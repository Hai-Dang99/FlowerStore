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
    public class TaikhoanAdminsController : BaseController
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/TaikhoanAdmins
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SaptheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var acc = db.TaiKhoanAdmins.Where(s => s.FullControl == false).Select(a => a);

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
                    var admin = new TaiKhoanAdmin();
                    admin.Username = model.Username;
                    admin.Password = model.Password;
                    admin.HoTen = model.HoTen;
                    admin.Email = model.Email;
                    admin.FullControl = false;
                    db.TaiKhoanAdmins.Add(admin);
                    var result = db.SaveChanges();
                    if(result > 0)
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
            TaiKhoanAdmin tk = db.TaiKhoanAdmins.Find(id);
            
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

            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = db.TaiKhoanAdmins.Find(model.Username);
                admin.Password = model.Password;
                admin.HoTen = model.HoTen;
                admin.Email = model.Email;
                admin.FullControl = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoanAdmin tk = db.TaiKhoanAdmins.Find(id);
            if (tk == null)
            {
                return HttpNotFound();
            }
            return View(tk);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoanAdmin tk = db.TaiKhoanAdmins.Find(id);
            try
            {
                db.TaiKhoanAdmins.Remove(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Flag = 1;
                return View("Delete", tk);
            }
        }

        public ActionResult Profile()
        {
            string username = (string)Session["Username"];
            var tk = db.TaiKhoanAdmins.Find(username);

            return View(tk);
        }

        private bool checkKey(string key)
        {
            return db.TaiKhoanAdmins.Count(u => u.Username == key) > 0;
        }
    }
}