using Nhom15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom15.Controllers
{
    public class HomeController : Controller
    {
        Nhom15DbContext db = new Nhom15DbContext();
        public ActionResult Index()
        {
            var danhmucs = db.DanhMucs.Select(d => d);
            return View(danhmucs);
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return View("Index");
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}