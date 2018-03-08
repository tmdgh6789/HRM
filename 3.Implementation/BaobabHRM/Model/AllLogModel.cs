using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class AllLogModel : BindableBase
    {
        public AllLogModel(AllLogDTO dto)
        {
            this.Dto = dto;
        }
        
        private AllLogDTO m_Dto;
        public AllLogDTO Dto
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
        
        public string ALLLOG_ADMIN
        {
            get
            {
                return Dto.ALLLOG_ADMIN;
            }
            set
            {
                Dto.ALLLOG_ADMIN = value;
                RaisePropertyChanged("ALLLOG_ADMIN");
            }
        }

        public string ALLLOG_WHAT
        {
            get
            {
                return Dto.ALLLOG_WHAT;
            }
            set
            {
                Dto.ALLLOG_WHAT = value;
                RaisePropertyChanged("ALLLOG_WHAT");
            }
        }

        public string ALLLOG_LOG
        {
            get
            {
                return Dto.ALLLOG_LOG;
            }
            set
            {
                Dto.ALLLOG_LOG = value;
                RaisePropertyChanged("ALLLOG_LOG");
            }
        }

        public string ALLLOG_REASON
        {
            get
            {
                return Dto.ALLLOG_REASON;
            }
            set
            {
                Dto.ALLLOG_REASON = value;
                RaisePropertyChanged("ALLLOG_REASON");
            }
        }

        public string ALLLOG_UPDATE_DATE
        {
            get
            {
                return Dto.ALLLOG_UPDATE_DATE;
            }
            set
            {
                Dto.ALLLOG_UPDATE_DATE = value;
                RaisePropertyChanged("ALLLOG_UPDATE_DATE");
            }
        }
    }
}
