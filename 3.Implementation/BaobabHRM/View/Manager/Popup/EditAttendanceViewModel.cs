using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class EditAttendanceViewModel : BindableBase
    {
        #region property

        /// <summary>
        /// 시작 날짜 체크박스
        /// </summary>
        private bool m_IsAttendanceTime;
        public bool IsAttendanceTime
        {
            get
            {
                return m_IsAttendanceTime;
            }
            set
            {
                m_IsAttendanceTime = value;
                RaisePropertyChanged("IsAttendanceTime");
            }
        }
        
        /// <summary>
        /// 끝 날짜 체크박스
        /// </summary>
        private bool m_IsLeaveWorkTime;
        public bool IsLeaveWorkTime
        {
            get
            {
                return m_IsLeaveWorkTime;
            }
            set
            {
                m_IsLeaveWorkTime = value;
                RaisePropertyChanged("IsLeaveWorkTime");
            }
        }

        /// <summary>
        /// 출근 시간
        /// </summary>
        private string m_AttendanceTime;
        public string AttendanceTime
        {
            get
            {
                return ASelectedTime;
            }
            set
            {
                m_AttendanceTime = value;
                RaisePropertyChanged("AttendanceTime");
            }
        }

        /// <summary>
        /// 퇴근 시간
        /// </summary>
        private string m_LeaveWorkTime;
        public string LeaveWorkTime
        {
            get
            {
                return ASelectedTime;
            }
            set
            {
                m_LeaveWorkTime = value;
                RaisePropertyChanged("LeaveWorkTime");
            }
        }

        /// <summary>
        /// 사유
        /// </summary>
        private string m_Reason;
        public string Reason
        {
            get
            {
                return m_Reason;
            }
            set
            {
                m_Reason = value;
                RaisePropertyChanged("Reason");
            }
        }

        #region attendance time

        #region property

        private string m_ASelectedTime = DateTime.Now.ToString("HH:mm:ss");
            public string ASelectedTime
            {
                get
                {
                    return m_ASelectedTime;
                }
                set
                {
                    m_ASelectedTime = value;
                    RaisePropertyChanged("ASelectedTime");
                }
            }


            public string ADisplayTimeHours
            {
                get
                {
                    var hours = m_ACurrentTime.Hour;
                    return hours > 24 ? (hours - 24).ToString("00") : hours.ToString("00");
                }
                set
                {
                    var hour = 0;
                    Int32.TryParse(value, out hour);
                    ACurrentTime = ACurrentTime.AddHours(hour);
                    RaisePropertyChanged("ADisplayTimeHours");
                    RaisePropertyChanged("ADisplayTimeMinutes");
                }
            }

            public string ADisplayTimeMinutes
            {
                get
                {
                    return m_ACurrentTime.Minute.ToString("00"); ;
                }
                set
                {
                    var minutes = 0;
                    Int32.TryParse(value, out minutes);
                    ACurrentTime = ACurrentTime.AddMinutes(minutes);
                    RaisePropertyChanged("ADisplayTimeHours");
                    RaisePropertyChanged("ADisplayTimeMinutes");
                }
            }

            private DateTime m_ACurrentTime = DateTime.Now;
            public DateTime ACurrentTime
            {
                get
                {
                    return m_ACurrentTime;
                }
                set
                {
                    m_ACurrentTime = value;
                    ASelectedTime = value.ToString("HH:mm:ss");
                    RaisePropertyChanged("ACurrentTime");
                    RaisePropertyChanged("ADisplayTimeHours");
                    RaisePropertyChanged("ADisplayTimeMinutes");
                }
            }

            #endregion

            #region command

            public DelegateCommand ClickAHourUpCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        ACurrentTime = ACurrentTime.AddHours(1);
                        ASelectedTime = ACurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickAHourDownCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        ACurrentTime = ACurrentTime.AddHours(-1);
                        ASelectedTime = ACurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickAMinutesUpCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        ACurrentTime = ACurrentTime.AddMinutes(1);
                        ASelectedTime = ACurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickAMinutesDownCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        ACurrentTime = ACurrentTime.AddMinutes(-1);
                        ASelectedTime = ACurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

        #endregion

        #endregion

        #region leave work time

            #region property

            private string m_LSelectedTime = DateTime.Now.ToString("HH:mm:ss");
            public string LSelectedTime
            {
                get
                {
                    return m_LSelectedTime;
                }
                set
                {
                    m_LSelectedTime = value;
                    RaisePropertyChanged("LSelectedTime");
                }
            }


            public string LDisplayTimeHours
            {
                get
                {
                    var hours = m_LCurrentTime.Hour;
                    return hours > 24 ? (hours - 24).ToString("00") : hours.ToString("00");
                }
                set
                {
                    var hour = 0;
                    Int32.TryParse(value, out hour);
                    LCurrentTime = LCurrentTime.AddHours(hour);
                    RaisePropertyChanged("LDisplayTimeHours");
                    RaisePropertyChanged("LDisplayTimeMinutes");
                }
            }

            public string LDisplayTimeMinutes
            {
                get
                {
                    return m_LCurrentTime.Minute.ToString("00"); ;
                }
                set
                {
                    var minutes = 0;
                    Int32.TryParse(value, out minutes);
                    LCurrentTime = LCurrentTime.AddMinutes(minutes);
                    RaisePropertyChanged("LADisplayTimeHours");
                    RaisePropertyChanged("LDisplayTimeMinutes");
                }
            }

            private DateTime m_LCurrentTime = DateTime.Now;
            public DateTime LCurrentTime
            {
                get
                {
                    return m_LCurrentTime;
                }
                set
                {
                    m_LCurrentTime = value;
                    LSelectedTime = value.ToString("HH:mm:ss");
                    RaisePropertyChanged("LCurrentTime");
                    RaisePropertyChanged("LDisplayTimeHours");
                    RaisePropertyChanged("LDisplayTimeMinutes");
                }
            }

            #endregion

            #region command

            public DelegateCommand ClickLHourUpCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        LCurrentTime = LCurrentTime.AddHours(1);
                        LSelectedTime = LCurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickLHourDownCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        LCurrentTime = LCurrentTime.AddHours(-1);
                        LSelectedTime = LCurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickLMinutesUpCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        LCurrentTime = LCurrentTime.AddMinutes(1);
                        LSelectedTime = LCurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

            public DelegateCommand ClickLMinutesDownCommand
            {
                get
                {
                    return new DelegateCommand(delegate ()
                    {
                        LCurrentTime = LCurrentTime.AddMinutes(-1);
                        LSelectedTime = LCurrentTime.ToString("HH:mm:ss");
                    });
                }
            }

        #endregion

        #endregion

        #endregion

        #region method

        public void LoadData()
        {

        }

        #endregion

        #region command


        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    ADisplayTimeHours = "-" + DateTime.Now.Hour.ToString();
                    ADisplayTimeHours = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IN_TIME.Substring(0, 2);
                    ADisplayTimeMinutes = "-" + DateTime.Now.Minute.ToString();
                    ADisplayTimeMinutes = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IN_TIME.Substring(3, 2);
                    
                    if (SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OUT_TIME.Length != 0 || SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OUT_TIME != "")
                    {
                        LDisplayTimeHours = "-" + DateTime.Now.Hour.ToString();
                        LDisplayTimeHours = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OUT_TIME.Substring(0, 2);
                        LDisplayTimeMinutes = "-" + DateTime.Now.Minute.ToString();
                        LDisplayTimeMinutes = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OUT_TIME.Substring(3, 2);
                    }
                });
            }
        }


        /// <summary>
        /// 취소 커맨드
        /// </summary>
        public DelegateCommand<UserControl> CancelCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    Window.GetWindow(uc).DialogResult = false;
                });
            }
        }

        /// <summary>
        /// 확인 커맨드
        /// </summary>
        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    AttendanceDTO beforeDto = new AttendanceDTO
                    {
                        ATTENDANCE_BUSINESS_DAY = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_BUSINESS_DAY,
                        ATTENDANCE_NAME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_NAME,
                        ATTENDANCE_IDNUMBER = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IDNUMBER,
                        ATTENDANCE_IN_TIME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IN_TIME,
                        ATTENDANCE_OUT_TIME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OUT_TIME,
                        ATTENDANCE_OVERTIME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OVERTIME,
                        ATTENDANCE_OFF_DAY = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OFF_DAY,
                        ATTENDANCE_ETC = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_ETC
                    };

                    AttendanceDTO afterDto = new AttendanceDTO
                    {
                        ATTENDANCE_BUSINESS_DAY = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_BUSINESS_DAY,
                        ATTENDANCE_NAME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_NAME,
                        ATTENDANCE_IDNUMBER = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IDNUMBER,
                        ATTENDANCE_IN_TIME = AttendanceTime,
                        ATTENDANCE_OUT_TIME = LeaveWorkTime,
                        ATTENDANCE_OVERTIME = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OVERTIME,
                        ATTENDANCE_OFF_DAY = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_OFF_DAY,
                        ATTENDANCE_ETC = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_ETC
                    };

                    string idnumber = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IDNUMBER;
                    string businessDay = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_BUSINESS_DAY;
                    string what = "";
                    string log = "";

                    if (IsAttendanceTime)
                    {
                        if (AttendanceTime == null || AttendanceTime.Length == 0)
                        {
                            MessageBox.Show("출근시간을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        what += "출근시간 ";
                        log += $"출근시간: {beforeDto.ATTENDANCE_IN_TIME} -> {afterDto.ATTENDANCE_IN_TIME} ";
                    }
                    if (IsLeaveWorkTime)
                    {
                        if (LeaveWorkTime == null || LeaveWorkTime.Length == 0)
                        {
                            MessageBox.Show("퇴근시간을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        what += "퇴근시간 ";
                        log += $"퇴근시간: {beforeDto.ATTENDANCE_OUT_TIME} -> {afterDto.ATTENDANCE_OUT_TIME} ";
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    try
                    {

                        new AttendanceQuery().Update(afterDto);

                        try
                        {
                            AttendanceLogDTO logDto = new AttendanceLogDTO
                            {
                                ATTENDANCE_LOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                ATTENDANCE_LOG_IDNUMBER = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_IDNUMBER,
                                ATTENDANCE_LOG_BUSINESSDAY = SharedPreference.Instance.SelectedAttendance.ATTENDANCE_BUSINESS_DAY,
                                ATTENDANCE_LOG_WHAT = what,
                                ATTENDANCE_LOG_LOG = log,
                                ATTENDANCE_LOG_REASON = Reason,
                                ATTENDANCE_LOG_UPDATE_DATE = DateTime.Now.ToString()
                            };

                            new AttendanceLogQuery().Insert(logDto);

                            MessageBox.Show("사원 정보를 수정하셨습니다.");
                            Reason = "";
                        }
                        catch (Exception e)
                        {
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                            MessageBox.Show("출결 정보 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                        {
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                        MessageBox.Show("출결 정보 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                    }

                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        #endregion
    }
}
