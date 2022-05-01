using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BTLQLSV.DAO
{
    public class DataHelper             //Lớp này sẽ dùng chung, nên đổi sang public
    {
        string sqlcon; // = @"Data Source=DESKTOP-48U32JN\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True";
        SqlConnection conn = null;
        public DataHelper(string sqlcon)
        {
            conn = new SqlConnection(sqlcon);
            this.sqlcon = sqlcon;
        }
        // PHƯƠNG PHÁP THAO TÁC HƯỚNG KẾT NỐI
        public void Open()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }
        public void Close()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        /// <summary>
        /// Phương thức thực thi câu lệnh truy vấn insert, update, delete
        /// </summary>
        /// <param name="sql">Câu lệnh truy vấn SQL cần thực hiện</param>
        public void ExcuteNonQerry(string sql)
        {
            Open();         //Mở kết nối đến CSDL cho đối tượng conn (Connection)
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Phương thức thực thi câu lệnh truy vấn select
        /// </summary>
        /// <param name="sql">Câu lệnh truy vấn select</param>
        /// <returns>Đối tượng SQLDataReader</returns>
        public SqlDataReader ExcuteReader(string sql)
        {
            Open();
            SqlCommand cm = new SqlCommand(sql, conn);
            //return cm.EndExecuteReader();
            SqlDataReader dr = cm.ExecuteReader();
            return dr;
        }

        // PHƯƠNG PHÁP THAO TÁC PHI KẾT NỐI
        public DataTable FillDataTable(string sqlselect)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlselect, sqlcon);
            adapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Phương thức thêm một dòng vào DataTable
        /// </summary>
        /// <param name="dt">Đối tượng DataTable</param>
        /// <param name="values">Các giá trị được truyền vào</param>
        public void AddRows(DataTable dt, params object[] values)
        {
            DataRow dr = dt.NewRow();
            for (int i = 0; i < values.Length; i++)
                dr[i] = values[i];
            dt.Rows.Add(dr);
        }
        /// <summary>
        /// Lọc các bản ghi thỏa mãn điều kiện
        /// </summary>
        /// <param name="dt">Đối tượng DataTable</param>
        /// <param name="dk">Điều kiện lọc</param>
        /// <returns></returns>
        public DataView Fillter(DataTable dt, string dk)
        {
            DataView dv = new DataView();
            dv.RowFilter = dk;
            return dv;
        }

        public void EditRows(DataTable dt, string dk, params object[] values)
        {
            DataView dv = Fillter(dt, dk);
            dv.AllowEdit = true;
            if (dv.Count > 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    dv[0][i] = values[i];
                }    
            }
            
        }

        public void DeleteRows(DataTable dt, string dk)
        {
            DataView dv = Fillter(dt, dk);
            dv.AllowEdit = true;
            while (dv.Count > 0)
            {
                dv[0].Delete();
            }
        }

        public void UpdateDateToDatabase(DataTable dt, string tenbang)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from " + tenbang, conn);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(dt);
        }
    }
}
