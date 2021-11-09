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
    public class DanhMucsController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        public ActionResult Index()
        {
            return View(db.DanhMucs.ToList());
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DanhMuc> danhmucs = db.DanhMucs.ToList();
            var danhMuc = danhmucs.Where(d=> d.MaDanhMuc == id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }
    }

}
