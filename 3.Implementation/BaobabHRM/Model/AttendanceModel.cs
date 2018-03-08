using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AttendanceModel : BindableBase
    {
        public AttendanceModel(AttendanceDTO dto)
        {
            this.Dto = dto;
        }

        private AttendanceDTO m_Dto;
        public AttendanceDTO Dto
        {
            get
            {
                return m_Dto;
            }
            set
            {
                m_Dto = value;
                RaisePropertyChanged("Dto");
            }
        }

        public string ATTENDANCE_BUSINESS_DAY
        {
            get
            {
                return Dto.ATTENDANCE_BUSINESS_DAY;
            }
            set
            {
                Dto.ATTENDANCE_BUSINESS_DAY = value;
                RaisePropertyChanged("ATTENDANCE_BUSINESS_DAY");
            }
        }

        public string ATTENDANCE_IDNUMBER
        {
            get
            {
                return Dto.ATTENDANCE_IDNUMBER;
            }
            set
            {
                Dto.ATTENDANCE_IDNUMBER = value;
                RaisePropertyChanged("ATTENDANCE_IDNUMBER");
            }
        }

        public string ATTENDANCE_IN_TIME
        {
            get
            {
                return Dto.ATTENDANCE_IN_TIME;
            }
            set
            {
                Dto.ATTENDANCE_IN_TIME = value;
                RaisePropertyChanged("ATTENDANCE_IN_TIME");
            }
        }

        public string ATTENDANCE_OUT_TIME
        {
            get
            {
                return Dto.ATTENDANCE_OUT_TIME;
            }
            set
            {
                Dto.ATTENDANCE_OUT_TIME = value;
                RaisePropertyChanged("ATTENDANCE_OUT_TIME");
            }
        }

        private string m_ATTENDANCE_OUT_TIMEstr;
        public string ATTENDANCE_OUT_TIMEstr
        {
            get
            {
                return m_ATTENDANCE_OUT_TIMEstr;
            }
            set
            {
                m_ATTENDANCE_OUT_TIMEstr = value;
                RaisePropertyChanged("ATTENDANCE_OUT_TIMEstr");
            }
        }


        public string ATTENDANCE_OVERTIME
        {
            get
            {
                return Dto.ATTENDANCE_OVERTIME;
            }
            set
            {
                Dto.ATTENDANCE_OVERTIME = value;
                RaisePropertyChanged("ATTENDANCE_OVERTIME");
            }
        }

        public string ATTENDANCE_OFF
        {
            get
            {
                return Dto.ATTENDANCE_OFF;
            }
            set
            {
                Dto.ATTENDANCE_OFF = value;
                RaisePropertyChanged("ATTENDANCE_OFF");
            }
        }
        
        public string ATTENDANCE_ETC
        {
            get
            {
                return Dto.ATTENDANCE_ETC;
            }
            set
            {
                Dto.ATTENDANCE_ETC = value;
                RaisePropertyChanged("ATTENDANCE_ETC");
            }
        }
    }
}
