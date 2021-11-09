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
    public class SanPhamsController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();
        public ActionResult Index(FormCollection form)
        {
            string x = form["searchString"].ToString();
            ViewBag.input = x;
            var ds = db.SanPhams.Where(s => s.TenSP.Contains(x)).ToList();
            return View(ds);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SanPham> sanphams = db.SanPhams.ToList();
            var sanPham = sanphams.Where(s => s.MaSP == id).FirstOrDefault();

            if (sanphams == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
    }
}
