using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class StaffQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_staff;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithDept(string dept)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_staff WHERE dept = '{dept}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithIdnumber(string idnumber)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_staff WHERE idnumber = '{idnumber}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(StaffDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_staff (idnumber, dept, rank, name, address, tel, join_day, state) VALUES ('{dto.STAFF_IDNUMBER}', '{dto.STAFF_DEPT}', '{dto.STAFF_RANK}', '{dto.STAFF_NAME}', '{dto.STAFF_ADDRESS}', '{dto.STAFF_TEL}', '{dto.STAFF_JOIN_DAY}', '{dto.STAFF_STATE}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Delete(DeptDTO dto)
        {
            //SharedPreference.Instance.DBM.SqlConn.Open();
            //string query = $"DELETE FROM tbl_department WHERE code = {dto.DEPT_CODE};";
            //SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            //SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            //SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
