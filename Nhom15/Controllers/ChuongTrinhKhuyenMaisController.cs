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
    public class ChuongTrinhKhuyenMaisController : Controller
    {
        private Nhom15DbContext db = new Nhom15DbContext();

        // GET: ChuongTrinhKhuyenMais
        public ActionResult Index()
        {
            var chuongTrinhKhuyenMais = db.ChuongTrinhKhuyenMais.Include(c => c.SanPhamKhuyenMais);
            return View(chuongTrinhKhuyenMais.ToList());
        }
        public ActionResult ChiTiet(string id)
        {
            List<ChuongTrinhKhuyenMai> CTKM = db.ChuongTrinhKhuyenMais.ToList();
            var ctkm = CTKM.Where(s => s.MaChuongTrinh == id).FirstOrDefault();
            return View(ctkm);
        }
    }
}
