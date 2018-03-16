using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class SharedPreference : BindableBase
    {
        #region singleton

        private SharedPreference()
        {
            //this.LoginUser = null;
        }

        private static SharedPreference m_Instance = null;
        public static SharedPreference Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new SharedPreference();
                    m_Instance.DBM = new DBManager();
                    m_Instance.DBM.Init();
                }
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }

        #endregion

        #region property

        /// <summary>
        /// 로컬 데이터베이스 객체
        /// </summary>
        public DBManager DBM { get; set; }

        /// <summary>
        /// 로그인 관리자
        /// </summary>
        public AdminModel LoginAdmin { get; set; }

        /// <summary>
        /// 전역 워크스페이스 뷰 모델
        /// </summary>
        public WorkspaceViewModel Workspace { get; set; }

        #endregion

        #region property (bindable)

        /// <summary>
        /// 로그인 완료 여부
        /// </summary>
        private bool m_IsLoginCompleted;
        public bool IsLoginCompleted
        {
            get
            {
                return m_IsLoginCompleted;
            }
            set
            {
                m_IsLoginCompleted = value;
                RaisePropertyChanged("IsLoginCompleted");
            }
        }

        /// <summary>
        /// 관리 여부
        /// </summary>
        private bool m_IsManagement;
        public bool IsManagement
        {
            get
            {
                return m_IsManagement;
            }
            set
            {
                m_IsManagement = value;
                RaisePropertyChanged("IsManagement");
            }
        }


        /// <summary>
        /// 관리 뷰 이름
        /// </summary>
        private string m_ViewName;
        public string ViewName
        {
            get
            {
                return m_ViewName;
            }
            set
            {
                m_ViewName = value;
                RaisePropertyChanged("ViewName");
            }
        }

        /// <summary>
        /// 부서 리스트
        /// </summary>
        private ObservableCollection<DeptModel> m_DeptList;
        public ObservableCollection<DeptModel> DeptList
        {
            get
            {
                if (m_DeptList == null)
                {
                    m_DeptList = new ObservableCollection<DeptModel>();
                }
                return m_DeptList;
            }
            set
            {
                m_DeptList = value;
                RaisePropertyChanged("DeptList");
            }
        }

        /// <summary>
        /// 선택한 부서
        /// </summary>
        private DeptModel m_SelectedDept;
        public DeptModel SelectedDept
        {
            get
            {
                return m_SelectedDept;
            }
            set
            {
                m_SelectedDept = value;
                RaisePropertyChanged("SelectedDept");
            }
        }

        /// <summary>
        /// 직급 리스트
        /// </summary>
        private ObservableCollection<RankModel> m_RankList;
        public ObservableCollection<RankModel> RankList
        {
            get
            {
                if (m_RankList == null)
                {
                    m_RankList = new ObservableCollection<RankModel>();
                }
                return m_RankList;
            }
            set
            {
                m_RankList = value;
                RaisePropertyChanged("RankList");
            }
        }

        /// <summary>
        /// 선택한 직급
        /// </summary>
        private RankModel m_SelectedRank;
        public RankModel SelectedRank
        {
            get
            {
                return m_SelectedRank;
            }
            set
            {
                m_SelectedRank = value;
                RaisePropertyChanged("SelectedRank");
            }
        }
        
        /// <summary>
        /// 사원 리스트
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

        private ObservableCollection<StaffModel> m_StaffViewStaffList;
        public ObservableCollection<StaffModel> StaffViewStaffList
        {
            get
            {
                if (m_StaffViewStaffList == null)
                {
                    m_StaffViewStaffList = new ObservableCollection<StaffModel>();
                }
                return m_StaffViewStaffList;
            }
            set
            {
                m_StaffViewStaffList = value;
                RaisePropertyChanged("StaffViewStaffList");
            }
        }


        /// <summary>
        /// 선택한 사원
        /// </summary>
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

        /// <summary>
        /// 선택한 출결
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


        #endregion

        #region method

        /// <summary>
        /// 로그인
        /// </summary>
        public bool Login(string id, string pwd)
        {
            var sqlData = new AdminQuery().SelectWithId(id, pwd);
            if (sqlData.HasRows)
            {
                sqlData.Read();
                AdminDTO dto = new AdminDTO
                {
                    ADMIN_ID = sqlData["id"].ToString(),
                    ADMIN_PASSWORD = sqlData["password"].ToString(),
                    ADMIN_NAME = sqlData["name"].ToString(),
                    ADMIN_RANK = sqlData["rank"].ToString(),
                    ADMIN_GRADE = sqlData["grade"].ToString()
                };
                SharedPreference.Instance.LoginAdmin = new AdminModel(dto);
                SharedPreference.Instance.IsLoginCompleted = true;
                SharedPreference.Instance.IsManagement = true;
                sqlData.Close();
                SharedPreference.Instance.DBM.SqlConn.Close();
                return true;
            }
            else
            {
                SharedPreference.Instance.IsLoginCompleted = false;
                sqlData.Close();
                SharedPreference.Instance.DBM.SqlConn.Close();
                return false;
            }

        } // end method

        /// <summary>
        /// 로그아웃
        /// </summary>
        public void Logout()
        {
            SharedPreference.Instance.IsLoginCompleted = false;
            SharedPreference.Instance.LoginAdmin = null;
        } // end method

        #endregion

        #region command

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
                        var list = SharedPreference.Instance.StaffList.OrderBy(p => p.STAFF_RANK).ThenBy(p => p.STAFF_IDNUMBER);
                        SharedPreference.Instance.StaffList = new ObservableCollection<StaffModel>(list);

                        sqlData.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                    }
                });
            }
        }

        #endregion
    }
}
