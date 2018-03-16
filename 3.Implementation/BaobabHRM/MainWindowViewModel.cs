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

namespace BaobabHRM
{
    public class MainWindowViewModel : BindableBase
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

        private void LoadDept()
        {
            SharedPreference.Instance.DeptList.Clear();

            var sqlData = new DeptQuery().SelectAll();
            while (sqlData.Read())
            {
                var dto = new DeptDTO()
                {
                    DEPT_CODE = sqlData["code"].ToString(),
                    DEPT_NAME = sqlData["name"].ToString()
                };
                SharedPreference.Instance.DeptList.Add(new DeptModel(dto));
            }
            sqlData.Close();
            SharedPreference.Instance.DBM.SqlConn.Close();

            var list = SharedPreference.Instance.DeptList.OrderBy(p => p.DEPT_CODE);
            SharedPreference.Instance.DeptList = new ObservableCollection<DeptModel>(list);
        }

        private void LoadRank()
        {
            SharedPreference.Instance.RankList.Clear();

            var sqlData = new RankQuery().SelectAll();
            while (sqlData.Read())
            {
                var dto = new RankDTO()
                {
                    RANK_CODE = sqlData["code"].ToString(),
                    RANK_NAME = sqlData["name"].ToString()
                };
                SharedPreference.Instance.RankList.Add(new RankModel(dto));
            }
            var list = SharedPreference.Instance.RankList.OrderBy(p => p.RANK_CODE);
            SharedPreference.Instance.RankList = new ObservableCollection<RankModel>(list);

            sqlData.Close();
            SharedPreference.Instance.DBM.SqlConn.Close();

        }

        private void LoadStaff()
        {
            SharedPreference.Instance.StaffList.Clear();

            if (SharedPreference.Instance.SelectedDept != null)
            {
                SqlDataReader sqlData = null;
                if (!SharedPreference.Instance.IsManagement)
                {
                    sqlData = new StaffQuery().SelectWithDeptUser(SharedPreference.Instance.SelectedDept.DEPT_CODE);
                }
                else
                {
                    sqlData = new StaffQuery().SelectWithDept(SharedPreference.Instance.SelectedDept.DEPT_CODE);
                }
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
        }

        #endregion

        #region command

        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    LoadDept();
                    LoadRank();
                    LoadStaff();
                });
            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
               {
                   var popup = new LoginPopup();
                   if (WindowHelper.CreatePopup(popup, "관리자 로그인", true) == true)
                   {
                       var vm = popup.DataContext as LoginPopupViewModel;
                   }
               });
            }
        }
        #endregion
    }
}
