using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class StaffDTO
    {
        public string STAFF_IDNUMBER { get; set; }
        public int STAFF_DEPT { get; set; }
        public int STAFF_RANK { get; set; }
        public string STAFF_NAME { get; set; }
        public string STAFF_ADDRESS { get; set; }
        public string STAFF_TEL { get; set; }
        public string STAFF_JOIN_DAY { get; set; }
        public string STAFF_RETIREMENT_DAY { get; set; }
        public string STAFF_STATE { get; set; }
    }
}
