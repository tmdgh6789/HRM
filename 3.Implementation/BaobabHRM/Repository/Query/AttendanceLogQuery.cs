using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AttendanceLogQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_attendance_log;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(AttendanceLogDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_attendance_log (admin, idnumber, businessday, what, log, reason, update_date) VALUES ('{dto.ATTENDANCE_LOG_ADMIN}', '{dto.ATTENDANCE_LOG_IDNUMBER}', '{dto.ATTENDANCE_LOG_BUSINESSDAY}', '{dto.ATTENDANCE_LOG_WHAT}', '{dto.ATTENDANCE_LOG_LOG}', '{dto.ATTENDANCE_LOG_REASON}', '{dto.ATTENDANCE_LOG_UPDATE_DATE}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
