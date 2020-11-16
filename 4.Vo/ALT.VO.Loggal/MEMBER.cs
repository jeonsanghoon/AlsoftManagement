using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class MEMBER_JOIN
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string PHONE { get; set; }
        public string BIRTH { get; set; }
        public int GENDER { get; set; }
        /// <summary>
        /// T_COMPANY의 COMPANY_NAME 저장
        /// </summary>
        public string COMPANY_NAME { get; set; }
        

        public string ZIP_CODE { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }

        public string BUSI_TYPE { get; set; }
        public int COMPANY_TYPE { get; set; }
        /// <summary>
        ///  회사유형2 T_COMPANY 테이블  7:개인 8:기업
        /// </summary>
        public int COMPANY_TYPE2 { get; set; }
        public string BUSI_REG_NUMBER { get; set; }

        public string SHARE_AUTH_NUMBER { get; set; }

        public int? REG_CODE { get; set; }
    }
}
