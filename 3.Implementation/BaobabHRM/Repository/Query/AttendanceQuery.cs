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
            string query = $"SELECT * FROM tbl_attendance WHERE (idnumber = '{idnumber}' AND (CAST([businessday] AS DATETIME) > '{twoWeeksAgo}') AND (CAST([businessday] AS DATETIME) < '{today}')) AND out_time IS NULL;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public SqlDataReader SelectWithToday(string idnumber, string today)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"SELECT * FROM tbl_attendance WHERE (idnumber = '{idnumber}' AND businessday = '{today}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(AttendanceDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_attendance (businessday, idnumber, in_time) VALUES ('{dto.ATTENDANCE_BUSINESS_DAY}', '{dto.ATTENDANCE_IDNUMBER}', '{dto.ATTENDANCE_IN_TIME}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void UpdateOutTime(ObservableCollection<AttendanceModel> attendanceList)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            foreach(var model in attendanceList)
            {
                string query = $"UPDATE tbl_attendance SET out_time = '{model.Dto.ATTENDANCE_OUT_TIME}' WHERE idnumber = '{model.Dto.ATTENDANCE_IDNUMBER}' AND businessday = '{model.Dto.ATTENDANCE_BUSINESS_DAY}';";
                SharedPreference.Instance.DBM.SqlComm.CommandText = query;
                SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            }
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
