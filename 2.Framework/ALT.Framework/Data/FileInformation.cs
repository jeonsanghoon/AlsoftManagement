using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net;
using log4net.Config;

namespace ALT.Framework.Data
{
    /// <summary>
    /// -------------------------------------------------------------------------------------------------------------
    /// 클래스이름	:	FileInformation
    /// 설	   명	:	파일정보을 관리하는 클래스
    /// 작  성  자	:	전상훈
    /// 최초작성일	:	2013년 05월 21일
    /// 최종수정일  :   2013년 05월 21일
    /// -------------------------------------------------------------------------------------------------------------
    /// 수정  내역	:	
    ///					
    /// </summary>
    public class FileInformation
    {

        public FileInformation()
        {
        }
        #region >> 폴더 만들기
        public bool MakeFolder(string FolderUrl)
        {
            try
            {
                //디렉토리의 정보를 가지고 있는 DirectoryInfo클래스를 데리고 옵니다. 
                DirectoryInfo f = new DirectoryInfo(FolderUrl);
                if (!f.Exists)
                {
                    f.Create(); //폴더를 생성합니다
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            { }
            return false;
        }
        #endregion
        #region >> 파일을 String로 Convert
        public string FromFileToString(string path)
        {

            string[] arrText = System.IO.File.ReadAllLines(path);
            StringBuilder sbText = new StringBuilder();
            foreach (string str in arrText)
            {
                sbText.Append(str);
            }
            return sbText.ToString();
        }

        public string FromFileToString<T>(string path, IList<T> list)
        {

            string[] arrText = System.IO.File.ReadAllLines(path);
            StringBuilder sbText = new StringBuilder();
            foreach (string str in arrText)
            {
                sbText.Append(str);
            }
            string sContent = sbText.ToString();

            System.Data.DataTable dt = Global.Format.ConvertToDataTable(list);

            for (int nRow = 0; nRow < dt.Rows.Count; nRow++)
            {
                for (int nCol = 0; nCol < dt.Columns.Count; nCol++)
                {
                    sContent = sContent.Replace("#[" + dt.Columns[nCol].ColumnName + "]", dt.Rows[nRow][nCol].ToString());
                }
            }

            return sContent;
        }
        #endregion
        public void Log4NetWrite(ref ILog log, string msg, GlobalEnum.LogType logType = GlobalEnum.LogType.Info)
        {
            var applicationBasePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var configPath = applicationBasePath + "bin\\Data\\log4net.config";
            XmlConfigurator.Configure(new System.IO.FileInfo(configPath));
            
            switch (logType)
            {
                case GlobalEnum.LogType.Info :
                    log.Info(msg);
                    break;
                case GlobalEnum.LogType.Debug:
                    log.Debug(msg);
                    break;
                case GlobalEnum.LogType.Warn:
                    log.Warn(msg);
                    break;
                case GlobalEnum.LogType.Error:
                    log.Error(msg);
                    break;
                case GlobalEnum.LogType.Fatal:
                    log.Fatal(msg);
                    break;
            }
        }

        
    }
}
