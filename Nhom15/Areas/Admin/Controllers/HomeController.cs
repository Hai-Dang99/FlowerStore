using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom15.Models;

namespace Nhom15.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: Admin/Home
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TaiKhoanAdmin model)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoanAdmins.Where(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password)).ToList();
                if (user.Count > 0)
                {
                    Session["Username"] = user.FirstOrDefault().Username;
                    var check = user.FirstOrDefault().FullControl; 
                    if(check == true)
                    {
                        Session["role"] = "SUPER_ADMIN";
                    }
                    else
                    {
                        Session["role"] = "ADMIN";
                    }
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Flag = 1;
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Role"] = null;
            return Redirect("/Admin/Home/Index");
        }
    }
}