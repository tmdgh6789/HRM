using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class StaffManagementViewModel : BindableBase
    {
        #region property

        /// <summary>
        /// 스태프 리스트
        /// </summary>
        private ObservableCollection<StaffModel> m_StaffList;
        public ObservableCollection<StaffModel> StaffList
        {
            get
            {
                if (m_StaffList == null)
                {
                    m_StaffList = new ObservableCollection<StaffModel>();
                }
                return m_StaffList;
            }
            set
            {
                m_StaffList = value;
                RaisePropertyChanged("StaffList");
            }
        }

        private StaffModel m_SelectedStaff;
        public StaffModel SelectedStaff
        {
            get
            {
                return m_SelectedStaff;
            }
            set
            {
                m_SelectedStaff = value;
                RaisePropertyChanged("SelectedStaff");
            }
        }


        #endregion

        #region command

        /// <summary>
        /// loaded command
        /// </summary>
        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                
                });
            }
        }

        public DelegateCommand SelectionChanged
        {
            get
            {
                return new DelegateCommand(delegate ()
                {

                });
            }
        }


        /// <summary>
        /// 스태프 추가 커맨드
        /// </summary>
        public DelegateCommand AddStaffCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    var popup = new AddStaffPopup();
                    if (WindowHelper.CreatePopup(popup, "사원추가", true) == true)
                    {
                        var vm = popup.DataContext as StaffManagementViewModel;
                    }
                });
            }
        }

        /// <summary>
        /// 스태프 수정 커맨드
        /// </summary>
        public DelegateCommand EditStaffCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    
                });
            }
        }

        /// <summary>
        /// 뒤로가기 커맨드
        /// </summary>
        public DelegateCommand BackCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    SharedPreference.Instance.ViewName = "";
                    SharedPreference.Instance.IsManagement = false;
                });
            }
        }

        #endregion
    }
}
