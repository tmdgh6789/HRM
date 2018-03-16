using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class DeptAndRankPopupItemViewModel : BindableBase
    {

        #region property

        /// <summary>
        /// 타이틀
        /// </summary>
        private string m_Title;
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                RaisePropertyChanged("Title");
            }
        }

        /// <summary>
        /// 부서 / 직급 코드
        /// </summary>
        private string m_Code;
        public string Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                m_Code = value;
                RaisePropertyChanged("Code");
            }
        }
        
        /// <summary>
        /// 부서 / 직급 이름
        /// </summary>
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// 수정 사유
        /// </summary>
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
        
        #endregion

        #region method

        private void LoadDept()
        {
            DeptList.Clear();
            foreach (var model in SharedPreference.Instance.DeptList)
            {
                DeptList.Add(model);
                Title = "DEPT";
            }
            var list = DeptList.OrderBy(p => p.DEPT_CODE);
            DeptList = new ObservableCollection<DeptModel>(list);
        }

        private void LoadRank()
        {
            RankList.Clear();
            foreach (var model in SharedPreference.Instance.RankList)
            {
                RankList.Add(model);
                Title = "RANK";
            }
            var list = RankList.OrderBy(p => p.RANK_CODE);
            RankList = new ObservableCollection<RankModel>(list);
        }

        #endregion

        #region command

        public DelegateCommand<UserControl> LoadedCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "부서관리")
                    {
                        LoadDept();
                    }
                    else if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "직급관리")
                    {
                        LoadRank();
                    }
                });
            }
        }

        /// <summary>
        /// 취소 커맨드
        /// </summary>
        public DelegateCommand<UserControl> CancelCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    SharedPreference.Instance.ViewName = "";
                    SharedPreference.Instance.IsManagement = false;
                    Window.GetWindow(uc).DialogResult = false;
                });
            }
        }

        /// <summary>
        /// 추가 커맨드
        /// </summary>
        public DelegateCommand<UserControl> AddCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if (Code == null || Code.Length == 0)
                    {
                        MessageBox.Show("코드를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Name == null || Name.Length == 0)
                    {
                        MessageBox.Show("이름을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "부서관리")
                    {
                        DeptDTO dto = new DeptDTO
                        {
                            DEPT_CODE = Code,
                            DEPT_NAME = Name
                        };

                        try
                        {
                            SharedPreference.Instance.DeptList.Add(new DeptModel(dto));
                            var list = SharedPreference.Instance.DeptList.OrderBy(p => p.DEPT_CODE);
                            SharedPreference.Instance.DeptList = new ObservableCollection<DeptModel>(list);
                            DeptList.Add(new DeptModel(dto));
                            var list2 = DeptList.OrderBy(p => p.DEPT_CODE);
                            DeptList = new ObservableCollection<DeptModel>(list2);
                            new DeptQuery().Insert(dto);

                            // 수정 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "부서",
                                    ALLLOG_LOG = Name + " 부서 생성",
                                    ALLLOG_REASON = "부서 신설",
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);

                                MessageBox.Show("부서를 추가하셨습니다.");
                                Code = "";
                                Name = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("부서 생성을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("부서 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    else if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "직급관리")
                    {
                        RankDTO dto = new RankDTO
                        {
                            RANK_CODE = Code,
                            RANK_NAME = Name
                        };

                        try
                        {
                            SharedPreference.Instance.RankList.Add(new RankModel(dto));
                            var list = SharedPreference.Instance.RankList.OrderBy(p => p.RANK_CODE);
                            SharedPreference.Instance.RankList = new ObservableCollection<RankModel>(list);
                            RankList.Add(new RankModel(dto));
                            var list2 = RankList.OrderBy(p => p.RANK_CODE);
                            RankList = new ObservableCollection<RankModel>(list2);
                            new RankQuery().Insert(dto);

                            // 수정 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "직급",
                                    ALLLOG_LOG = Name + " 직급 생성",
                                    ALLLOG_REASON = "직급 신설",
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);

                                MessageBox.Show("직급을 추가하셨습니다.");
                                Code = "";
                                Name = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("직급 생성을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("직급 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 수정 커맨드
        /// </summary>
        public DelegateCommand<UserControl> EditCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if (Title == "DEPT")
                    {
                        if (SelectedDept == null)
                        {
                            MessageBox.Show("수정할 부서 코드를 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else if (Title == "RANK")
                    {
                        if (SelectedRank == null)
                        {
                            MessageBox.Show("수정할 직급 코드를 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    if (Name == null || Name.Length == 0)
                    {
                        MessageBox.Show("수정할 이름을 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Title == "DEPT")
                    {
                        var index = SharedPreference.Instance.DeptList.IndexOf(SelectedDept);
                        var beforeName = SelectedDept.DEPT_NAME;
                        DeptDTO deptDto = new DeptDTO
                        {
                            DEPT_CODE = SelectedDept.DEPT_CODE,
                            DEPT_NAME = Name
                        };

                        // 부서 테이블 수정
                        try
                        {
                            DeptList.Remove(SelectedDept);
                            SharedPreference.Instance.DeptList.Remove(SelectedDept);
                            DeptList.Insert(index, new DeptModel(deptDto));
                            SharedPreference.Instance.DeptList.Insert(index, new DeptModel(deptDto));
                            new DeptQuery().Update(deptDto);

                            // 수정 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "부서 이름",
                                    ALLLOG_LOG = beforeName + " -> " + Name,
                                    ALLLOG_REASON = Reason,
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);
                                MessageBox.Show("부서 이름을 수정하셨습니다.");

                                Name = "";
                                Reason = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("부서 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("부서 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    else if (Title == "RANK")
                    {
                        var index = SharedPreference.Instance.RankList.IndexOf(SelectedRank);
                        var beforeName = SelectedRank.RANK_NAME;
                        RankDTO rankDto = new RankDTO
                        {
                            RANK_CODE = SelectedRank.RANK_CODE,
                            RANK_NAME = Name
                        };

                        // 부서 테이블 수정
                        try
                        {
                            RankList.Remove(SelectedRank);
                            SharedPreference.Instance.RankList.Remove(SelectedRank);
                            RankList.Insert(index, new RankModel(rankDto));
                            SharedPreference.Instance.RankList.Insert(index, new RankModel(rankDto));
                            new RankQuery().Update(rankDto);
                            
                            // 수정 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "직급 이름",
                                    ALLLOG_LOG = beforeName + " -> " + Name,
                                    ALLLOG_REASON = Reason,
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);
                                MessageBox.Show("직급 이름을 수정하셨습니다.");

                                Name = "";
                                Reason = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("직급 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("직급 수정을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                });
            }
        }


        /// <summary>
        /// 삭제 커맨드
        /// </summary>
        public DelegateCommand<UserControl> DeleteCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if (Title == "DEPT")
                    {
                        if (SelectedDept == null)
                        {
                            MessageBox.Show("삭제할 부서를 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    else if (Title == "RANK")
                    {
                        if (SelectedRank == null)
                        {
                            MessageBox.Show("삭제할 직급을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Title == "DEPT")
                    {
                        var beforeName = SelectedDept.DEPT_NAME;
                        DeptDTO dto = new DeptDTO
                        {
                            DEPT_CODE = SelectedDept.DEPT_CODE,
                            DEPT_NAME = SelectedDept.DEPT_NAME
                        };

                        // 부서 삭제
                        try
                        {
                            SharedPreference.Instance.DeptList.Remove(SelectedDept);
                            DeptList.Remove(SelectedDept);
                            new DeptQuery().Delete(dto);
                            
                            // 삭제 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "부서",
                                    ALLLOG_LOG = beforeName + "삭제",
                                    ALLLOG_REASON = Reason,
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);
                                MessageBox.Show("부서를 삭제하셨습니다.");

                                Name = "";
                                Reason = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("부서 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("부서 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    else if (Title == "RANK")
                    {
                        var beforeName = SelectedRank.RANK_NAME;
                        RankDTO dto = new RankDTO
                        {
                            RANK_CODE = SelectedRank.RANK_CODE,
                            RANK_NAME = SelectedRank.RANK_NAME
                        };

                        try
                        {
                            SharedPreference.Instance.RankList.Remove(SelectedRank);
                            RankList.Remove(SelectedRank);
                            new RankQuery().Delete(dto);

                            // 삭제 내역 저장
                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "직급",
                                    ALLLOG_LOG = beforeName + "삭제",
                                    ALLLOG_REASON = Reason,
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);
                                MessageBox.Show("직급을 삭제하셨습니다.");

                                Name = "";
                                Reason = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("직급 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("직급 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                });
            }
        }

        #endregion
    }
}
