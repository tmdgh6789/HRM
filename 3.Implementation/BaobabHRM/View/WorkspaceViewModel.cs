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

        private string m_id;
        public string Id
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string m_password;
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
                RaisePropertyChanged("Password");
            }
        }
        #endregion

        #region method

        #endregion

        #region command

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
                                ATTENDANCE_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                                ATTENDANCE_IN_TIME = DateTime.Now.ToString("hh:mm:ss")
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
                                    var vm = popup.DataContext as OutTimeCheckPopupViewModel;

                                    sqlData.Close();
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                    SqlDataReader sqlData2 = new AttendanceQuery().SelectWithToday(dto.ATTENDANCE_IDNUMBER, today.ToString("yyyy-MM-dd"));
                                    // 오늘 이미 출근 기록이 있을 경우
                                    if (sqlData2.HasRows)
                                    {
                                        MessageBox.Show("이미 출근 처리 되었습니다.");
                                    }
                                    else
                                    {
                                        sqlData2.Close();
                                        SharedPreference.Instance.DBM.SqlConn.Close();
                                        // TODO 출근 찍기
                                        try
                                        {
                                            if (streamPlayerControl.IsPlaying)
                                            {
                                                var image = new BitmapToBitmapImageConverter().Convert(streamPlayerControl.GetCurrentFrame(), null, null, null);
                                                var popup2 = new CapturePopup();
                                                (popup2.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                                                if (WindowHelper.CreatePopup(popup2, "출근", true) == true)
                                                {
                                                    new AttendanceQuery().Insert(dto);

                                                    Defines.STAFF_PATH = Defines.CAPTURE_PATH + @"\" + SharedPreference.Instance.SelectedStaff.STAFF_NameAndIdnumber + @"\";
                                                    if (!Directory.Exists(Defines.STAFF_PATH))
                                                    {
                                                        Directory.CreateDirectory(Defines.STAFF_PATH);
                                                    }
                                                    streamPlayerControl.GetCurrentFrame().Save(Defines.STAFF_PATH + today.ToString("yyyyMMdd") + "_" + dto.ATTENDANCE_IDNUMBER + ".bmp");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.");
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sqlData.Close();
                                SharedPreference.Instance.DBM.SqlConn.Close();
                                SqlDataReader sqlData2 = new AttendanceQuery().SelectWithToday(dto.ATTENDANCE_IDNUMBER, today.ToString("yyyy-MM-dd"));
                                // 오늘 이미 출근 기록이 있을 경우
                                if (sqlData2.HasRows)
                                {
                                    MessageBox.Show("이미 출근 처리 되었습니다.");
                                }
                                else
                                {
                                    sqlData2.Close();
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                    // TODO 출근 찍기
                                    try
                                    {
                                        if (streamPlayerControl.IsPlaying)
                                        {
                                            var image = new BitmapToBitmapImageConverter().Convert(streamPlayerControl.GetCurrentFrame(), null, null, null);
                                            var popup = new CapturePopup();
                                            (popup.DataContext as CapturePopupViewModel).Image = image as BitmapImage;
                                            if (WindowHelper.CreatePopup(popup, "출근", true) == true)
                                            {
                                                new AttendanceQuery().Insert(dto);
                                                
                                                Defines.STAFF_PATH = Defines.CAPTURE_PATH + @"\" + SharedPreference.Instance.SelectedStaff.STAFF_NameAndIdnumber + @"\";
                                                if (!Directory.Exists(Defines.STAFF_PATH))
                                                {
                                                    Directory.CreateDirectory(Defines.STAFF_PATH);
                                                }
                                                streamPlayerControl.GetCurrentFrame().Save(Defines.STAFF_PATH + today.ToString("yyyyMMdd") + "_" + dto.ATTENDANCE_IDNUMBER + ".bmp");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("캠이 정상 작동중이지 않습니다. 관리자에게 문의하세요.");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                                    }
                                }
                            }

                            sqlData.Close();
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                        else
                        {
                            MessageBox.Show("출근 처리 할 사원을 선택해주세요.");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("출근 실패하셨습니다. 관리자에게 문의하세요.\n에러내용 : " + e.Message);
                    }
                });
            }
        }

        /// <summary>
        /// 관리자 로그인 커맨드
        /// </summary>
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (SharedPreference.Instance.IsLoginCompleted == false)
                    {
                        var popup = new LoginPopup();
                        if (WindowHelper.CreatePopup(popup, "관리자 로그인", true) == true)
                        {
                            var vm = popup.DataContext as LoginPopupViewModel;
                        }
                    }
                    else
                    {
                        var popup = new ManagementPopup();
                        if (WindowHelper.CreatePopup(popup, "관리자", true) == true)
                        {
                            var vm = popup.DataContext as ManagementPopupViewModel;
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
                        sqlData.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                    }
                });
            }
        }


        public DelegateCommand SelectionStaffChanged
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    var list = SharedPreference.Instance.StaffList.Where(p => p.STAFF_IDNUMBER == SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER);
                    SharedPreference.Instance.StaffViewStaffList = new ObservableCollection<StaffModel>(list);
                    //SharedPreference.Instance.StaffList.Clear();
                    //if (SharedPreference.Instance.SelectedDept != null)
                    //{
                    //    var sqlData = new StaffQuery().SelectWithIdnumber(SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER);
                    //    while (sqlData.Read())
                    //    {
                    //        var dto = new StaffDTO()
                    //        {
                    //            STAFF_IDNUMBER = sqlData["idnumber"].ToString(),
                    //            STAFF_DEPT = sqlData["dept"].ToString(),
                    //            STAFF_RANK = sqlData["rank"].ToString(),
                    //            STAFF_NAME = sqlData["name"].ToString(),
                    //            STAFF_ADDRESS = sqlData["address"].ToString(),
                    //            STAFF_TEL = sqlData["tel"].ToString(),
                    //            STAFF_JOIN_DAY = sqlData["join_day"].ToString(),
                    //            STAFF_RETIREMENT_DAY = sqlData["retirement_day"].ToString(),
                    //            STAFF_STATE = sqlData["state"].ToString()
                    //        };
                    //        SharedPreference.Instance.StaffList.Add(new StaffModel(dto));
                    //    }
                    //    sqlData.Close();
                    //    SharedPreference.Instance.DBM.SqlConn.Close();
                    //}
                });
            }
        }

        #endregion
    }
}
