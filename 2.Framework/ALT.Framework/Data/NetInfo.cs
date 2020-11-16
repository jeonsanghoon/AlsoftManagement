using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;


namespace ALT.Framework.Data
{
    #region >> Network 정보를 관리하는 클래스
    /// <summary>
    /// -------------------------------------------------------------------------------------------------------------
    /// 클래스이름	:	NetInfo
    /// 설	   명	:	Network 정보를 관리하는 클래스
    /// 작  성  자	:	전상훈
    /// 최초작성일	:	2011년 1월 20일
    /// 최종수정일   :   2011년 1월 20일
    /// -------------------------------------------------------------------------------------------------------------
    /// 수정  내역	:	
    ///					
    /// </summary>
    public class NetInfo
    {
        /// <summary>
        /// IP Address
        /// </summary>
        public string IP_ADDRESS { get; set; }
        /// <summary>
        /// 네트워크카드
        /// </summary>
        public string NETWORK_CARD { get; set; }
        /// <summary>
        /// MAC 주소
        /// </summary>
        public string MAC_ADDRESS { get; set; }
        /// <summary>
        /// 서브넷 마스크
        /// </summary>
        public string SUBNET_MASK { get; set; }
        /// <summary>
        /// 게이트웨이
        /// </summary>
        public string GATE_WAY { get; set; }
        public NetInfo()
        {
            GetNetInfo();
        }

        /// <summary>
        /// 네트우크정보 가져오기
        /// </summary>
        private void GetNetInfo()
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher
                ("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");
            ManagementObjectCollection queryCol = query.Get();
            foreach (ManagementObject mo in queryCol)
            {
                string[] address = (string[])mo["IPAddress"];
                string[] subnets = (string[])mo["IPSubnet"];
                string[] defaultgateways = (string[])mo["DefaultIPGateway"];
                NETWORK_CARD = mo["Description"].ToString();
                MAC_ADDRESS = mo["MACAddress"].ToString();

                if (address != null)
                {
                    foreach (string ipaddress in address)
                    {
                        IP_ADDRESS = ipaddress;
                    }
                }
                else
                    IP_ADDRESS = string.Empty;

                if (subnets != null)
                {
                    foreach (string subnet in subnets)
                    {
                        SUBNET_MASK = subnet;
                    }
                }
                else
                    SUBNET_MASK = string.Empty;

                if (defaultgateways != null)
                {
                    foreach (string defaultgateway in defaultgateways)
                    {
                        GATE_WAY = defaultgateway;
                    }
                }
                else
                    GATE_WAY = string.Empty;
            }
        }



       /* System.Threading.Thread thread1;
        System.Threading.ThreadStart ThreadStart1;
        UPS.Vo.Common.MAILINFO obMail;
        public string SendMail(UPS.Vo.Common.MAILINFO ob)
        {
            string msg = string.Empty;
            try
            {
                obMail = ob;
                ThreadStart1 = new System.Threading.ThreadStart(this.SendMailExec);
                thread1 = new Thread(ThreadStart1);
                thread1.Start();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        public void SendMailExec()
        {

            try
            {
                UPS.Vo.Common.MAILINFO ob = obMail;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ob.SMTP_ADDRESS);
                mail.From = new MailAddress(ob.SENDER_ID, ob.SENDER_NAME, System.Text.Encoding.UTF8); //보내는 사람 설정
                if (!string.IsNullOrEmpty(ob.ACCEPT_ID))
                {
                    string[] arrAc = ob.ACCEPT_ID.Split(';');
                    foreach (string Data1 in arrAc)
                    {
                        if (!string.IsNullOrEmpty(Data1))
                            mail.To.Add(Data1);  //받는 사람 설정
                    }
                }

                if (!string.IsNullOrEmpty(ob.CC_ID))
                {
                    string[] arrCC = ob.CC_ID.Split(';');
                    foreach (string ccData in arrCC)
                    {
                        if (!string.IsNullOrEmpty(ccData))
                            mail.CC.Add(ccData); //참조메일 설정
                    }
                }
                if (!string.IsNullOrEmpty(ob.SUBJECT))
                    mail.Subject = ob.SUBJECT;//제목 설정
                if (!string.IsNullOrEmpty(ob.CONTENT))
                    mail.Body = ob.CONTENT;//내용 설정
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;//한글 입력하려면 써야한다는군요
                mail.SubjectEncoding = System.Text.Encoding.UTF8;//마찬가지


                if (!string.IsNullOrEmpty(ob.FILE_PATH))
                {
                    System.Net.Mail.Attachment attachment;//첨부파일 만들기
                    attachment = new System.Net.Mail.Attachment(ob.FILE_PATH);//첨부파일 붙이기
                    mail.Attachments.Add(attachment);//첨부파일 붙이기
                }
                SmtpServer.Port = 25;//쥐메일 포트 설정
                SmtpServer.UseDefaultCredentials = true; // 시스템에 설정된 인증 정보를 사용하지 않는다.

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
                SmtpServer.Credentials = new System.Net.NetworkCredential(ob.SMTP_ID, ob.SMTP_PW);//쥐메일 인증을 위한 아이디 비밀번호
                SmtpServer.EnableSsl = false;//SSL 설정 (쥐메일은 트루해야해요)

                SmtpServer.Send(mail);// 메일 발송
                thread1.Abort();
        * 
            }
            catch (Exception ex)
            {
                thread1.Abort();

                throw ex;
            }


        }*/

