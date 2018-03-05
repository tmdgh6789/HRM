using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class UserModel : BindableBase
    {
        public UserModel(UserDTO dto)
        {
            this.Dto = dto;
        }


        private UserDTO m_Dto;
        public UserDTO Dto
        {
            get
            {
                return m_Dto;
            }
            set
            {
                m_Dto = value;
                RaisePropertyChanged("Dto");
            }
        }

        
        public string USER_ID
        {
            get
            {
                return Dto.USER_ID;
            }
            set
            {
                Dto.USER_ID = value;
                RaisePropertyChanged("USER_ID");
            }
        }
        
        public string USER_PW
        {
            get
            {
                return Dto.USER_PW;
            }
            set
            {
                Dto.USER_PW = value;
                RaisePropertyChanged("USER_PW");
            }
        }


    }
}
