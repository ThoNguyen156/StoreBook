using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreBook.Models
{
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