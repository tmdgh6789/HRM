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
    public class ManagementPopupViewModel : BindableBase
    {

        /// <summary>
        /// loaded command
        /// </summary>
        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    //if (SharedPreference.Instance.SelectedDept != null)
                    //{
                    //    SharedPreference.Instance.SelectedDept = null;
                    //}
                    //if (SharedPreference.Instance.SelectedStaff != null)
                    //{
                    //    SharedPreference.Instance.SelectedStaff = null;
                    //}
                    //if (SharedPreference.Instance.SelectedRank != null)
                    //{
                    //    SharedPreference.Instance.SelectedRank = null;
                    //}
                });
            }
        }

        /// <summary>
        /// 부서 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> DeptManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Dept";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;

                    var popup = new DeptManagementPopup();
                    if (WindowHelper.CreatePopup(popup, "부서관리", true) == true)
                    {
                        var vm = popup.DataContext as DeptManagementPopupViewModel;
                    }
                });
            }
        }

        /// <summary>
        /// 직급 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> RankManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Rank";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;

                    var popup = new RankManagementPopup();
                    if (WindowHelper.CreatePopup(popup, "직급관리", true) == true)
                    {
                        var vm = popup.DataContext as RankManagementPopupViewModel;
                    }
                });
            }
        }

        /// <summary>
        /// 사원 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> StaffManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Staff";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        /// <summary>
        /// 출결 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> AttendanceManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Attendance";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        /// <summary>
        /// 통계 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> StatisticsCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Statistics";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        /// <summary>
        /// 기타 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> EtcManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Etc";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        /// <summary>
        /// 관리자 관리 커맨드
        /// </summary>
        public DelegateCommand<UserControl> AdminManagementCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "Admin";
                    SharedPreference.Instance.IsManagement = true;
                    Window.GetWindow(uc).DialogResult = true;

                    var popup = new AdminManagementPopup();
                    if (WindowHelper.CreatePopup(popup, "관리자 관리", true) == true)
                    {
                        var vm = popup.DataContext as AdminManagementPopupViewModel;
                    }
                });
            }
        }
        
        /// <summary>
        /// 뒤로가기 커맨드
        /// </summary>
        public DelegateCommand<UserControl> BackCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.IsManagement = false;
                    Window.GetWindow(uc).DialogResult = false;
                });
            }
        }

    }
}
