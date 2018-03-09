using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AttendanceDTO
    {
        public string ATTENDANCE_BUSINESS_DAY { get; set; }
        public string ATTENDANCE_NAME { get; set; }
        public string ATTENDANCE_IDNUMBER { get; set; }
        public string ATTENDANCE_IN_TIME { get; set; }
        public string ATTENDANCE_OUT_TIME { get; set; }
        public string ATTENDANCE_OVERTIME { get; set; }
        public string ATTENDANCE_OFF { get; set; }
        public string ATTENDANCE_ETC { get; set; }
    }
}
