using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BaobabHRM
{
    public class AdminPopupViewModel : BindableBase
    {

        #region property

        /// <summary>
        /// 패스워드 체크 여부
        /// </summary>
        private bool m_IsCheckedPassword;
        public bool IsCheckedPassword
        {
            get
            {
                return m_IsCheckedPassword;
            }
            set
            {
                m_IsCheckedPassword = value;
                RaisePropertyChanged("IsCheckedPassword");
            }
        }

        /// <summary>
        /// 등급 체크 여부
        /// </summary>
        private bool m_IsCheckedGrade;
        public bool IsCheckedGrade
        {
            get
            {
                return m_IsCheckedGrade;
            }
            set
            {
                m_IsCheckedGrade = value;
                RaisePropertyChanged("IsCheckedGrade");
            }
        }

        /// <summary>
        /// 권한 체크 여부
        /// </summary>
        private bool m_IsCheckedAuth;
        public bool IsCheckedAuth
        {
            get
            {
                return m_IsCheckedAuth;
            }
            set
            {
                m_IsCheckedAuth = value;
                RaisePropertyChanged("IsCheckedAuth");
            }
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        private ObservableCollection<AdminModel> m_AdminList;
        public ObservableCollection<AdminModel> AdminList
        {
            get
            {
                if (m_AdminList == null)
                {
                    m_AdminList = new ObservableCollection<AdminModel>();
                }
                return m_AdminList;
            }
            set
            {
                m_AdminList = value;
                RaisePropertyChanged("AdminList");
            }
        }

        /// <summary>
        /// 선택된 관리자
        /// </summary>
        private AdminModel m_SelectedAdmin;
        public AdminModel SelectedAdmin
        {
            get
            {
                return m_SelectedAdmin;
            }
            set
            {
                m_SelectedAdmin = value;
                RaisePropertyChanged("SelectedAdmin");
            }
        }

        /// <summary>
        /// 번호
        /// </summary>
        private int m_Num;
        public int Num
        {
            get
            {
                return m_Num;
            }
            set
            {
                m_Num = value;
                RaisePropertyChanged("Num");
            }
        }

        /// <summary>
        /// 아이디
        /// </summary>
        private string m_Id;
        public string Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
                RaisePropertyChanged("Id");
            }
        }

        /// <summary>
        /// 비밀번호
        /// </summary>
        private string m_Password;
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// 비밀번호 확인
        /// </summary>
        private string m_PasswordConfirm;
        public string PasswordConfirm
        {
            get
            {
                return m_PasswordConfirm;
            }
            set
            {
                m_PasswordConfirm = value;
                RaisePropertyChanged("PasswordConfirm");
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
        /// 선택된 사원
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
                if (m_SelectedStaff == null)
                {
                    Name = "";
                    Rank = "";
                }
                else
                {
                    Name = m_SelectedStaff.STAFF_NAME;
                    Rank = m_SelectedStaff.STAFF_RANK;
                }
                RaisePropertyChanged("SelectedStaff");
            }
        }
        
        /// <summary>
        /// 선택된 사원 이름
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
        /// 선택된 사원 직급
        /// </summary>
        private string m_Rank;
        public string Rank
        {
            get
            {
                return m_Rank;
            }
            set
            {
                m_Rank = value;
                RaisePropertyChanged("Rank");
            }
        }

        /// <summary>
        /// 권한 리스트
        /// 00 : 루트
        /// 10 : CRUD
        /// 20 : CR
        /// 30 : R
        /// </summary>
        private ObservableCollection<string> m_AuthList;
        public ObservableCollection<string> AuthList
        {
            get
            {
                if (m_AuthList == null)
                {
                    m_AuthList = new ObservableCollection<string>
                    {
                        "10",
                        "20",
                        "30"
                    };
                }
                return m_AuthList;
            }
            set
            {
                m_AuthList = value;
                RaisePropertyChanged("AuthList");
            }
        }

        /// <summary>
        /// 선택된 권한
        /// </summary>
        private string m_SelectedAuth;
        public string SelectedAuth
        {
            get
            {
                return m_SelectedAuth;
            }
            set
            {
                m_SelectedAuth = value;
                RaisePropertyChanged("SelectedAuth");
            }
        }

        /// <summary>
        /// 등급 리스트
        /// 000 : 루트
        /// 100 : 모두 (관리장)
        /// 200 : 대표
        /// 300 : 이사
        /// 400 : 소속 부서
        /// 410 : 소속 팀
        /// </summary>
        private ObservableCollection<string> m_GradeList;
        public ObservableCollection<string> GradeList
        {
            get
            {
                if (m_GradeList == null)
                {
                    m_GradeList = new ObservableCollection<string>
                    {
                        "100",
                        "200",
                        "300",
                        "400",
                        "410"
                    };
                }
                return m_GradeList;
            }
            set
            {
                m_GradeList = value;
                RaisePropertyChanged("GradeList");
            }
        }

        /// <summary>
        /// 선택된 등급
        /// </summary>
        private string m_SelectedGrade;
        public string SelectedGrade
        {
            get
            {
                return m_SelectedGrade;
            }
            set
            {
                m_SelectedGrade = value;
                RaisePropertyChanged("SelectedGrade");
            }
        }

        /// <summary>
        /// 사유
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

        #endregion

        #region method

        private void LoadAdmin()
        {
            AdminList.Clear();
            SqlDataReader sqlData = null;
            if (SharedPreference.Instance.LoginAdmin.ADMIN_GRADE == "000")
            {
                sqlData = new AdminQuery().SelectAll();
            }
            else
            {
                sqlData = new AdminQuery().SelectAllWithoutRoot();
            }
            if (sqlData.HasRows)
            {
                while (sqlData.Read())
                {
                    AdminDTO dto = new AdminDTO
                    {
                        ADMIN_NUM = Int32.Parse(sqlData["num"].ToString()),
                        ADMIN_ID = sqlData["id"].ToString(),
                        ADMIN_PASSWORD = sqlData["password"].ToString(),
                        ADMIN_IDNUMBER = sqlData["idnumber"].ToString(),
                        ADMIN_NAME = sqlData["name"].ToString(),
                        ADMIN_RANK = sqlData["rank"].ToString(),
                        ADMIN_AUTH = sqlData["auth"].ToString(),
                        ADMIN_GRADE = sqlData["grade"].ToString()
                    };
                    AdminList.Add(new AdminModel(dto));
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

        private void LoadStaff()
        {
            StaffList.Clear();
            var sqlData = new StaffQuery().SelectAllNotRetirement();
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

        #endregion

        #region command

        /// <summary>
        /// loaded 커맨드
        /// </summary>
        public DelegateCommand LoadedCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    LoadAdmin();
                    LoadStaff();
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
                    var pb1 = uc.FindName("PB1") as PasswordBox;
                    var pb2 = uc.FindName("PB2") as PasswordBox;
                    Password = pb1.Password;
                    PasswordConfirm = pb2.Password;

                    if (Id == null || Id.Length == 0)
                    {
                        MessageBox.Show("아이디를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Password == null || Password.Length == 0)
                    {
                        MessageBox.Show("비밀번호를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (PasswordConfirm == null || PasswordConfirm.Length == 0)
                    {
                        MessageBox.Show("비밀번호를 한번 더 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Password != PasswordConfirm)
                    {
                        MessageBox.Show("비밀번호가 같지 않습니다. 정확히 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (SelectedStaff == null)
                    {
                        MessageBox.Show("사원을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    if (SelectedAuth == null)
                    {
                        MessageBox.Show("권한을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (SelectedGrade == null)
                    {
                        MessageBox.Show("등급을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    try
                    {
                        AdminDTO dto = new AdminDTO
                        {
                            ADMIN_ID = Id,
                            ADMIN_PASSWORD = Password,
                            ADMIN_IDNUMBER = SelectedStaff.STAFF_IDNUMBER,
                            ADMIN_NAME = Name,
                            ADMIN_RANK = Rank,
                            ADMIN_AUTH = SelectedAuth,
                            ADMIN_GRADE = SelectedGrade
                        };

                        var sqlData = new AdminQuery().SelectWithId(Id, Password);
                        if (sqlData.HasRows)
                        {
                            MessageBox.Show("같은 아이디가 이미 존재합니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            sqlData.Close();
                            SharedPreference.Instance.DBM.SqlConn.Close();
                            return;
                        }
                        sqlData.Close();
                        SharedPreference.Instance.DBM.SqlConn.Close();
                        try
                        {
                            new AdminQuery().Insert(dto);

                            try
                            {
                                AllLogDTO logDto = new AllLogDTO
                                {
                                    ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                    ALLLOG_WHAT = "관리자",
                                    ALLLOG_LOG = Name + "관리자 추가",
                                    ALLLOG_REASON = Reason,
                                    ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                                };

                                new AllLogQuery().Insert(logDto);

                                MessageBox.Show("관리자를 추가하셨습니다.");
                                Id = "";
                                pb1.Password = "";
                                pb2.Password = "";
                                Password = "";
                                PasswordConfirm = "";
                                SelectedStaff = null;
                                SelectedAuth = null;
                                SelectedGrade = null;
                                Reason = "";
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("관리자 추가 내역 저장을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                                if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                                {
                                    SharedPreference.Instance.DBM.SqlConn.Close();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("관리자 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("관리자 추가를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                        {
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 관리자 수정 커맨드
        /// </summary>
        public DelegateCommand<UserControl> EditCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    var index = AdminList.IndexOf(SelectedAdmin);

                    var pb1 = uc.FindName("PB1") as PasswordBox;
                    var pb2 = uc.FindName("PB2") as PasswordBox;
                    Password = pb1.Password;
                    PasswordConfirm = pb2.Password;

                    AdminDTO beforeDto = SelectedAdmin.Dto;
                    AdminDTO afterDto = new AdminDTO
                    {
                        ADMIN_ID = SelectedAdmin.ADMIN_ID,
                        ADMIN_PASSWORD = SelectedAdmin.ADMIN_PASSWORD,
                        ADMIN_IDNUMBER = SelectedAdmin.ADMIN_IDNUMBER,
                        ADMIN_NAME = SelectedAdmin.ADMIN_NAME,
                        ADMIN_RANK = SelectedAdmin.ADMIN_RANK,
                        ADMIN_AUTH = SelectedAdmin.ADMIN_AUTH,
                        ADMIN_GRADE = SelectedAdmin.ADMIN_GRADE
                    };

                    var what = $"관리자 사번: {SelectedAdmin.ADMIN_IDNUMBER}, ";
                    var log = $"";

                    if (IsCheckedPassword)
                    {
                        if (Password == null || Password.Length == 0)
                        {
                            MessageBox.Show("변경할 비밀번호를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        if (PasswordConfirm == null || PasswordConfirm.Length == 0)
                        {
                            MessageBox.Show("변경할 비밀번호를 한번 더 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        if (Password != PasswordConfirm)
                        {
                            MessageBox.Show("비밀번호가 같지 않습니다. 정확히 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        afterDto.ADMIN_PASSWORD = Password;

                        what += "비밀번호 ";
                        log += $"비밀번호: {beforeDto.ADMIN_PASSWORD} -> {afterDto.ADMIN_PASSWORD} ";
                    }

                    if (IsCheckedAuth)
                    {
                        if (SelectedAuth == null)
                        {
                            MessageBox.Show("변경할 권한을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        afterDto.ADMIN_AUTH = SelectedAuth;

                        what += "권한 ";
                        log += $"권한: {beforeDto.ADMIN_AUTH} -> {afterDto.ADMIN_AUTH} ";
                    }

                    if (IsCheckedGrade)
                    {
                        if (SelectedGrade == null)
                        {
                            MessageBox.Show("변경할 등급을 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        afterDto.ADMIN_GRADE = SelectedGrade;

                        what += "등급 ";
                        log += $"등급: {beforeDto.ADMIN_GRADE} -> {afterDto.ADMIN_GRADE} ";
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!IsCheckedPassword && !IsCheckedGrade && !IsCheckedAuth)
                    {
                        MessageBox.Show("변경 내역이 없습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    try
                    {
                        AdminList.Remove(SelectedAdmin);
                        AdminList.Insert(index, new AdminModel(afterDto));

                        new AdminQuery().Update(afterDto);

                        try
                        {
                            AllLogDTO logDto = new AllLogDTO
                            {
                                ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                ALLLOG_WHAT = what,
                                ALLLOG_LOG = log,
                                ALLLOG_REASON = Reason,
                                ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                            };

                            new AllLogQuery().Insert(logDto);

                            MessageBox.Show("관리자를 수정하셨습니다.");
                            IsCheckedPassword = false;
                            IsCheckedAuth = false;
                            IsCheckedGrade = false;
                            SelectedAdmin = null;
                            pb1.Password = "";
                            pb2.Password = "";
                            Password = "";
                            PasswordConfirm = "";
                            SelectedStaff = null;
                            SelectedAuth = null;
                            SelectedGrade = null;
                            Reason = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("관리자 변경 내역 저장을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("관리자 변경을 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                        if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                        {
                            SharedPreference.Instance.DBM.SqlConn.Close();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 관리자 삭제 커맨드
        /// </summary>
        public DelegateCommand<UserControl> DeleteCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    if (SelectedAdmin == null)
                    {
                        MessageBox.Show("삭제할 관리자를 선택해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (Reason == null || Reason.Length == 0)
                    {
                        MessageBox.Show("사유를 입력해주세요.", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    try
                    {
                        new AdminQuery().Delete(SelectedAdmin.ADMIN_NUM);
                        
                        // 삭제 내역 저장
                        try
                        {
                            AllLogDTO logDto = new AllLogDTO
                            {
                                ALLLOG_ADMIN = SharedPreference.Instance.LoginAdmin.ADMIN_ID,
                                ALLLOG_WHAT = "관리자 삭제",
                                ALLLOG_LOG = $"{SelectedAdmin.ADMIN_NAME}({SelectedAdmin.ADMIN_IDNUMBER}) 관리자 삭제",
                                ALLLOG_REASON = Reason,
                                ALLLOG_UPDATE_DATE = DateTime.Now.ToString()
                            };

                            new AllLogQuery().Insert(logDto);
                            MessageBox.Show("관리자를 삭제하셨습니다.");

                            AdminList.Remove(SelectedAdmin);
                            Reason = "";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("관리자 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
                            if (SharedPreference.Instance.DBM.SqlConn.State == System.Data.ConnectionState.Open)
                            {
                                SharedPreference.Instance.DBM.SqlConn.Close();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("관리자 삭제를 실패하셨습니다. 관리자에게 문의하세요.\n에러 내용 : " + e.Message);
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
