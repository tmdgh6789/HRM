using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AdminQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_admin;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectAllWithoutRoot()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_admin WHERE CAST([grade] AS INT) >= 100;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithId(string id, string password)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_admin WHERE id = '{id}' AND password = '{password}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(AdminDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_admin (id, password, idnumber, name, rank, grade, auth) VALUES ('{dto.ADMIN_ID}', '{dto.ADMIN_PASSWORD}', '{dto.ADMIN_IDNUMBER}', '{dto.ADMIN_NAME}', '{dto.ADMIN_RANK}', '{dto.ADMIN_GRADE}', '{dto.ADMIN_AUTH}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Update(AdminDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"UPDATE tbl_admin SET password = '{dto.ADMIN_PASSWORD}', grade = '{dto.ADMIN_GRADE}', auth ='{dto.ADMIN_AUTH}' WHERE id = '{dto.ADMIN_ID}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Delete(int num)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"DELETE FROM tbl_admin WHERE num = '{num}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
