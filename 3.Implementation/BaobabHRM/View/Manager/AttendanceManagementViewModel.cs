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
    public class AttendanceManagementViewModel : BindableBase
    {
        #region property

        /// <summary>
        /// 오늘 출결 리스트
        /// </summary>
        private ObservableCollection<AttendanceModel> m_AttendanceList;
        public ObservableCollection<AttendanceModel> AttendanceList
        {
            get
            {
                if (m_AttendanceList == null)
                {
                    m_AttendanceList = new ObservableCollection<AttendanceModel>();
                }
                return m_AttendanceList;
            }
            set
            {
                m_AttendanceList = value;
                RaisePropertyChanged("AttendanceList");
            }
        }

        /// <summary>
        /// 선택된 출결
        /// </summary>
        private AttendanceModel m_SelectedAttendance;
        public AttendanceModel SelectedAttendance
        {
            get
            {
                return m_SelectedAttendance;
            }
            set
            {
                m_SelectedAttendance = value;
                RaisePropertyChanged("SelectedAttendance");
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
        
        #endregion

        #region command

        /// <summary>
        /// 선택 초기화 커맨드
        /// </summary>
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
                            AttendanceList.Clear();
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
                                    AttendanceList.Add(new AttendanceModel(dto));
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
                            AttendanceList.Clear();
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
                                    AttendanceList.Add(new AttendanceModel(dto));
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
                        AttendanceList.Clear();
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
                                AttendanceList.Add(new AttendanceModel(dto));
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
        /// 취소 커맨드
        /// </summary>
        public DelegateCommand<StreamPlayerControl> CancelCommand
        {
            get
            {
                return new DelegateCommand<StreamPlayerControl>(delegate (StreamPlayerControl streamPlayerControl)
                {
                    SharedPreference.Instance.ViewName = "";
                    SharedPreference.Instance.IsManagement = false;
                });
            }
        }

        /// <summary>
        /// 수정 커맨드
        /// </summary>
        public DelegateCommand EditPopupCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    var popup = new EditAttendancePopup();
                    if (SharedPreference.Instance.SelectedAttendance != null)
                    {
                        if (WindowHelper.CreatePopup(popup, "출결 수정", true) == true)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("수정할 사원을 선택해주세요.");
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
        
        #endregion
    }
}
