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
            if (!System.IO.Directory.Exists(Defines.LOCALREPOSITORY_PATH))
            {
                System.IO.Directory.CreateDirectory(Defines.LOCALREPOSITORY_PATH);
            }
            if (!System.IO.Directory.Exists(Defines.CAPTURE_PATH))
            {
                System.IO.Directory.CreateDirectory(Defines.CAPTURE_PATH);
            }

            // Sql 연결정보
            string connectionString = $"server = {Defines.SERVER_IP}, {Defines.SERVER_PORT}; uid = {Defines.SERVER_ID}; pwd = {Defines.SERVER_PW}; database = {Defines.DATABASE_NAME};";
            // Sql 새연결정보 생성
            SqlConn = new SqlConnection(connectionString);
            SqlComm.Connection = SqlConn;
        }
        
    }
}
