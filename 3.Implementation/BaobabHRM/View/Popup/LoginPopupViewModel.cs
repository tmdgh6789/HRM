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
    public class LoginPopupViewModel : BindableBase
    {

        private string m_id;
        public string id
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
                RaisePropertyChanged("id");
            }
        }
        
        private string m_password;
        public string password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
                RaisePropertyChanged("password");
            }
        }

        public DelegateCommand<UserControl> OkCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
                {
                    var passwordBox = (PasswordBox)uc.FindName("PB");
                    password = passwordBox.Password;
                    if (id == null || id.Length == 0)
                    {
                        return;
                    }
               
                    if (password == null || password.Length == 0)
                    {
                        return;
                    }
               
                    SharedPreference.Instance.Login(id, password);
                    if (Window.GetWindow(uc) != null)
                    {
                        Window.GetWindow(uc).DialogResult = true;
                    }
                });
            }
        }

        public DelegateCommand<UserControl> CancelCommand
        {
            get
            {
                return new DelegateCommand<UserControl>(delegate (UserControl uc)
               {
                   if (Window.GetWindow(uc) != null)
                   {
                       Window.GetWindow(uc).DialogResult = false;
                   }
               });
            }
        }

    }
}
