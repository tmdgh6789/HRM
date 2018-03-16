using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WebEye.Controls.Wpf.StreamPlayerControl;

namespace BaobabHRM
{
    public class WorkspaceViewModel : BindableBase
    {
        #region property

        /// <summary>
        /// 오늘 출결 리스트
        /// </summary>
        private ObservableCollection<AttendanceModel> m_TodayAttendanceList;
        public ObservableCollection<AttendanceModel> TodayAttendanceList
        {
            get
            {
                if (m_TodayAttendanceList == null)
                {
                    m_TodayAttendanceList = new ObservableCollection<AttendanceModel>();
                }
                return m_TodayAttendanceList;
            }
            set
            {
                m_TodayAttendanceList = value;
                RaisePropertyChanged("TodayAttendanceList");
            }
        }

        /// <summary>
        /// 검색 시작 날짜
        /// </summary>
        private DateTime m_StartDate = DateTime.Now.AddDays(-14);
        public DateTime StartDate
        {
            get
            {
                return m_StartDate;
            }
            set
            {
                m_StartDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        /// <summary>
        /// 검색 끝 날짜
        /// </summary>
        private DateTime m_EndDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return m_EndDate;
            }
            set
            {
                m_EndDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        #endregion

        #region method

        private void LoadTodayAttendance()
        {
            TodayAttendanceList.Clear();
            try
            {
                var sqlData = new AttendanceQuery().SelectWithToday();
                while (sqlData.Read())
                {
                    AttendanceDTO dto = new AttendanceDTO
                    {
                        ATTENDANCE_BUSINESS_DAY = sqlData["businessday"].ToString(),
                        ATTENDANCE_NAME = sqlData["name"].ToString(),
                        ATTENDANCE_IDNUMBER = sqlData["idnumber"].ToString(),
                        ATTENDANCE_IN_TIME = sqlData["in_time"].ToString(),
                        ATTENDANCE_OUT_TIME = sqlData["out_time"].ToString(),
                        ATTENDANCE_OVERTIME = sqlData["overtime"].ToString(),
                        ATTENDANCE_OFF_DAY = sqlData["off_day"].ToString(),
                        ATTENDANCE_ETC = sqlData["etc"].ToString()
                    };
                    TodayAttendanceList.Add(new AttendanceModel(dto));
                }
                sqlData.Close();
                SharedPreference.Instance.DBM.SqlConn.Close();
                
                var sqlData2 = new AttendanceQuery().SelectTwoWeeksAgo();
                while (sqlData2.Read())
                {
                    AttendanceDTO dto = new AttendanceDTO
                    {
                        ATTENDANCE_BUSINESS_DAY = sqlData2["businessday"].ToString(),
                        ATTENDANCE_NAME = sqlData2["name"].ToString(),
                        ATTENDANCE_IDNUMBER = sqlData2["idnumber"].ToString(),
                        ATTENDANCE_IN_TIME = sqlData2["in_time"].ToString(),
                        ATTENDANCE_OUT_TIME = sqlData2["out_time"].ToString(),
                        ATTENDANCE_OVERTIME = sqlData2["overtime"].ToString(),
                        ATTENDANCE_OFF_DAY = sqlData2["off_day"].ToString(),
                        ATTENDANCE_ETC = sqlData2["etc"].ToString()
                    };
                    TodayAttendanceList.Add(new AttendanceModel(dto));
                }
                sqlData2.Close();
                SharedPreference.Instance.DBM.SqlConn.Close();
            }
            catch (Exception e)
            {
                SharedPreference.Instance.DBM.SqlConn.Close();

                MessageBox.Show("오늘 출결 상황을 읽어오지 못했습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
            }
        }

        public void InsertAttendance(SqlDataReader sqlData, AttendanceDTO dto, StreamPlayerControl streamPlayerControl)
        {
            var today = DateTime.Now;
            
            SqlDataReader sqlData2 = new AttendanceQuery().SelectWithIdnumberAndToday(dto.ATTENDANCE_IDNUMBER, today.ToString("yyyy-MM-dd"));
            // 오늘 이미 출근 기록이 있을 경우
            if (sqlData2.HasRows)
            {
                SharedPreference.Instance.DBM.SqlConn.Close();
                MessageBox.Show("오늘은 이미 출근 처리가 되어있습니다.");
            }
            else
            {
                try
                {
                    if (streamPlayerControl.IsPlaying)
                    {
                        var picture = streamPlayerControl.GetCurrentFrame();
                        var image = new BitmapToBitmapImageConverter().Convert(picture, null, null, null);
                        var popup = new CapturePopup();
                        //(popup.DataContext as CapturePopupViewModel).Message = "정상적으로 출근처리 되었습니다.\n즐거운 하루 되세요!";
                        //(popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                        if (WindowHelper.CreatePopup(popup, "출근", true) == true)
                        {
                            try
                            {
                                sqlData2.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                                new AttendanceQuery().Insert(dto);

                                TodayAttendanceList.Insert(0, new AttendanceModel(dto));

                                Defines.STAFF_PATH = Defines.CAPTURE_PATH + @"\Staff\" + SharedPreference.Instance.SelectedStaff.STAFF_NameAndIdnumber + @"\";
                                if (!Directory.Exists(Defines.STAFF_PATH))
                                {
                                    Directory.CreateDirectory(Defines.STAFF_PATH);
                                }
                                picture.Save(Defines.STAFF_PATH + today.ToString("yyyyMMdd") + "_Attendance_" + dto.ATTENDANCE_IDNUMBER + ".bmp");

                                try
                                {
                                    AttendanceLogDTO logDto = new AttendanceLogDTO()
                                    {
                                        ATTENDANCE_LOG_ADMIN = "출근",
                                        ATTENDANCE_LOG_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                                        ATTENDANCE_LOG_BUSINESSDAY = DateTime.Now.ToString("yyyy-MM-dd"),
                                        ATTENDANCE_LOG_WHAT = "출근",
                                        ATTENDANCE_LOG_LOG = "출근 시간: " + DateTime.Now.ToString("HH:mm:ss"),
                                        ATTENDANCE_LOG_REASON = "당일 출근 기록",
                                        ATTENDANCE_LOG_UPDATE_DATE = DateTime.Now.ToString("yyyy-MM-dd")
                                    };
                                    
                                    new AttendanceLogQuery().Insert(logDto);
                                }
                                catch (Exception e)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                    MessageBox.Show("기록을 남기는데 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                                }
                            }
                            catch (Exception e)
                            {
                                sqlData2.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                                MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                            }
                        }
                    }
                    else
                    {
                        sqlData2.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                        MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                }
            }
        }

        #endregion

        #region command

        /// <summary>
        /// 페이지 로드 커맨드
        /// </summary>
        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    LoadTodayAttendance();
                });
            }
        }


        public DelegateCommand SelectedClearCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    SharedPreference.Instance.SelectedDept = null;
                    SharedPreference.Instance.SelectedStaff = null;
                    SharedPreference.Instance.SelectedRank = null;
                });
            }
        }

        /// <summary>
        /// 출근 커맨드
        /// </summary>
        public DelegateCommand<StreamPlayerControl> AttendanceCommand
        {
            get
            {
                return new DelegateCommand<StreamPlayerControl>(delegate (StreamPlayerControl streamPlayerControl)
                {
                    try
                    {
                        if (SharedPreference.Instance.SelectedStaff != null)
                        {
                            var today = DateTime.Now;
                            var yesterday = today.AddDays(-14);
                            AttendanceDTO dto = new AttendanceDTO()
                            {
                                ATTENDANCE_BUSINESS_DAY = today.ToString("yyyy-MM-dd"),
                                ATTENDANCE_NAME = SharedPreference.Instance.SelectedStaff.STAFF_NAME,
                                ATTENDANCE_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                                ATTENDANCE_IN_TIME = DateTime.Now.ToString("HH:mm:ss")
                            };

                            SqlDataReader sqlData = new AttendanceQuery().SelectWithIdnumberAndBusinessDay(dto.ATTENDANCE_IDNUMBER, today.ToString("yyyy-MM-dd"), yesterday.ToString("yyyy-MM-dd"));
                            // 최근 2주 중 퇴근한 기록이 하나라도 없는 경우
                            if (sqlData.HasRows)
                            {
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                                var popup = new OutTimeCheckPopup();
                                if (WindowHelper.CreatePopup(popup, "퇴근시간 입력", true) == true)
                                {
                                    InsertAttendance(sqlData, dto, streamPlayerControl);
                                }
                            }
                            else
                            {
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();

                                InsertAttendance(sqlData ,dto, streamPlayerControl);
                            }
                        }
                        else
                        {
                            MessageBox.Show("출근 처리 할 사원을 선택해주세요.");
                        }
                    }
                    catch (Exception e)
                    {
                        SharedPreference.Instance.DBM.SqlConn.Close();

                        MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                    }
                });
            }
        }

        /// <summary>
        /// 퇴근 커맨드
        /// </summary>
        public DelegateCommand<StreamPlayerControl> LeaveWorkCommand
        {
            get
            {
                return new DelegateCommand<StreamPlayerControl>(delegate (StreamPlayerControl streamPlayerControl)
                {
                    try
                    {
                        if (SharedPreference.Instance.SelectedStaff != null)
                        {
                            var today = DateTime.Now;
                            AttendanceDTO dto = new AttendanceDTO()
                            {
                                ATTENDANCE_BUSINESS_DAY = today.ToString("yyyy-MM-dd"),
                                ATTENDANCE_NAME = SharedPreference.Instance.SelectedStaff.STAFF_NAME,
                                ATTENDANCE_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                                ATTENDANCE_IN_TIME = DateTime.Now.ToString("HH:mm:ss")
                            };
                            var sqlData = new AttendanceQuery().SelectWithIdnumberAndToday(dto.ATTENDANCE_IDNUMBER, today.ToString("yyyy-MM-dd"));
                            if (sqlData.HasRows)
                            {
                                sqlData.Read();
                                if (sqlData["out_time"].ToString() == "" || sqlData["out_time"] == null)
                                {
                                    try
                                    {
                                        if (streamPlayerControl.IsPlaying)
                                        {
                                            var picture = streamPlayerControl.GetCurrentFrame();
                                            var image = new BitmapToBitmapImageConverter().Convert(picture, null, null, null);
                                            var popup = new CapturePopup();
                                            //(popup.DataContext as CapturePopupViewModel).Message = "정상적으로 퇴근처리 되었습니다.\n오늘 하루도 수고하셨습니다!";
                                            //(popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                                            if (WindowHelper.CreatePopup(popup, "퇴근", true) == true)
                                            {
                                                sqlData.Close();
                                                SharedPreference.Instance.DBM.SqlConn.Close();

                                                new AttendanceQuery().UpdateOutTime(dto, today.ToString("HH:mm:ss"));

                                                var selectedStaff = TodayAttendanceList.Where(p => p.ATTENDANCE_IDNUMBER == SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER).FirstOrDefault();
                                                selectedStaff.ATTENDANCE_OUT_TIME = today.ToString("HH:mm:ss");
                                                var index = TodayAttendanceList.IndexOf(selectedStaff);

                                                TodayAttendanceList.Remove(selectedStaff);

                                                TodayAttendanceList.Insert(index, selectedStaff);

                                                Defines.STAFF_PATH = Defines.CAPTURE_PATH + @"\Staff\" + SharedPreference.Instance.SelectedStaff.STAFF_NameAndIdnumber + @"\";
                                                if (!Directory.Exists(Defines.STAFF_PATH))
                                                {
                                                    Directory.CreateDirectory(Defines.STAFF_PATH);
                                                }
                                                
                                                picture.Save(Defines.STAFF_PATH + today.ToString("yyyyMMdd") + "_LeaveWork_" + dto.ATTENDANCE_IDNUMBER + ".bmp");

                                                try
                                                {
                                                    AttendanceLogDTO logDto = new AttendanceLogDTO()
                                                    {
                                                        ATTENDANCE_LOG_ADMIN = "출근",
                                                        ATTENDANCE_LOG_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                                                        ATTENDANCE_LOG_BUSINESSDAY = DateTime.Now.ToString("yyyy-MM-dd"),
                                                        ATTENDANCE_LOG_WHAT = "출근",
                                                        ATTENDANCE_LOG_LOG = "출근 시간: " + DateTime.Now.ToString("HH:mm:ss"),
                                                        ATTENDANCE_LOG_REASON = "당일 출근 기록",
                                                        ATTENDANCE_LOG_UPDATE_DATE = DateTime.Now.ToString("yyyy-MM-dd")
                                                    };

                                                    new AttendanceLogQuery().Insert(logDto);
                                                }
                                                catch (Exception e)
                                                {
                                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                                    MessageBox.Show("기록을 남기는데 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            sqlData.Close();
                                            SharedPreference.Instance.DBM.SqlConn.Close();
                                            MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show("퇴근 처리를 실패했습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                                    }
                                }
                                else
                                {
                                    // warning
                                }
                            }
                            sqlData.Close();
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                        else
                        {
                            MessageBox.Show("퇴근 처리 할 사원을 선택해주세요.");
                        }
                    }
                    catch (Exception e)
                    {
                        SharedPreference.Instance.DBM.SqlConn.Close();

                        MessageBox.Show("퇴근 처리를 실패했습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                    }
                });
            }
        }

        /// <summary>
        /// 관리자 로그인 커맨드
        /// </summary>
        public DelegateCommand<StreamPlayerControl> LoginCommand
        {
            get
            {
                return new DelegateCommand<StreamPlayerControl>(delegate (StreamPlayerControl streamPlayerControl)
                {
                    if (streamPlayerControl.IsPlaying)
                    {
                        var picture = streamPlayerControl.GetCurrentFrame();
                        var image = new BitmapToBitmapImageConverter().Convert(picture, null, null, null);
                        var popup = new CapturePopup();
                        //(popup.DataContext as CapturePopupViewModel).Message = "관리자 로그인을 시도하셨습니다.";
                        //(popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                        if (WindowHelper.CreatePopup(popup, "관리자 로그인", true) == true)
                        {
                            try
                            {
                            Defines.ADMIN_PATH = Defines.CAPTURE_PATH + @"\Admin\" + DateTime.Now.ToString("yyyyMMdd");
                                if (!Directory.Exists(Defines.ADMIN_PATH))
                                {
                                    Directory.CreateDirectory(Defines.ADMIN_PATH);
                                }
                                picture.Save(Defines.ADMIN_PATH + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");

                                if (SharedPreference.Instance.IsLoginCompleted == false)
                                {
                                    var popup2 = new LoginPopup();
                                    if (WindowHelper.CreatePopup(popup2, "관리자 로그인", true) == true)
                                    {
                                        //var vm = popup.DataContext as LoginPopupViewModel;

                                    }
                                }
                                else
                                {
                                    var popup3 = new ManagementPopup();
                                    if (WindowHelper.CreatePopup(popup3, "관리자", true) == true)
                                    {
                                        var vm = popup3.DataContext as ManagementPopupViewModel;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.");
                    }
                });
            }
        }

        /// <summary>
        /// 관리자 로그아웃 커맨드
        /// </summary>
        public DelegateCommand LogoutCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (MessageBox.Show("로그아웃 하시겠습니까?", "로그아웃", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SharedPreference.Instance.Logout();
                    }
                });
            }
        }

        /// <summary>
        /// 검색 커맨드
        /// </summary>
        public DelegateCommand SerchCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (SharedPreference.Instance.SelectedDept != null)
                    {
                        if (SharedPreference.Instance.SelectedStaff == null)
                        {
                            TodayAttendanceList.Clear();
                            var sqlData = new AttendanceQuery().SelectWithDeptAndDay(SharedPreference.Instance.SelectedDept.DEPT_CODE, StartDate, EndDate);
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    AttendanceDTO dto = new AttendanceDTO
                                    {
                                        ATTENDANCE_BUSINESS_DAY = sqlData["businessday"].ToString(),
                                        ATTENDANCE_NAME = sqlData["name"].ToString(),
                                        ATTENDANCE_IDNUMBER = sqlData["idnumber"].ToString(),
                                        ATTENDANCE_IN_TIME = sqlData["in_time"].ToString(),
                                        ATTENDANCE_OUT_TIME = sqlData["out_time"].ToString(),
                                        ATTENDANCE_OVERTIME = sqlData["overtime"].ToString(),
                                        ATTENDANCE_OFF_DAY = sqlData["off_day"].ToString(),
                                        ATTENDANCE_ETC = sqlData["etc"].ToString()
                                    };
                                    TodayAttendanceList.Add(new AttendanceModel(dto));
                                }
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                            else
                            {
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                        else if (SharedPreference.Instance.SelectedStaff != null)
                        {
                            TodayAttendanceList.Clear();
                            var sqlData = new AttendanceQuery().SelectWithIdnumberAndDay(SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER, StartDate, EndDate);
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    AttendanceDTO dto = new AttendanceDTO
                                    {
                                        ATTENDANCE_BUSINESS_DAY = sqlData["businessday"].ToString(),
                                        ATTENDANCE_NAME = sqlData["name"].ToString(),
                                        ATTENDANCE_IDNUMBER = sqlData["idnumber"].ToString(),
                                        ATTENDANCE_IN_TIME = sqlData["in_time"].ToString(),
                                        ATTENDANCE_OUT_TIME = sqlData["out_time"].ToString(),
                                        ATTENDANCE_OVERTIME = sqlData["overtime"].ToString(),
                                        ATTENDANCE_OFF_DAY = sqlData["off_day"].ToString(),
                                        ATTENDANCE_ETC = sqlData["etc"].ToString()
                                    };
                                    TodayAttendanceList.Add(new AttendanceModel(dto));
                                }
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                            else
                            {
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    else
                    {
                        TodayAttendanceList.Clear();
                        StartDate = DateTime.Now;
                        var sqlData = new AttendanceQuery().SelectWithAllStaffAndDay(StartDate, EndDate);
                        if (sqlData.HasRows)
                        {
                            while (sqlData.Read())
                            {
                                AttendanceDTO dto = new AttendanceDTO
                                {
                                    ATTENDANCE_BUSINESS_DAY = sqlData["businessday"].ToString(),
                                    ATTENDANCE_NAME = sqlData["name"].ToString(),
                                    ATTENDANCE_IDNUMBER = sqlData["idnumber"].ToString(),
                                    ATTENDANCE_IN_TIME = sqlData["in_time"].ToString(),
                                    ATTENDANCE_OUT_TIME = sqlData["out_time"].ToString(),
                                    ATTENDANCE_OVERTIME = sqlData["overtime"].ToString(),
                                    ATTENDANCE_OFF_DAY = sqlData["off_day"].ToString(),
                                    ATTENDANCE_ETC = sqlData["etc"].ToString()
                                };
                                TodayAttendanceList.Add(new AttendanceModel(dto));
                            }
                            sqlData.Close();
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                        else
                        {
                            sqlData.Close();
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 부서 선택할 때
        /// </summary>
        public DelegateCommand SelectionDeptChanged
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    SharedPreference.Instance.StaffList.Clear();

                    if (SharedPreference.Instance.SelectedDept != null)
                    {
                        var sqlData = new StaffQuery().SelectWithDept(SharedPreference.Instance.SelectedDept.DEPT_CODE);
                        while (sqlData.Read())
                        {
                            var dto = new StaffDTO()
                            {
                                STAFF_IDNUMBER = sqlData["idnumber"].ToString(),
                                STAFF_DEPT = sqlData["dept"].ToString(),
                                STAFF_RANK = sqlData["rank"].ToString(),
                                STAFF_NAME = sqlData["name"].ToString(),
                                STAFF_ADDRESS = sqlData["address"].ToString(),
                                STAFF_TEL = sqlData["tel"].ToString(),
                                STAFF_JOIN_DAY = sqlData["join_day"].ToString(),
                                STAFF_RETIREMENT_DAY = sqlData["retirement_day"].ToString(),
                                STAFF_STATE = sqlData["state"].ToString()
                            };
                            SharedPreference.Instance.StaffList.Add(new StaffModel(dto));
                        }
                        var list = SharedPreference.Instance.StaffList.OrderBy(p => p.STAFF_IDNUMBER);
                        SharedPreference.Instance.StaffList = new ObservableCollection<StaffModel>(list);
                        sqlData.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                    }
                });
            }
        }
        
        public DelegateCommand<StreamPlayerControl> StreamPlayerReload
        {
            get
            {
                return new DelegateCommand<StreamPlayerControl>(delegate (StreamPlayerControl streamPlayerControl)
                {
                    if (streamPlayerControl.IsPlaying)
                    {
                        streamPlayerControl.Stop();
                    }
                    streamPlayerControl.StartPlay(new Uri("rtmp://61.72.187.6/oflaDemo/testStream"));
                });
            }
        }


        #endregion
    }
}
