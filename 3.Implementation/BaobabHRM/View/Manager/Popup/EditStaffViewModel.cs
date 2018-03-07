using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class EditStaffViewModel : BindableBase
    {
        #region property
        
        private bool m_IsCheckedDept;
        public bool IsCheckedDept
        {
            get
            {
                return m_IsCheckedDept;
            }
            set
            {
                m_IsCheckedDept = value;
                RaisePropertyChanged("IsCheckedDept");
            }
        }

        private bool m_IsCheckedRank;
        public bool IsCheckedRank
        {
            get
            {
                return m_IsCheckedRank;
            }
            set
            {
                m_IsCheckedRank = value;
                RaisePropertyChanged("IsCheckedRank");
            }
        }

        private bool m_IsCheckedName;
        public bool IsCheckedName
        {
            get
            {
                return m_IsCheckedName;
            }
            set
            {
                m_IsCheckedName = value;
                RaisePropertyChanged("IsCheckedName");
            }
        }

        private bool m_IsCheckedAddress;
        public bool IsCheckedAddress
        {
            get
            {
                return m_IsCheckedAddress;
            }
            set
            {
                m_IsCheckedAddress = value;
                RaisePropertyChanged("IsCheckedAddress");
            }
        }

        private bool m_IsCheckedTel;
        public bool IsCheckedTel
        {
            get
            {
                return m_IsCheckedTel;
            }
            set
            {
                m_IsCheckedTel = value;
                RaisePropertyChanged("IsCheckedTel");
            }
        }

        private bool m_IsCheckedJoinDay;
        public bool IsCheckedJoinDay
        {
            get
            {
                return m_IsCheckedJoinDay;
            }
            set
            {
                m_IsCheckedJoinDay = value;
                RaisePropertyChanged("IsCheckedJoinDay");
            }
        }

        private bool m_IsCheckedRetirementDay;
        public bool IsCheckedRetirementDay
        {
            get
            {
                return m_IsCheckedRetirementDay;
            }
            set
            {
                m_IsCheckedRetirementDay = value;
                RaisePropertyChanged("IsCheckedRetirementDay");
            }
        }

        private bool m_IsCheckedState;
        public bool IsCheckedState
        {
            get
            {
                return m_IsCheckedState;
            }
            set
            {
                m_IsCheckedState = value;
                RaisePropertyChanged("IsCheckedState");
            }
        }

        private ObservableCollection<DeptModel> m_DeptList;
        public ObservableCollection<DeptModel> DeptList
        {
            get
            {
                if (m_DeptList == null)
                {
                    m_DeptList = new ObservableCollection<DeptModel>();
                    foreach (var dto in SharedPreference.Instance.DeptList)
                    {
                        m_DeptList.Add(dto);
                    }
                }
                return m_DeptList;
            }
            set
            {
                m_DeptList = value;
                RaisePropertyChanged("DeptList");
            }
        }

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

        private int m_SelectedDeptIndex;
        public int SelectedDeptIndex
        {
            get
            {
                return m_SelectedDeptIndex;
            }
            set
            {
                m_SelectedDeptIndex = value;
                RaisePropertyChanged("SelectedDeptIndex");
            }
        }

        private ObservableCollection<RankModel> m_RankList;
        public ObservableCollection<RankModel> RankList
        {
            get
            {
                if (m_RankList == null)
                {
                    m_RankList = new ObservableCollection<RankModel>();
                    foreach (var dto in SharedPreference.Instance.RankList)
                    {
                        m_RankList.Add(dto);
                    }
                }
                return m_RankList;
            }
            set
            {
                m_RankList = value;
                RaisePropertyChanged("RankList");
            }
        }

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

        private int m_SelectedRankIndex;
        public int SelectedRankIndex
        {
            get
            {
                return m_SelectedRankIndex;
            }
            set
            {
                m_SelectedRankIndex = value;
                RaisePropertyChanged("SelectedRankIndex");
            }
        }

        private string m_StaffName;
        public string StaffName
        {
            get
            {
                return m_StaffName;
            }
            set
            {
                m_StaffName = value;
                RaisePropertyChanged("StaffName");
            }
        }

        private string m_StaffAddress;
        public string StaffAddress
        {
            get
            {
                return m_StaffAddress;
            }
            set
            {
                m_StaffAddress = value;
                RaisePropertyChanged("StaffAddress");
            }
        }

        private string m_StaffTel;
        public string StaffTel
        {
            get
            {
                return m_StaffTel;
            }
            set
            {
                m_StaffTel = value;
                RaisePropertyChanged("StaffTel");
            }
        }

        private DateTime m_StaffJoinDay;
        public DateTime StaffJoinDay
        {
            get
            {
                return m_StaffJoinDay;
            }
            set
            {
                m_StaffJoinDay = value;
                RaisePropertyChanged("StaffJoinDay");
            }
        }


        private string m_StaffJoinDayStr;
        public string StaffJoinDayStr
        {
            get
            {
                return m_StaffJoinDayStr;
            }
            set
            {
                m_StaffJoinDayStr = value;
                RaisePropertyChanged("StaffJoinDayStr");
            }
        }

        private DateTime m_StaffRetirementDay = DateTime.Now;
        public DateTime StaffRetirementDay
        {
            get
            {
                return m_StaffRetirementDay;
            }
            set
            {
                m_StaffRetirementDay = value;
                RaisePropertyChanged("StaffRetirementDay");
            }
        }


        private string m_StaffRetirementDayStr;
        public string StaffRetirementDayStr
        {
            get
            {
                return m_StaffRetirementDayStr;
            }
            set
            {
                m_StaffRetirementDayStr = value;
                RaisePropertyChanged("StaffRetirementDayStr");
            }
        }
        
        private ObservableCollection<string> m_StateList;
        public ObservableCollection<string> StateList
        {
            get
            {
                if (m_StateList == null)
                {
                    m_StateList = new ObservableCollection<string>();
                    m_StateList.Add("재직");
                    m_StateList.Add("퇴사");
                    m_StateList.Add("장기휴가");
                    m_StateList.Add("장기출장");
                }
                return m_StateList;
            }
            set
            {
                m_StateList = value;
                RaisePropertyChanged("StateList");
            }
        }

        private string m_StaffState;
        public string StaffState
        {
            get
            {
                return m_StaffState;
            }
            set
            {
                if (value == "재직")
                {
                    IsCheckedRetirementDay = false;
                }
                else
                {
                    IsCheckedRetirementDay = true;
                }
                m_StaffState = value;
                RaisePropertyChanged("StaffState");
            }
        }

        private int m_StateIndex;
        public int StateIndex
        {
            get
            {
                return m_StateIndex;
            }
            set
            {
                m_StateIndex = value;
                RaisePropertyChanged("StateIndex");
            }
        }

        private string m_Reason;
        public string Reason
        {
            get
            {
                return m_Reason;
            }
            set
            {
                m_Reason = value;
                RaisePropertyChanged("Reason");
            }
        }

        #endregion

        #region method

        public void LoadData()
        {
            DeptList = SharedPreference.Instance.DeptList;
            RankList = SharedPreference.Instance.RankList;
            StaffName = SharedPreference.Instance.SelectedStaff.STAFF_NAME;
            StaffAddress = SharedPreference.Instance.SelectedStaff.STAFF_ADDRESS;
            StaffTel = SharedPreference.Instance.SelectedStaff.STAFF_TEL;
            StaffJoinDayStr = SharedPreference.Instance.SelectedStaff.STAFF_JOIN_DAY;
            StaffJoinDay = DateTime.ParseExact(StaffJoinDayStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if ((SharedPreference.Instance.SelectedStaff.STAFF_RETIREMENT_DAY != "" || SharedPreference.Instance.SelectedStaff.STAFF_RETIREMENT_DAY.Length != 0) && SharedPreference.Instance.SelectedStaff.STAFF_RETIREMENT_DAY != null)
            {
                StaffRetirementDayStr = SharedPreference.Instance.SelectedStaff.STAFF_RETIREMENT_DAY;
                StaffRetirementDay = DateTime.ParseExact(StaffRetirementDayStr, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            StaffState = SharedPreference.Instance.SelectedStaff.STAFF_STATE;

            var deptList = SharedPreference.Instance.DeptList.Where(p => p.DEPT_CODE == SharedPreference.Instance.SelectedStaff.STAFF_DEPT).FirstOrDefault();
            SelectedDeptIndex = SharedPreference.Instance.DeptList.IndexOf(deptList);

            var rankList = SharedPreference.Instance.RankList.Where(p => p.RANK_CODE == SharedPreference.Instance.SelectedStaff.STAFF_RANK).FirstOrDefault();
            SelectedRankIndex = SharedPreference.Instance.RankList.IndexOf(rankList);

            var stateList = StateList.Where(p => p == SharedPreference.Instance.SelectedStaff.STAFF_STATE).FirstOrDefault();
            StateIndex = StateList.IndexOf(stateList);
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
                    LoadData();
                });
            }
        }


        public DelegateCommand<UserControl> CancelCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    Window.GetWindow(uc).DialogResult = false;
                });
            }
        }

        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    var index = SharedPreference.Instance.StaffList.IndexOf(SharedPreference.Instance.SelectedStaff);

                    StaffDTO beforeDto = new StaffDTO
                    {
                        STAFF_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                        STAFF_DEPT = SharedPreference.Instance.SelectedStaff.STAFF_DEPT,
                        STAFF_RANK = SharedPreference.Instance.SelectedStaff.STAFF_RANK,
                        STAFF_NAME = SharedPreference.Instance.SelectedStaff.STAFF_NAME,
                        STAFF_ADDRESS = SharedPreference.Instance.SelectedStaff.STAFF_ADDRESS,
                        STAFF_TEL = SharedPreference.Instance.SelectedStaff.STAFF_TEL,
                        STAFF_JOIN_DAY = SharedPreference.Instance.SelectedStaff.STAFF_JOIN_DAY,
                        STAFF_RETIREMENT_DAY = SharedPreference.Instance.SelectedStaff.STAFF_RETIREMENT_DAY,
                        STAFF_STATE = SharedPreference.Instance.SelectedStaff.STAFF_STATE,
                    };

                    StaffDTO afterDto = new StaffDTO
                    {
                        STAFF_IDNUMBER = SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER,
                        STAFF_DEPT = SelectedDept.DEPT_CODE,
                        STAFF_RANK = SelectedRank.RANK_CODE,
                        STAFF_NAME = StaffName,
                        STAFF_ADDRESS = StaffAddress,
                        STAFF_TEL = StaffTel,
                        STAFF_JOIN_DAY = StaffJoinDay.ToString("yyyy-MM-dd"),
                        STAFF_RETIREMENT_DAY = StaffRetirementDay.ToString("yyyy-MM-dd"),
                        STAFF_STATE = StaffState,
                    };

                    if (StaffState == "재직") afterDto.STAFF_RETIREMENT_DAY = null;

                    string what = $"사번: {SharedPreference.Instance.SelectedStaff.STAFF_IDNUMBER}, ";
                    string log = "";

                    if (beforeDto.STAFF_DEPT != afterDto.STAFF_DEPT)
                    {
                        what += "부서 ";
                        log += $"부서: {beforeDto.STAFF_DEPT} -> {afterDto.STAFF_DEPT} ";
                    }
                    if (beforeDto.STAFF_RANK != afterDto.STAFF_RANK)
                    {
                        what += "직급 ";
                        log += $"직급: {beforeDto.STAFF_RANK} -> {afterDto.STAFF_RANK} ";
                    }
                    if (beforeDto.STAFF_NAME != afterDto.STAFF_NAME)
                    {
                        what += "이름 ";
                        log += $"이름: {beforeDto.STAFF_NAME} -> {afterDto.STAFF_NAME} ";
                    }
                    if (beforeDto.STAFF_ADDRESS != afterDto.STAFF_ADDRESS)
                    {
                        what += "주소 ";
                        log += $"주소: {beforeDto.STAFF_ADDRESS} -> {afterDto.STAFF_ADDRESS} ";
                    }
                    if (beforeDto.STAFF_TEL != afterDto.STAFF_TEL)
                    {
                        what += "연락처 ";
                        log += $"연락처: {beforeDto.STAFF_TEL} -> {afterDto.STAFF_TEL} ";
                    }
                    if (beforeDto.STAFF_JOIN_DAY != afterDto.STAFF_JOIN_DAY)
                    {
                        what += "입사날짜 ";
                        log += $"입사날짜: {beforeDto.STAFF_JOIN_DAY} -> {afterDto.STAFF_JOIN_DAY} ";
                    }
                    if (beforeDto.STAFF_RETIREMENT_DAY != afterDto.STAFF_RETIREMENT_DAY)
                    {
                        what += "퇴사날짜 ";
                        log += $"퇴사날짜: {beforeDto.STAFF_RETIREMENT_DAY} -> {afterDto.STAFF_RETIREMENT_DAY} ";
                    }
                    if (beforeDto.STAFF_STATE != afterDto.STAFF_STATE)
                    {
                        what += "상태 ";
                        log += $"상태: {beforeDto.STAFF_STATE} -> {afterDto.STAFF_STATE} ";
                    }

                    try
                    {
                        SharedPreference.Instance.StaffList.Remove(SharedPreference.Instance.SelectedStaff);
                        SharedPreference.Instance.StaffList.Insert(index, new StaffModel(afterDto));

                        new StaffQuery().Update(afterDto);

                        try
                        {
                            AllLogDTO logDto = new AllLogDTO
                            {
                                ALLLOG_ADMIN = SharedPreference.Instance.LoginUser.USER_ID,
                                ALLLOG_WHAT = what,
                                ALLLOG_LOG = log,
                                ALLLOG_REASON = Reason,
                                ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                            };

                            new AllLogQuery().Insert(logDto);

                            MessageBox.Show("사원 정보를 수정하셨습니다.");
                            StaffName = "";
                            StaffAddress = "";
                            StaffTel = "";
                            StaffState = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("사원 정보 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }

                        Window.GetWindow(uc).DialogResult = true;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("사원 정보 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                        {
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                    }
                });
            }
        }

        #endregion
    }
}
