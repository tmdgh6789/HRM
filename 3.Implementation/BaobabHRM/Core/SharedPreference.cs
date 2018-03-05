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
        /// 로그인 유저
        /// </summary>
        public UserModel LoginUser { get; set; }

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


        #endregion

        #region method

        /// <summary>
        /// 로그인
        /// </summary>
        public bool Login(string id, string pwd)
        {
            //var results = new UserQuery().SELECT(id, pwd);
            //if (results.Count > 0)
            //{
                UserDTO dto = new UserDTO();
                dto.USER_ID = id;
                dto.USER_PW = pwd;
                SharedPreference.Instance.LoginUser = new UserModel(dto);
                SharedPreference.Instance.IsLoginCompleted = true;
                SharedPreference.Instance.IsManagement = true;
                return true;
            //}
            //else
            //{
            //    SharedPreference.Instance.IsLoginCompleted = false;
            //    return false;
            //}
        } // end method

        /// <summary>
        /// 로그아웃
        /// </summary>
        public void Logout()
        {
            SharedPreference.Instance.IsLoginCompleted = false;
        } // end method

        #endregion
    }
}
