using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTLQLSV.DAO;
using BTLQLSV.Entities;
using System.Data;

namespace BTLQLSV.BUS
{

    public class DangNhapBUS
    {
        DataHelper dh;
        public DangNhapBUS(string sqlcon)
        {
            dh = new DataHelper(sqlcon);
        }
        public User GetUser(string un, string pw)
        {
            DataTable dt = dh.FillDataTable("select * from Users WHERE (UserID ='" + un + "')" + "and (Password = '" + pw + "')");
            if (dt.Rows.Count > 0)
            {
                User user = new User();
                user.UserID = dt.Rows[0]["UserId"].ToString();
                user.PassWord = dt.Rows[0]["Password"].ToString();
                user.Role = int.Parse(dt.Rows[0]["Role"].ToString());
                return user;
            }
            else return null;
        }

    }
}
