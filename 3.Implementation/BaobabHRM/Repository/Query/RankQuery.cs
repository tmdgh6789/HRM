using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class RankQuery
    {
        public SqlDataReader SelectAll()
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = "SELECT * FROM tbl_rank;";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SqlDataReader sd;
            sd = SharedPreference.Instance.DBM.SqlComm.ExecuteReader();
            return sd;
        }

        public void Insert(RankDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"INSERT INTO tbl_rank (code, name) VALUES ('{dto.RANK_CODE}', '{dto.RANK_NAME}');";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Update(RankDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"UPDATE tbl_rank SET name = '{dto.RANK_NAME}' WHERE code = '{dto.RANK_CODE}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }

        public void Delete(RankDTO dto)
        {
            SharedPreference.Instance.DBM.SqlConn.Open();
            string query = $"DELETE FROM tbl_rank WHERE code = '{dto.RANK_CODE}';";
            SharedPreference.Instance.DBM.SqlComm.CommandText = query;
            SharedPreference.Instance.DBM.SqlComm.ExecuteNonQuery();
            SharedPreference.Instance.DBM.SqlConn.Close();
        }
    }
}
