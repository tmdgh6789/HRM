using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        /// <summary>
        /// 출근 커맨드
        /// </summary>
        public DelegateCommand AttendanceCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    
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
                        var view = new LoginPopup();
                        if (WindowHelper.CreatePopup(view, "관리자 로그인", true) == true)
                        {
                            var vm = view.DataContext as LoginPopupViewModel;
                        }
                    }
                    else
                    {
                        var view = new ManagementPopup();
                        if (WindowHelper.CreatePopup(view, "관리자", true) == true)
                        {
                            var vm = view.DataContext as ManagementPopupViewModel;
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

    }
}
