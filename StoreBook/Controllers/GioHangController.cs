using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;
using static StoreBook.Controllers.SachOnlineController;

namespace StoreBook.Controllers
{
    public class GioHangController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang>  lstGiohang = Session["Giohang"] as List<Giohang>;
            if(lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int ms,string url)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sp = lstgiohang.Find(n => n.iMaSach == ms);
            if(sp == null)
            {
                sp = new Giohang(ms);
                lstgiohang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

        private int Tongsoluong()
        {
            int iTongsoluong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if(lstGiohang != null)
            {
                iTongsoluong = lstGiohang.Sum(n => n.iSoLuong);
            }
            return iTongsoluong;
        }

        private double Tongtien()
        {
            double dTongtien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if(lstGiohang != null)
            {
                dTongtien = lstGiohang.Sum(n => n.dThanhTien);
            }
            return dTongtien;
        }

        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if(lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
    }
}