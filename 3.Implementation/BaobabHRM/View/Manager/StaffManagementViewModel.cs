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

        /// <summary>
        /// 선택 초기화 커맨드
        /// </summary>
        public DelegateCommand SelectedClearCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    SelectedStaff = null;
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
                            StaffList.Clear();
                            var sqlData = new StaffQuery().SelectWithDept(SharedPreference.Instance.SelectedDept.DEPT_CODE);
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    StaffDTO dto = new StaffDTO
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
                                    StaffList.Add(new StaffModel(dto));
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
                            StaffList.Clear();
                            var sqlData = new StaffQuery().SelectWithIdnumber(SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER);
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    StaffDTO dto = new StaffDTO
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
                                    StaffList.Add(new StaffModel(dto));
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
                        StaffList.Clear();
                        var sqlData = new StaffQuery().SelectAll();
                        if (sqlData.HasRows)
                        {
                            while (sqlData.Read())
                            {
                                StaffDTO dto = new StaffDTO
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
                                StaffList.Add(new StaffModel(dto));
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
                    var popup = new EditStaffPopup();
                    if (SelectedStaff != null)
                    {
                        SharedPreference.Instance.StaffList = StaffList;
                        SharedPreference.Instance.SelectedStaff = SelectedStaff;
                        if (WindowHelper.CreatePopup(popup, "사원수정", true) == true)
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
