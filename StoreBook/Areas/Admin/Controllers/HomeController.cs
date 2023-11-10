using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;

namespace StoreBook.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["Username"];
            var sMatKhau = f["Password"];

            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}