using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class OutTimeCheckPopupViewModel : BindableBase
    {
        #region property

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

        #endregion

        #region method

        private void LoadAttendance()
        {
            try
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
                while (sqlData.Read())
                {
                    AttendanceDTO newDto = new AttendanceDTO
                    {
                        ATTENDANCE_IDNUMBER = sqlData["idnumber"].ToString(),
                        ATTENDANCE_BUSINESS_DAY = sqlData["businessday"].ToString(),
                        ATTENDANCE_OUT_TIME = sqlData["out_time"].ToString()
                    };
                    AttendanceList.Add(new AttendanceModel(newDto));
                }
                sqlData.Close();
                SharedPreference.Instance.DBM.SqlConn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("퇴근 리스트를 불러오는데 실패했습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
            }
        }

        #endregion

        #region command


        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    LoadAttendance();
                });
            }
        }


        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    try
                    {
                        new AttendanceQuery().UpdateOutTime(AttendanceList);

                        MessageBox.Show("처리되었습니다.");
                        Window.GetWindow(uc).DialogResult = true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("퇴근시간 입력을 실패했습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                    }
                });
            }
        }

        #endregion
    }
}
