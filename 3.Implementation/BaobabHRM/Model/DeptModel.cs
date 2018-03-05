using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabHRM
{
    public class DeptModel : BindableBase
    {
        public DeptModel(DeptDTO dto)
        {
            this.Dto = dto;
        }

        private DeptDTO m_Dto;
        public DeptDTO Dto
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
        public int DEPT_CODE
        {
            get
            {
                return Dto.DEPT_CODE;
            }
            set
            {
                Dto.DEPT_CODE = value;
                RaisePropertyChanged("DEPT_CODE");
            }
        }
        
        /// <summary>
        /// 부서 이름
        /// </summary>
        public string DEPT_NAME
        {
            get
            {
                return Dto.DEPT_NAME;
            }
            set
            {
                Dto.DEPT_NAME = value;
                RaisePropertyChanged("DEPT_NAME");
            }
        }
    }
}
