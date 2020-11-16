using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Net.Mail;

using System.Net.Configuration;
using System.Configuration;

namespace ALT.Framework.Mvc.Helpers
{
    public static class MailSection
    {
        public static SmtpSection section { get { return ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection; } }
    }

    public enum enAcceptType 
    {
        ALL, PERSONAL
    }
    public class MAILINFO
    {
      
       
        /// <summary>
        /// 메일 서버 IP : ex) 61.252.144.203 
        /// </summary>
        private string _smtpAddress;
        public string SMTP_ADDRESS {
            get { return (_smtpAddress == null) ? MailSection.section.Network.Host : _smtpAddress; ; }
            set
            {
                _smtpAddress = value;
            }
        }
        /// <summary>
        /// 메일서버 아이디 : ex) hancomms\webmaster@careercare.co.kr
        /// </summary>
        private string _smtpID;
        public string SMTP_ID {
            get { return _smtpID = (_smtpID == null) ? MailSection.section.Network.UserName : _smtpID; } 
            set{
                _smtpID = value;
            }
        }
        /// <summary>
        /// 메일서버 Password : ex) kyoreh0422
        /// </summary>
        private string _smtpPw;
        public string SMTP_PW 
        {
            get { return (_smtpPw == null) ? MailSection.section.Network.Password : _smtpPw; }
            set {
                _smtpPw = value;
            }
        }
        /// <summary>
        /// SMTP 포트
        /// </summary>
        private int? _smptPort;
        public int SMTP_PORT
        {
            get { return (int)((_smptPort == null || _smptPort == 0) ? MailSection.section.Network.Port : _smptPort); }
            set { _smptPort = value; }
        }


        /// <summary>
        /// SSL 사용여부
        /// </summary>
        private bool? _enableSsl;
        public bool EnableSsl
        {
            get { return Convert.ToBoolean((_enableSsl == null) ? _enableSsl : MailSection.section.Network.EnableSsl); }
            set { _enableSsl = value; }
        }

        private bool? _IsBodyHtml;
        public bool IsBodyHtml
        {
            get { return Convert.ToBoolean((_IsBodyHtml == null) ? true : _IsBodyHtml); }
            set { _IsBodyHtml = value; }
        }
        /// <summary>
        /// 보내는 사람 이메일 => SMTP가 지메일경우 지메일에서 설정된 메일로만 발송됨
        /// </summary>
        private string _senderId;
        public string SENDER_ID
        {
            get { return (_senderId == null) ? MailSection.section.From : _senderId; } 
            set{
                _senderId = value; ;
            }
        }
        /// <summary>
        /// 보내는 사람 이름 : 홍길동
        /// </summary>
        public string SENDER_NAME { get; set; }
        /// <summary>
        /// 첨부파일 경로
        /// </summary>
        public string FILE_PATH { get; set; }
        /// <summary>
        /// 받는 사람 이메일 : ex) abc@careercare.co.kr;def@careercare.co.kr
        /// </summary>
        public string ACCEPT_ID { get; set; }

        /// <summary>
        /// 받는 사람 메일 유형 0:받는사람에 모두추가/ 1: 각각으로 메일을 보냄
        /// </summary>
        private enAcceptType? _acceptType;
        public enAcceptType ACCEPT_TYPE
        {
            get
            {
                return (enAcceptType)((_acceptType == null) ? enAcceptType.ALL : _acceptType);
            }
            set{ _acceptType = value;}
        }
        /// <summary>
        /// 참조할 사람 이메일 : ex) abc@careercare.co.kr;def@careercare.co.kr
        /// </summary>
        public string CC_ID { get; set; }
        /// <summary>
        /// 메일 제목
        /// </summary>
        public string SUBJECT { get; set; }
        /// <summary>
        /// 메일 내용
        /// </summary>
        public string CONTENT { get; set; }

        /// <summary>
        /// 메일을 보낼때 입력한 메일주소의 customer_code 값을 넘김
        /// </summary>
        public string CUSTOMER_CODE { get; set; } 
        
    }
    public  class MailHelper
    {

        System.Threading.Thread thread1;
        System.Threading.ThreadStart ThreadStart1;
        MAILINFO  obMail;
        bool _bThread = true;
        public  string SendMail(MAILINFO ob, bool bThread = true)
        {
            string msg = string.Empty;
            try
            {
                obMail = ob;
                _bThread = bThread;
                if (bThread)
                {
                    ThreadStart1 = new System.Threading.ThreadStart(this.SendMailExec);
                    thread1 = new Thread(ThreadStart1);
                    thread1.Start();
                }
                else
                    this.SendMailExec();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return msg;
        }

        private void SendMailExec()
        {

            try
            {
                MAILINFO ob = obMail;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ob.SMTP_ADDRESS);
                mail.From = new MailAddress(ob.SENDER_ID, ob.SENDER_NAME, System.Text.Encoding.UTF8); //보내는 사람 설정

                
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
                    mail.Body = ob.CONTENT; /*+ ob.CUSTOMER_CODE;*///내용 설정
                ////추가
                //if (!string.IsNullOrEmpty(ob.CUSTOMER_CODE))
                //    mail.Body = ob.CUSTOMER_CODE;//내용에 customer_code 갖고오는지 확인 테스트


                mail.IsBodyHtml = ob.IsBodyHtml;
                mail.BodyEncoding = System.Text.Encoding.UTF8;//한글 입력하려면 써야한다는군요
                mail.SubjectEncoding = System.Text.Encoding.UTF8;//마찬가지


                if (!string.IsNullOrEmpty(ob.FILE_PATH))
                {
                    System.Net.Mail.Attachment attachment;//첨부파일 만들기
                    attachment = new System.Net.Mail.Attachment(ob.FILE_PATH);//첨부파일 붙이기
                    mail.Attachments.Add(attachment);//첨부파일 붙이기
                }
                SmtpServer.Port = ob.SMTP_PORT;//쥐메일 포트 설정
                SmtpServer.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
                SmtpServer.Credentials = new System.Net.NetworkCredential(ob.SMTP_ID, ob.SMTP_PW);//쥐메일 인증을 위한 아이디 비밀번호
                if (ob.SMTP_ADDRESS.Contains("gmail")) SmtpServer.EnableSsl  = true; 
                else
                    SmtpServer.EnableSsl = ob.EnableSsl;//SSL 설정 (쥐메일은 트루해야해요)

                if (ob.ACCEPT_TYPE == enAcceptType.PERSONAL) //개별적으로 발송
                {
                    if (!string.IsNullOrEmpty(ob.ACCEPT_ID))
                    {
                        string[] arrAc = ob.ACCEPT_ID.Split(';');
                        foreach (string Data1 in arrAc)
                        {
                            if (!string.IsNullOrEmpty(Data1))
                            {
                                mail.To.Add(Data1); //받는 사람 설정
                                SmtpServer.Send(mail);// 메일 발송
                                mail.To.RemoveAt(0);
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ob.ACCEPT_ID))
                    {
                        string[] arrAc = ob.ACCEPT_ID.Split(';');
                        foreach (string Data1 in arrAc)
                        {
                            if (!string.IsNullOrEmpty(Data1))
                                mail.To.Add(Data1);  //받는 사람 설정
                        }
                    }
                    SmtpServer.Send(mail);// 메일 발송
                }

                if (_bThread)
                {
                    thread1.Abort();
                }
            }
            catch (Exception ex)
            {
                if (_bThread)
                {
                    thread1.Abort();
                }

                throw ex;
            }
        }
    }
}