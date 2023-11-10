using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;

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

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XoaSPKhoiGioHang(int iMaSach)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sp = lstGiohang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if(sp !=  null)
            {
                lstGiohang.RemoveAll(n => n.iMaSach == iMaSach);
                if(lstGiohang.Count == 0)
                {
                    return RedirectToAction("Index", "SachOnline");
                }    
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {
            List<Giohang> lstGioHang = Laygiohang();
            Giohang sp = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            // Neu ton tai thi cho sua so luong
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaGioHang()
        {
            List<Giohang> lstGioHang = Laygiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "SachOnline");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }    
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "SachOnline");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstGiohang);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<Giohang> lstGiohang = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.Ngaydat = DateTime.Now;
            var Ngaygiao = String.Format("{0:MM/dd/yyyy}", f["Ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(Ngaygiao);
            ddh.Tinhtranggiaohang = 1;
            ddh.Dathanhtoan = false;
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            foreach (var item in lstGiohang)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSach = item.iMaSach;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.Dongia = (decimal)item.dDonGia;
                db.CHITIETDATHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}