using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class DeptQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_department;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(DeptDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_department (code, name) VALUES ('{dto.DEPT_CODE}', '{dto.DEPT_NAME}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Update(DeptDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"UPDATE tbl_department SET name = '{dto.DEPT_NAME}' WHERE code = '{dto.DEPT_CODE}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Delete(DeptDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"DELETE FROM tbl_department WHERE code = '{dto.DEPT_CODE}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