        /// <summary>
        /// 모든 IPv4 Address 구하기
        /// </summary>
        /// <returns>IPv4 Address 문자열 배열</returns>
        public string[] GetAllIPv4Address()
        {
            List<string> ipv4s = new List<string>();
            Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");

            foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList)
            {
                if (regex.IsMatch(ip.ToString()))
                {
                    ipv4s.Add(ip.ToString());
                }
            }

            return ipv4s.ToArray();
        }


        /// <summary>
        /// 유선 IPv4 Address 구하기
        /// </summary>
        /// <returns>유선 IPv4 Address 문자열 배열</returns>
        public string[] GetWiredIPv4Address()
        {
            return GetIPv4Address(true);
        }

        /// <summary>
        /// 무선 IPv4 Address 구하기
        /// </summary>
        /// <returns>무선 IPv4 Address 문자열 배열</returns>
        public string[] GetWirelessIPv4Address()
        {
            return GetIPv4Address(false);
        }

        /// <summary>
        /// 유선 or 무선 IPv4 Address 구하기
        /// </summary>
        /// <param name="wired">유선을 찾고 싶으면 true</param>
        /// <returns>IPv4 Address 문자열 배열</returns>
        public string[] GetIPv4Address(bool wired)
        {
            // IPv4 검사할 정규식
            Regex regex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");

            List<string> ipv4s = new List<string>();
            string[] wifiNames = GetWifiNetAdapterNames();

            try
            {
                ManagementObjectSearcher objSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                ManagementObjectCollection objCollection = objSearcher.Get();

                foreach (ManagementObject obj in objCollection)
                {
                    // wired값에 따라 무선 or 유선인것만 찾기
                    if (wired && Array.IndexOf(wifiNames, (string)obj["Description"]) < 0 || !wired && Array.IndexOf(wifiNames, (string)obj["Description"]) > -1)
                    {
                        foreach (string address in (string[])obj["IPAddress"])
                        {
                            if (regex.IsMatch(address) && address != "0.0.0.0")
                            {
                                ipv4s.Add(address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }

            // 반환
            return ipv4s.ToArray();
        }

        /// <summary>
        /// 무선 Network Adapter 이름 구하기
        /// </summary>
        /// <returns>무선 Network Adapter 문자열 배열</returns>
        public string[] GetWifiNetAdapterNames()
        {
            List<string> names = new List<string>();

            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM MSNdis_80211_ReceivedSignalStrength Where active = true");
                ManagementScope scope = new ManagementScope("root\\wmi");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject obj in searcher.Get())
                {
                    if ((bool)obj["Active"] == true)
                    {
                        names.Add((string)obj["InstanceName"]);
                    }
                }
            }
            catch (ManagementException mex)
            {
                if (mex.ErrorCode != ManagementStatus.NotSupported)
                {
                    Console.WriteLine("Error : " + mex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }

            return names.ToArray();
        }

    }
    #endregion

}
