using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AttendanceQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_attendance;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithIdnumberAndBusinessDay(string idnumber, string today, string twoWeeksAgo)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (idnumber = '{idnumber}' AND (CAST([businessday] AS DATETIME) > '{twoWeeksAgo}') AND (CAST([businessday] AS DATETIME) < '{today}')) AND (out_time IS NULL OR out_time = '');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithToday()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE CAST([businessday] AS DATETIME) = '{DateTime.Now.ToString("yyyy-MM-dd")}' ORDER BY num DESC;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectTwoWeeksAgo()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (CAST([businessday] AS DATETIME) >= '{DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd")}' AND CAST([businessday] AS DATETIME) < '{DateTime.Now.ToString("yyyy-MM-dd")}') AND (out_time IS NULL OR out_time = '') ORDER BY num DESC;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithIdnumberAndToday(string idnumber, string today)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (idnumber = '{idnumber}' AND CAST([businessday] AS DATETIME) = '{today}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithDeptAndDay(string dept, DateTime startDate, DateTime endDate)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT DISTINCT A.num, A.businessday, A.name, A.idnumber, A.in_time, A.out_time, A.overtime, A.off_day, A.etc FROM tbl_attendance AS A INNER JOIN tbl_staff AS S ON S.dept = '{dept}' ORDER BY num DESC;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithIdnumberAndDay(string idnumber, DateTime startDate, DateTime endDate)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (idnumber = '{idnumber}' AND (CAST([businessday] AS DATETIME) >= '{startDate.ToString("yyyy-MM-dd")}' AND CAST([businessday] AS DATETIME) <= '{endDate.ToString("yyyy-MM-dd")}')) ORDER BY num DESC;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithAllStaffAndDay(DateTime startDate, DateTime endDate)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (CAST([businessday] AS DATETIME) >= '{startDate.ToString("yyyy-MM-dd")}' AND CAST([businessday] AS DATETIME) <= '{endDate.ToString("yyyy-MM-dd")}') ORDER BY num DESC;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(AttendanceDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_attendance (businessday, name, idnumber, in_time) VALUES ('{dto.ATTENDANCE_BUSINESS_DAY}', '{dto.ATTENDANCE_NAME}', '{dto.ATTENDANCE_IDNUMBER}', '{dto.ATTENDANCE_IN_TIME}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Update(AttendanceDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"UPDATE tbl_attendance SET in_time = '{dto.ATTENDANCE_IN_TIME}', out_time = '{dto.ATTENDANCE_OUT_TIME}' WHERE idnumber = '{dto.ATTENDANCE_IDNUMBER}' AND businessday = '{dto.ATTENDANCE_BUSINESS_DAY}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void UpdateOutTime(AttendanceDTO dto, string outTime)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"UPDATE tbl_attendance SET out_time = '{outTime}' WHERE idnumber = '{dto.ATTENDANCE_IDNUMBER}' AND businessday = '{dto.ATTENDANCE_BUSINESS_DAY}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
