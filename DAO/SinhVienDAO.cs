using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BTLQLSV.Entities;
using System.Windows;

namespace BTLQLSV.DAO
{
    public class SinhVienDAO
    {
        DataHelper dh;
        DataTable dt;

        public SinhVienDAO(string sqlcon)
        {
            dh = new DataHelper(sqlcon);

        }

        public List<SinhVien> LaySinhVien(string malop)
        {
            dt = dh.FillDataTable("select * from SinhVien WHERE MaLop = '" + malop + "'");
            List<SinhVien> l = new List<SinhVien>();
            foreach(DataRow dr in dt.Rows)
            {
                SinhVien sv = new SinhVien();
                sv.MaLop = dr["MaLop"].ToString();
                sv.QueQuan = dr["QueQuan"].ToString();
                sv.NgaySinh = dr["NgaySinh"].ToString();
                sv.TenSV = dr["TenSV"].ToString();
                sv.MaSV = dr["MaSV"].ToString();
                l.Add(sv);
            }

            return l;
        }
        public List<SinhVien> LaySinhVienAll()
        {
            dt = dh.FillDataTable("select * from SinhVien");
            List<SinhVien> l = new List<SinhVien>();
            foreach (DataRow dr in dt.Rows)
            {
                SinhVien sv = new SinhVien();
                sv.MaLop = dr["MaLop"].ToString();
                sv.QueQuan = dr["QueQuan"].ToString();
                sv.NgaySinh = dr["NgaySinh"].ToString();
                sv.TenSV = dr["TenSV"].ToString();
                sv.MaSV = dr["MaSV"].ToString();
                l.Add(sv);
            }
            return l;
        }
        public void ThemSinhVien(SinhVien sv)
        {
            dh.AddRows(dt, sv.MaSV, sv.TenSV, sv.MaLop, sv.QueQuan, sv.NgaySinh);
            dh.UpdateDateToDatabase(dt, "SinhVien");
            //Dành cho lt hướng kết nối thì làm như sau:
            //dh.ExcuteNonQerry("INSERT into SinhVien VALUES('" + sv.MaSV + "', " + sv.)
        }
        public void SuaSinhVien(SinhVien sv)
        {
            dh.EditRows(dt, "MaSV='" + sv.MaSV + "'", sv.MaSV, sv.TenSV, sv.MaLop, sv.QueQuan, sv.NgaySinh);
            dh.UpdateDateToDatabase(dt, "SinhVien");
        }
        public void XoaSinhVien(string masv)
        {
            dh.DeleteRows(dt, "MaSV='" + masv + "'");
            dh.UpdateDateToDatabase(dt, "SinhVien");
        }
        public void XoaSinhVienH(string masv)
        {
            dh.ExcuteNonQerry("Delete from SinhVien WHERE MaSV='" + masv + "'");
        }
    }
}
