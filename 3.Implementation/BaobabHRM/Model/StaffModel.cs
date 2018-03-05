using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class StaffModel : BindableBase
    {
        public StaffModel(StaffDTO dto)
        {
            this.Dto = dto;
        }

        private StaffDTO m_Dto;
        public StaffDTO Dto
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
        
        public string STAFF_IDNUMBER
        {
            get
            {
                return Dto.STAFF_IDNUMBER;
            }
            set
            {
                Dto.STAFF_IDNUMBER = value;
                RaisePropertyChanged("STAFF_IDNUMBER");
            }
        }
        
        public int STAFF_DEPT
        {
            get
            {
                return Dto.STAFF_DEPT;
            }
            set
            {
                Dto.STAFF_DEPT = value;
                RaisePropertyChanged("STAFF_DEPT");
            }
        }
        
        public int STAFF_RANK
        {
            get
            {
                return Dto.STAFF_RANK;
            }
            set
            {
                Dto.STAFF_RANK = value;
                RaisePropertyChanged("STAFF_RANK");
            }
        }
        
        public string STAFF_NAME
        {
            get
            {
                return Dto.STAFF_NAME;
            }
            set
            {
                Dto.STAFF_NAME = value;
                RaisePropertyChanged("STAFF_NAME");
            }
        }
        
        public string STAFF_ADDRESS
        {
            get
            {
                return Dto.STAFF_ADDRESS;
            }
            set
            {
                Dto.STAFF_ADDRESS = value;
                RaisePropertyChanged("STAFF_ADDRESS");
            }
        }
        
        public string STAFF_TEL
        {
            get
            {
                return Dto.STAFF_TEL;
            }
            set
            {
                Dto.STAFF_TEL = value;
                RaisePropertyChanged("STAFF_TEL");
            }
        }
        
        public string STAFF_JOIN_DAY
        {
            get
            {
                return Dto.STAFF_JOIN_DAY;
            }
            set
            {
                Dto.STAFF_JOIN_DAY = value;
                RaisePropertyChanged("STAFF_JOIN_DAY");
            }
        }
        
        public string STAFF_RETIREMENT_DAY
        {
            get
            {
                return Dto.STAFF_RETIREMENT_DAY;
            }
            set
            {
                Dto.STAFF_RETIREMENT_DAY = value;
                RaisePropertyChanged("STAFF_RETIREMENT_DAY");
            }
        }


        private string m_STAFF_NameAndIdnumber;
        public string STAFF_NameAndIdnumber
        {
            get
            {
                return Dto.STAFF_NAME + "(" + Dto.STAFF_IDNUMBER + ")";
            }
            set
            {
                m_STAFF_NameAndIdnumber = value;
                RaisePropertyChanged("STAFF_NameAndIdnumber");
            }
        }

    }
}
