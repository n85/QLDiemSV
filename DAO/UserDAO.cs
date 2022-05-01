using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BTLQLSV.DAO;
using BTLQLSV.Entities;
using System.Data.SqlClient;

namespace BTLQLSV.DAO
{
    public class UserDAO
    {
        DataHelper dh; 
        public UserDAO (string sqlcon)
        {
            dh = new DataHelper(sqlcon);
        }
        public User GetUser(string un, string pw)
        {
            DataTable dt = dh.FillDataTable("select * from UserList where (UserID = '" + un + "')" + "and (Password = '" + pw + "')");
            if (dt.Rows.Count > 0)
            {
                User user = new User();
                user.UserID = dt.Rows[0][0].ToString();
                user.PassWord = dt.Rows[0][1].ToString();
                user.Role = int.Parse(dt.Rows[0][2].ToString());
                return user;
            }
            else return null;
        }
        public List<User> GetUserList()
        {
            List<User> list = new List<User>();
            SqlDataReader dr = dh.ExcuteReader("select * from UserList");
            while (dr.Read())
            {
                User user = new User();
                user.UserID = dr["UserID"].ToString();
                user.PassWord = dr["Password"].ToString();
                user.Role = int.Parse(dr["Role"].ToString());

                list.Add(user);
            }
            dh.Close();         
            return list;
        }
        public void AddUser(User user)
        {

            dh.ExcuteNonQerry("insert into UserList Values('" + user.UserID + "', '" + user.PassWord +
                                                                "', '" + user.Role + "')");
        }
        public void DeleteUser(string userID)
        {
            dh.ExcuteNonQerry("Delete from UserList WHERE UserID = '" + userID + "'");
        }
        public void EditUser(User user)
        {
            dh.ExcuteNonQerry("UPDATE UserList set Password = '" + user.PassWord +
                                                                "', Role = '" + user.Role + "'");
        }
    }
}
