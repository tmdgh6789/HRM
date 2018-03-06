using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AllLogQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_all_log;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(AllLogDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_all_log (admin, what, log, reason, update_date) VALUES ('{dto.ALLLOG_ADMIN}', '{dto.ALLLOG_WHAT}', '{dto.ALLLOG_LOG}', '{dto.ALLLOG_REASON}', '{dto.ALLLOG_UPDATE_DATE}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        //public void Update(AllLogDTO dto)
        //{
        //    SharedPreference.Instance.DBM.SqlConn.Open();
        //    string query = $"UPDATE tbl_department SET name = '{dto.DEPT_NAME}' WHERE code = '{dto.DEPT_CODE}';";
        //    SharedPreference.Instance.DBM.SqlComm.CommandText = query;
        //    SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
        //    SharedPreference.Instance.DBM.SqlConn.Close();
        //}

        //public void Delete(AllLogDTO dto)
        //{
        //    SharedPreference.Instance.DBM.SqlConn.Open();
        //    string query = $"DELETE FROM tbl_department WHERE code = '{dto.DEPT_CODE}';";
        //    SharedPreference.Instance.DBM.SqlComm.CommandText = query;
        //    SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
        //    SharedPreference.Instance.DBM.SqlConn.Close();
        //}
    }
}
