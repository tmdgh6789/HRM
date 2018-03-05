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
                    DEPT_CODE = (int)sqlData["code"],
                    DEPT_NAME = sqlData["name"].ToString()
                };
                SharedPreference.Instance.DeptList.Add(new DeptModel(dto));
            }
            sqlData.Close();
            SharedPreference.Instance.DBM.SqlConn.Close();

        }

        private void LoadStaff()
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
                        STAFF_DEPT = (int)sqlData["dept"],
                        STAFF_RANK = (int)sqlData["rank"],
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
                   var view = new LoginPopup();
                   if (WindowHelper.CreatePopup(view, "관리자 로그인", true) == true)
                   {
                       var vm = view.DataContext as LoginPopupViewModel;
                   }
               });
            }
        }
        #endregion
    }
}
