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
    public class AddStaffPopupViewModel : BindableBase
    {
        #region property

        private ObservableCollection<DeptModel> m_DeptName;
        public ObservableCollection<DeptModel> DeptName
        {
            get
            {
                if (m_DeptName == null)
                {
                    m_DeptName = new ObservableCollection<DeptModel>();
                    foreach (var dto in SharedPreference.Instance.DeptList)
                    {
                        m_DeptName.Add(dto);
                    }
                }
                return m_DeptName;
            }
            set
            {
                m_DeptName = value;
                RaisePropertyChanged("DeptName");
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


        private ObservableCollection<RankModel> m_RankName;
        public ObservableCollection<RankModel> RankName
        {
            get
            {
                if (m_RankName == null)
                {
                    m_RankName = new ObservableCollection<RankModel>();
                    foreach (var dto in SharedPreference.Instance.RankList)
                    {
                        m_RankName.Add(dto);
                    }
                }
                return m_RankName;
            }
            set
            {
                m_RankName = value;
                RaisePropertyChanged("RankName");
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

        private DateTime m_StaffJoinDay = DateTime.Now;
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
                return StaffJoinDay.ToString("yyyy-MM-dd");
            }
            set
            {
                m_StaffJoinDayStr = value;
                RaisePropertyChanged("StaffJoinDayStr");
            }
        }


        #endregion

        #region command

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
                    StaffDTO dto = new StaffDTO
                    {
                        STAFF_IDNUMBER = SelectedDept.DEPT_CODE.ToString() + "_" + SelectedRank.RANK_CODE.ToString() + DateTime.Now.ToString("yyMMdd"),
                        STAFF_DEPT = SelectedDept.DEPT_CODE,
                        STAFF_RANK = SelectedRank.RANK_CODE,
                        STAFF_NAME = StaffName,
                        STAFF_ADDRESS = StaffAddress,
                        STAFF_TEL = StaffTel,
                        STAFF_JOIN_DAY = StaffJoinDayStr,
                        STAFF_STATE = "재직"
                    };

                    try
                    {
                        SharedPreference.Instance.StaffList.Add(new StaffModel(dto));

                        new StaffQuery().Insert(dto);

                        // 수정 내역 저장
                        try
                        {
                            AllLogDTO logDto = new AllLogDTO
                            {
                                ALLLOG_ADMIN = SharedPreference.Instance.LoginUser.USER_ID,
                                ALLLOG_WHAT = "사원",
                                ALLLOG_LOG = StaffName + " 사원 추가",
                                ALLLOG_REASON = "사원 입사",
                                ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                            };

                            new AllLogQuery().Insert(logDto);

                            MessageBox.Show("사원을 추가하셨습니다.");
                            StaffName = "";
                            StaffAddress = "";
                            StaffTel = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("사원 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("사원 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                        {
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                    }

                    Window.GetWindow(uc).DialogResult = true;
                });
            }
        }

        #endregion
    }
}
