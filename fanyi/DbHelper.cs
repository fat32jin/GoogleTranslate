using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;  //添加引用
using System.Data;

namespace fanyi
{
    public static class DbHelper
    {
        //建立连接对象
        static OleDbConnection conn  ;

        
        public static DataTable GetTable(String sql)
        {
            DataTable dt = new DataTable();
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=acdb1.mdb;Jet OLEDB:Database Password=bdyy");
            using (conn)
            {
                OleDbCommand cmd = conn.CreateCommand();

                cmd.CommandText = sql;
                conn.Open();
                OleDbDataReader dr = cmd.ExecuteReader();
                
                if (dr.HasRows)
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        dt.Columns.Add(dr.GetName(i));
                    }
                    dt.Rows.Clear();
                }
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        row[i] = dr[i];
                    }
                    dt.Rows.Add(row);
                }
                cmd.Dispose();
                
            }
            conn = null;
            return dt;


        }

        public static int ExecSql(String sql)
        {
            DataTable dt = new DataTable();
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=acdb1.mdb;Jet OLEDB:Database Password=bdyy");
            int re = 0;
            using (conn)
            {
                OleDbCommand cmd = conn.CreateCommand();

                cmd.CommandText = sql;
                conn.Open();
                 re = cmd.ExecuteNonQuery(); 
                cmd.Dispose();

            }
            conn = null;
            return re;


        }

    }
}
