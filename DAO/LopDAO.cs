using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTLQLSV.Entities;
using System.Data;
using System.Data.SqlClient;
using BTLQLSV.DAO;

namespace BTLQLSV.DAO
{
    public class LopDAO
    {
        DataHelper dh;
        DataTable dt;
        public LopDAO(string sqlcon)
        {
            dh = new DataHelper(sqlcon);
        }

        /// <summary>
        /// Thêm thông tin 1 lớp vào CSDL
        /// </summary>
        /// <param name="lop">Đối tượng lớp cần thêm vào CSDL</param>
        public void SaveChange()
        {
            dh.UpdateDateToDatabase(dt, "Lop");
        }

        public void ThemLop(Lop lop)
        {
            dh.AddRows(dt, lop.MaLop, lop.TenLop, lop.SiSo);
            SaveChange();
            //INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country)
        }
        public void ThemLopH(Lop lop)
        {
            dh.ExcuteNonQerry("insert into Lop values ('" + lop.MaLop + "','" + lop.TenLop + "','" + lop.SiSo + "')");
            //INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country)
        }
        /// <summary>
        /// Sửa thông tin 1 lớp vào CSDL
        /// </summary>
        /// <param name="lop">Đối tượng lớp cần sửa vào CSDL</param>
        public void SuaLop(Lop lop)
        {
            dh.EditRows(dt, "MaLop = '" + lop.MaLop + "'", lop.MaLop, lop.TenLop, lop.SiSo);
            SaveChange();
        }
        public void SuaSiSo(int siso, string malop)
        {
            dh.ExcuteNonQerry("Update Lop set SiSo = SiSo + 2 WHERE MaLop='" + malop + "'");
        }
        public void SuaLopH(Lop lop)
        {
            dh.ExcuteNonQerry("update Lop set TenLop = '" + lop.TenLop + "', SiSo = '" + lop.SiSo + "' where MaLop = '" + lop.MaLop + "'");
        }
        /// <summary>
        /// Xóa thông tin 1 lớp từ CSDL
        /// </summary>
        /// <param name="lop">Đối tượng lớp cần xóa từ CSDL</param>
        /// 
        public void XoaLop(string malop)
        {
            dh.DeleteRows(dt, "MaLop = '" + malop + "'");
            SaveChange();
        }
        public void XoaLopH(string malop)
        {
            dh.ExcuteNonQerry("Delete from Lop where MaLop = '" + malop + "'");
        }
        public List<Lop> LayDanhSachLopH()
        {
            List<Lop> list = new List<Lop>();
            SqlDataReader dr = dh.ExcuteReader("Select * from Lop");
            while (dr.Read())
            {
                Lop lop = new Lop();
                lop.MaLop = dr["MaLop"].ToString();
                lop.TenLop = dr["TenLop"].ToString();
                lop.SiSo = int.Parse(dr["SiSo"].ToString());

                list.Add(lop); //Thêm lớp vào danh sách lớp
            }
            dh.Close();
            return list;
        }

        public List<Lop> LayDanhSachLop()
        {
            List<Lop> list = new List<Lop>();
            dt = dh.FillDataTable("select * from Lop");
            foreach (DataRow dr in dt.Rows)
            {
                Lop l = new Lop();
                l.MaLop = dr["MaLop"].ToString();
                l.TenLop = dr["TenLop"].ToString();
                l.SiSo = int.Parse(dr["SiSo"].ToString());

                list.Add(l);
            }

            return list;
        }
    }
}
