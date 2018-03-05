using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class RankModel : BindableBase
    {
        public RankModel(RankDTO dto)
        {
            this.Dto = dto;
        }

        private RankDTO m_Dto;
        public RankDTO Dto
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


        /// <summary>
        /// 부서 코드
        /// </summary>
        public int RANK_CODE
        {
            get
            {
                return Dto.RANK_CODE;
            }
            set
            {
                Dto.RANK_CODE = value;
                RaisePropertyChanged("RANK_CODE");
            }
        }

        /// <summary>
        /// 부서 이름
        /// </summary>
        public string RANK_NAME
        {
            get
            {
                return Dto.RANK_NAME;
            }
            set
            {
                Dto.RANK_NAME = value;
                RaisePropertyChanged("RANK_NAME");
            }
        }
    }
}
