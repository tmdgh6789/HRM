using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AdminModel : BindableBase
    {
        public AdminModel(AdminDTO dto)
        {
            this.Dto = dto;
        }


        private AdminDTO m_Dto;
        public AdminDTO Dto
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

        
        public string ADMIN_ID
        {
            get
            {
                return Dto.ADMIN_ID;
            }
            set
            {
                Dto.ADMIN_ID = value;
                RaisePropertyChanged("ADMIN_ID");
            }
        }
        
        public string ADMIN_PASSWORD
        {
            get
            {
                return Dto.ADMIN_PASSWORD;
            }
            set
            {
                Dto.ADMIN_PASSWORD = value;
                RaisePropertyChanged("ADMIN_PASSWORD");
            }
        }

        public string ADMIN_NAME
        {
            get
            {
                return Dto.ADMIN_NAME;
            }
            set
            {
                Dto.ADMIN_NAME = value;
                RaisePropertyChanged("ADMIN_NAME");
            }
        }

        public string ADMIN_RANK
        {
            get
            {
                return Dto.ADMIN_RANK;
            }
            set
            {
                Dto.ADMIN_RANK = value;
                RaisePropertyChanged("ADMIN_RANK");
            }
        }
        
        public string ADMIN_GRADE
        {
            get
            {
                return Dto.ADMIN_GRADE;
            }
            set
            {
                Dto.ADMIN_GRADE = value;
                RaisePropertyChanged("ADMIN_GRADE");
            }
        }
    }
}
