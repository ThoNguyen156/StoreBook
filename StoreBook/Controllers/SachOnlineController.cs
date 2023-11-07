using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;

namespace StoreBook.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

		private List<SACH> LaySachMoi(int count)
		{
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhap).Take(count).ToList();

        }

		public ActionResult Index()
        {
            //Lay 6 quyen sach moi
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);
        }



        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd; 
            return PartialView(listChuDe);
        }

        public ActionResult NXSPartial()
        {
            var listNXS = from nxs in data.NHAXUATBANs select nxs; 
            return PartialView(listNXS);
        }

        private List<SACH> LaySachBanNhieu(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Soluongban).Take(count).ToList();
        }

        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = LaySachBanNhieu(6);
            return View(listSachBanNhieu);
        }

        public ActionResult SachTheoChuDe(int id)
		{
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
		}

        public ActionResult SachTheoNhaXuatBan(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult ChiTietSach (int id)
        {
            var sach = from s in data.SACHes
                       where s.MaSach == id
                       select s;
            return View(sach.Single());
        }

        public class Giohang
        {
            dbSachOnlineDataContext db = new dbSachOnlineDataContext();

            public int iMaSach { get; set; }
            public string sTenSach { get; set; }
            public string sAnhBia { get; set; }
            public double dDonGia { get; set; }
            public int iSoLuong { get; set; }
            public double dThanhTien
            {
                get { return iSoLuong * dDonGia; }

            }

            public Giohang(int ms)
            {
                iMaSach = ms;
                SACH s = db.SACHes.Single(n => n.MaSach == iMaSach);
                sTenSach = s.TenSach;
                sAnhBia = s.Anhbia;
                dDonGia = double.Parse(s.Giaban.ToString());
                iSoLuong = 1;
            }

        }
    }
}