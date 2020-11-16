using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;

namespace ALT.Framework.Mvc.Helpers
{
    public class FileInfo
    {
        //public  string FileUpload(HttpPostedFileBase file, string folderName = "")
        //{
        //    string fileExtension = System.IO.Path.GetExtension(file.FileName);
        //    string fileName = System.IO.Path.GetFileName(file.FileName);

        //    string UploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].ToString() + (string.IsNullOrEmpty(folderName) ? "" : folderName + "/");
        //    string oriUploadUrl = UploadUrl;
        //    UploadUrl = HttpContext.Current.Server.MapPath(UploadUrl);
        //    Global.FileInformation.MakeFolder(UploadUrl);
        //    string fileLocation = UploadUrl + fileName;
        //    if (System.IO.File.Exists(fileLocation))
        //    {
        //        System.IO.File.Delete(fileLocation);
        //    }
        //    file.SaveAs(fileLocation);
        //    return oriUploadUrl + fileName;
        //}
        public string FileUpload(HttpPostedFileBase file, string folderName = "")
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName);
            string fileName = System.IO.Path.GetFileName(file.FileName);
            string fileDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string UploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].ToString() + (string.IsNullOrEmpty(folderName) ? DateTime.Now.ToString("yyyyMM") + "/" : folderName + "/");
            string oriUploadUrl = UploadUrl;
            UploadUrl = HttpContext.Current.Server.MapPath(UploadUrl);
            Global.FileInformation.MakeFolder(UploadUrl);
            string fileLocation = UploadUrl + fileDate + fileExtension; //  fileName;
            //if (System.IO.File.Exists(fileLocation))
            //{
            //    System.IO.File.Delete(fileLocation);
            //}
            file.SaveAs(fileLocation);
            return fileName + "|" + oriUploadUrl + fileDate + fileExtension;
        }



        public FILE_INFO FileUpload2(HttpPostedFileBase file, string folderName = "")
        {
            FILE_INFO rtnFile = new FILE_INFO();
            try
            {
                rtnFile.FILE_EXT  = System.IO.Path.GetExtension(file.FileName);
                rtnFile.FILE_NAME = System.IO.Path.GetFileName(file.FileName);
                string fileDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                string UploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].ToString() + (string.IsNullOrEmpty(folderName) ? DateTime.Now.ToString("yyyyMM") + "/" : folderName + "/");
                string oriUploadUrl = UploadUrl;
                UploadUrl = HttpContext.Current.Server.MapPath(UploadUrl);
                Global.FileInformation.MakeFolder(UploadUrl);
                string fileLocation = UploadUrl + fileDate + rtnFile.FILE_EXT; //  fileName;

                file.SaveAs(fileLocation);
                rtnFile.URL = oriUploadUrl + fileDate + rtnFile.FILE_EXT;
                rtnFile.FULL_URL = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + rtnFile.URL;
                rtnFile.FULL_URL = rtnFile.FULL_URL.ConvertHttpsUrl();
            }
            catch (Exception ex)
            {
               rtnFile.return_msg = ex.Message;
            }
            return rtnFile;
        }


        public string FileDelete(string fileLocation)
        {
            try
            {
                string directoryUrl = string.Empty;
                string fileName = string.Empty;
                if(string.IsNullOrEmpty(fileLocation))
                {
                    string[] arrUrl = fileLocation.Split('/');
                    fileName = arrUrl[arrUrl.Count() - 1];

                    directoryUrl = fileLocation.Replace(fileName,"");
                    directoryUrl = HttpContext.Current.Server.MapPath(directoryUrl);
                    fileLocation = directoryUrl + fileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }
        public byte[] GetFileToByteArray( HttpPostedFileBase file)
        {
            var length = file.InputStream.Length; //Length: 103050706
            
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }

          
            return fileData;
        }

        public Size GetFileImageSize(Stream files)
        {
            using (var stream = files)
            {
                var image = System.Drawing.Image.FromStream(stream);

                return image.Size;
            }
        }
    }
}
