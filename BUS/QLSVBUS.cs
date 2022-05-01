using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTLQLSV.DAO;
using BTLQLSV.Entities;
using System.Windows;

namespace BTLQLSV.BUS
{
    public class QLSVBUS
    {
        SinhVienDAO svd;
        LopDAO lopd;
        public QLSVBUS(string sqlcon)
        {
            svd = new SinhVienDAO(sqlcon);
            lopd = new LopDAO(sqlcon);
        }
        public List<Lop> LayLop()
        {
            return lopd.LayDanhSachLop();
        }
        public List<SinhVien> LaySinhVienAll()
        {
            return svd.LaySinhVienAll();
        }
        public List<SinhVien> LaySinhVien(string malop)
        {
            return svd.LaySinhVien(malop);
        }
        public void NhapSinhVien(SinhVien sv)
        {
            lopd.SuaSiSo(1, sv.MaLop);
            svd.ThemSinhVien(sv);
        }
        public void SuaSinhvien(SinhVien sv, string malopcu)
        {
            svd.SuaSinhVien(sv);
            if (sv.MaLop != malopcu)
            {
                lopd.SuaSiSo(1, sv.MaLop);
                lopd.SuaSiSo(-1, malopcu);
            }
        }
        public void XoaSinhVien(string maSV)
        {
            svd.XoaSinhVien(maSV);
        }
        public void XoaSinhVienH(string maSV)
        {
            svd.XoaSinhVienH(maSV);
        }
    }
    
}
