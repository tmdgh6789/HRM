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

        private int m_Code;
        public int Code
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
            }
        }

        private void LoadRank()
        {
            RankList.Clear();
            foreach (var model in SharedPreference.Instance.RankList)
            {
                RankList.Add(model);
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
                });
            }
        }

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

        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
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
                            DeptList.Add(new DeptModel(dto));
                            new DeptQuery().Insert(dto);
                            MessageBox.Show("부서를 추가하셨습니다.");
                            Code = 0;
                            Name = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("부서 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
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
                            RankList.Add(new RankModel(dto));
                            new RankQuery().Insert(dto);
                            MessageBox.Show("직급을 추가하셨습니다.");
                            Code = 0;
                            Name = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("직급 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        }
                    }
                });
            }
        }

        public DelegateCommand<UserControl> DeleteCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "부서관리")
                    {
                        DeptDTO dto = new DeptDTO
                        {
                            DEPT_CODE = SelectedDept.DEPT_CODE,
                            DEPT_NAME = SelectedDept.DEPT_NAME
                        };

                        try
                        {
                            SharedPreference.Instance.DeptList.Remove(SelectedDept);
                            DeptList.Remove(SelectedDept);
                            new DeptQuery().Delete(dto);
                            MessageBox.Show("부서를 삭제하셨습니다.");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("부서 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        }
                    }
                    else if ((Window.GetWindow(uc).FindName("TITLE") as TextBlock).Text == "직급관리")
                    {
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
                            MessageBox.Show("직급을 삭제하셨습니다.");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("직급 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        }
                    }
                });
            }
        }

        #endregion
    }
}
