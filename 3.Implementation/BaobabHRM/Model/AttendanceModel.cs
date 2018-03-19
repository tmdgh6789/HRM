using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        
        public string ATTENDANCE_NAME
        {
            get
            {
                return Dto.ATTENDANCE_NAME;
            }
            set
            {
                Dto.ATTENDANCE_NAME = value;
                RaisePropertyChanged("ATTENDANCE_NAME");
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

        public string ATTENDANCE_OFF_DAY
        {
            get
            {
                return Dto.ATTENDANCE_OFF_DAY;
            }
            set
            {
                Dto.ATTENDANCE_OFF_DAY = value;
                RaisePropertyChanged("ATTENDANCE_OFF_DAY");
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


        private string m_ATTENDANCE_NameAndIdnumber;
        public string ATTENDANCE_NameAndIdnumber
        {
            get
            {
                return Dto.ATTENDANCE_NAME + "(" + Dto.ATTENDANCE_IDNUMBER + ")";
            }
            set
            {
                m_ATTENDANCE_NameAndIdnumber = value;
                RaisePropertyChanged("ATTENDANCE_NameAndIdnumber");
            }
        }

        #region timepicker

        #region property

        private string m_SelectedTime = DateTime.Now.ToString("HH:mm:00");
        public string SelectedTime
        {
            get
            {
                return m_SelectedTime;
            }
            set
            {
                m_SelectedTime = value;
                RaisePropertyChanged("SelectedTime");
            }
        }

        
        public string DisplayTimeHours
        {
            get
            {
                var hours = m_CurrentTime.Hour;
                return hours > 24 ? (hours - 24).ToString("00") : hours.ToString("00");
            }
            set
            {
                var hour = 0;
                Int32.TryParse(value, out hour);
                CurrentTime = CurrentTime.AddHours(hour);
                RaisePropertyChanged("DisplayTimeHours");
                RaisePropertyChanged("DisplayTimeMinutes");
            }
        }
        
        public string DisplayTimeMinutes
        {
            get
            {
                return m_CurrentTime.Minute.ToString("00"); ;
            }
            set
            {
                var minutes = 0;
                Int32.TryParse(value, out minutes);
                CurrentTime = CurrentTime.AddMinutes(minutes);
                RaisePropertyChanged("DisplayTimeHours");
                RaisePropertyChanged("DisplayTimeMinutes");
            }
        }

        private DateTime m_CurrentTime = DateTime.Now;
        public DateTime CurrentTime
        {
            get
            {
                return m_CurrentTime;
            }
            set
            {
                m_CurrentTime = value;
                SelectedTime = value.ToString("HH:mm:00");
                RaisePropertyChanged("CurrentTime");
                RaisePropertyChanged("DisplayTimeHours");
                RaisePropertyChanged("DisplayTimeMinutes");
            }
        }

        #endregion

        #region command

        public DelegateCommand ClickHourUpCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    CurrentTime = CurrentTime.AddHours(1);
                    SelectedTime = CurrentTime.ToString("HH:mm:00");
                });
            }
        }

        public DelegateCommand ClickHourDownCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    CurrentTime = CurrentTime.AddHours(-1);
                    SelectedTime = CurrentTime.ToString("HH:mm:00");
                });
            }
        }

        public DelegateCommand ClickMinutesUpCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    CurrentTime = CurrentTime.AddMinutes(1);
                    SelectedTime = CurrentTime.ToString("HH:mm:00");
                });
            }
        }

        public DelegateCommand ClickMinutesDownCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    CurrentTime = CurrentTime.AddMinutes(-1);
                    SelectedTime = CurrentTime.ToString("HH:mm:00");
                });
            }
        }

        #endregion

        #endregion
    }
}
