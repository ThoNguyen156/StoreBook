using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreBook.Models;

namespace StoreBook.Controllers
{
    public class HomeController : Controller
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

 
    }
}