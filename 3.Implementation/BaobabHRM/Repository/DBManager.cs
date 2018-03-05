using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class DBManager
    {
        
        public SqlConnection SqlConn { get; set; }
        public SqlCommand SqlComm = new SqlCommand();

        public void Init()
        {
            // Sql 연결정보(서버:127.0.0.1, 포트:1433, 아이디:sa, 비밀번호 : password, db : member)
            string connectionString = $"server = {Defines.SERVER_IP}, {Defines.SERVER_PORT}; uid = {Defines.SERVER_ID}; pwd = {Defines.SERVER_PW}; database = {Defines.DATABASE_NAME};";
            // Sql 새연결정보 생성
            SqlConn = new SqlConnection(connectionString);
            SqlComm.Connection = SqlConn;
        }
        
    }
}
