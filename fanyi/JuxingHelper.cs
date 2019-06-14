using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fanyi
{
    class JuxingHelper
    {
        public static DataTable LoadJuxingColList(string colid)
        {   

            String sql = "select det_id,det_memo from juxingdet where col_id = " + colid + " ";

            DataTable dt = DbHelper.GetTable(sql);
            return dt; 
        }

        public static DataTable LoadJuxingList()
        {

            String sql = "select col_id,col_name from juxingcol";

            DataTable dt = DbHelper.GetTable(sql);
            return dt;
        }

        public static bool AddJuXing(String juxingdetail)
        {

            String sql = "insert into  juxingdet( col_id, det_memo) values (1, '"+ juxingdetail + "')";

            int  dt = DbHelper.ExecSql(sql);
            if( dt >=0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DelJuXing(String detid)
        {

            String sql = "delete from  juxingdet where det_id = " + detid  ;

            int dt = DbHelper.ExecSql(sql);
            if (dt >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
