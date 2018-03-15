using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaobabHRM
{
    public class InquiryPopupViewModel : BindableBase
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
                var sqlData = new AttendanceQuery().SelectWithDeptAndDay(SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER, StartDate, EndDate);
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
            catch (Exception e)
            {
                SharedPreference.Instance.DBM.SqlConn.Close();

                MessageBox.Show("오늘 출결 상황을 읽어오지 못했습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
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

        /// <summary>
        /// 검색 커맨드
        /// </summary>
        public DelegateCommand SerchCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    if (SharedPreference.Instance.SelectedStaff != null)
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
                });
            }
        }

        #endregion
    }
}
