using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WebEye.Controls.Wpf.StreamPlayerControl;

namespace BaobabHRM
{
    public class UserViewModel : BindableBase
    {
        #region property

        private ObservableCollection<string> m_ListViewer;
        public ObservableCollection<string> ListViewer
        {
            get
            {
                if (m_ListViewer == null)
                {
                    m_ListViewer = new ObservableCollection<string>();
                    m_ListViewer.Add("DataGrid에 그리기 위한 데이터");
                }
                return m_ListViewer;
            }
            set
            {
                m_ListViewer = value;
                RaisePropertyChanged("ListViewer");
            }
        }

        private string m_Today;
        public string Today
        {
            get
            {
                return DateTime.Now.ToString("yyyy년 MM월 dd일 dddd");
            }
            set
            {
                m_Today = value;
                RaisePropertyChanged("Today");
            }
        }

        private string m_CurrentTime;
        public string CurrentTime
        {
            get
            {
                return m_CurrentTime;
            }
            set
            {
                if (m_CurrentTime == value)
                {
                    return;
                }
                m_CurrentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }

        private DispatcherTimer m_Timer;
        public DispatcherTimer Timer
        {
            get
            {
                return m_Timer;
            }
            set
            {
                m_Timer = value;
                RaisePropertyChanged("Timer");
            }
        }


        #endregion

        #region method

        public void StartClock()
        {
            Timer = new DispatcherTimer(DispatcherPriority.Render);
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            };
            Timer.Start();
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
                        (popup.DataContext as CapturePopupViewModel).Message = "정상적으로 출근처리 되었습니다.\n즐거운 하루 되세요!";
                        (popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                        if (WindowHelper.CreatePopup(popup, "출근", true) == true)
                        {
                            try
                            {
                                sqlData2.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                                new AttendanceQuery().Insert(dto);
                                
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
                    StartClock();
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
                        sqlData.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                    }
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

                                InsertAttendance(sqlData, dto, streamPlayerControl);
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
                                            (popup.DataContext as CapturePopupViewModel).Message = "정상적으로 퇴근처리 되었습니다.\n오늘 하루도 수고하셨습니다!";
                                            (popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                                            if (WindowHelper.CreatePopup(popup, "퇴근", true) == true)
                                            {
                                                sqlData.Close();
                                                SharedPreference.Instance.DBM.SqlConn.Close();

                                                new AttendanceQuery().UpdateOutTime(dto, today.ToString("HH:mm:ss"));

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


        public DelegateCommand InquiryCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    var popup = new InquiryPopup();
                    if (WindowHelper.CreatePopup(popup, "출근", true) == true)
                    {

                    }
                });
            }
        }


        /// <summary>
        /// 캠 다시 실행 커맨드
        /// </summary>
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
