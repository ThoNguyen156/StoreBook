using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;
using PagedList;
using PagedList.Mvc;

namespace StoreBook.Controllers
{
    public class SachOnlineController : Controller
    {
        private dbSachOnlineDataContext data = new dbSachOnlineDataContext();

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
            var listChuDe = data.CHUDEs.ToList();
            return PartialView(listChuDe);
        }

        public ActionResult NXSPartial()
        {
            var listNXS = data.NHAXUATBANs.ToList();
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
            var sach = data.SACHes.Where(s => s.MaNXB == id).ToList();
            return View(sach);
        }

        public ActionResult ChiTietSach(int id)
        {
            var sach = data.SACHes.FirstOrDefault(s => s.MaSach == id);

            if (sach == null)
            {
                return HttpNotFound(); // Hoặc xử lý trường hợp không tìm thấy sách theo id
            }

            return View(sach);
        }
    }
}