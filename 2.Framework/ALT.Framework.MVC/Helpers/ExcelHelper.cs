using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace ALT.Framework.Mvc.Helpers
{
    /// <summary>
    /// List Helper
    /// </summary>
    public static class ListHelper
    {

        public static DataTable ConvertToDataTable<T>(this IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
               // table.Columns.Add(prop.Name, prop.PropertyType);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            return table;
        }
    }
   

    /// <summary>
    /// ExcelHelper
    /// </summary>
    public static class ExcelHelper
    {

        #region >> 폴더 만들기
        public static bool MakeFolder(string FolderUrl)
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

        #region >> Excel UPload
        public static DataSet ExcelUpload(HttpPostedFileBase file)
        {
            DataSet ds = new DataSet();

            if (file.ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(file.FileName);
                string UploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].ToString() + DateTime.Now.ToString("yyyyMMdd") + "/";
                UploadUrl = HttpContext.Current.Server.MapPath(UploadUrl);
                MakeFolder(UploadUrl);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {

                    string fileLocation = UploadUrl + file.FileName.Replace(fileExtension, "") + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExtension;
                    if (System.IO.File.Exists(fileLocation))
                    {

                        System.IO.File.Delete(fileLocation);
                    }
                    file.SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {

                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    System.Data.OleDb.OleDbConnection excelConnection = new System.Data.OleDb.OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    string query = string.Empty; // string.Format("Select * from [{0}]", excelSheets[0]);
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        query = string.Format(" Select * from [{0}]", excelSheets[t]);

                        System.Data.OleDb.OleDbConnection excelConnection1 = new System.Data.OleDb.OleDbConnection(excelConnectionString);


                        //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (System.Data.OleDb.OleDbDataAdapter dataAdapter = new System.Data.OleDb.OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(ds, excelSheets[t]);
                        }
                        t++;
                    }

                }
                else if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = HttpContext.Current.Server.MapPath(UploadUrl) + HttpContext.Current.Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    HttpContext.Current.Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                    // DataSet ds = new DataSet();
                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }
            }
            System.Text.StringBuilder sbSql = new System.Text.StringBuilder();



            //string sInsert = "INSERT INTO ExcelUploadData";
            //for (int nSheet = 0; nSheet < ds.Tables.Count; nSheet++)
            //{
            //    DataTable dt = ds.Tables[nSheet];
            //    for (int nRow = 0; nRow < dt.Rows.Count; nRow++)
            //    {
            //        sbSql.Append("\n").Append(" SELECT ");
            //        for (int nCol = 1; nCol <= dt.Columns.Count; nCol++)
            //        {

            //        }

            //        for (int n = 12 - dt.Columns.Count; n <= 12; n++)
            //        {
            //            sbSql.Append(", null");
            //        }

            //        sbSql.Append(", " + SessionHelper.UserInfo.AccountCode + ", GETDATE()" );
            //        if ((nRow + 1) % 10 == 0)
            //        {
            //            sbSql = new System.Text.StringBuilder();
            //            sbSql.Append("\n").Append(sInsert);
            //        }
            //    }
            //}
            return ds;
        }


        /// <summary>
        /// ExcelReport
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="Title"></param>
        /// <param name="HeaderNames"></param>
        public static  FileContentResult ToExcel<T>(this List<T> list, string Title = "Excel Report", IList<string> HeaderNames = null)
        {
            
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Result");
                //get our column headings
                var t = typeof(T);
                System.Reflection.PropertyInfo[] Headings = t.GetProperties();

                #region >> Title
                ws.Cells[1, 1].Value = Title;

                ws.Cells[1, 1, 1, Headings.Count()].Merge = true;
                // Setting Font and Alignment for Header
                ws.Cells[1, 1, 1, Headings.Count()].Style.Font.Size = 14;
                ws.Cells[1, 1, 1, Headings.Count()].Style.Font.Bold = true;
                ws.Cells[1, 1, 1, Headings.Count()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                #endregion


                int total = list.Count();
                //populate our Data
                if (total > 0)
                {
                    ws.Cells["A3"].LoadFromCollection(list);
                }

                #region >> Header
                for (int i = 0; i < Headings.Count(); i++)
                {

                    if (HeaderNames == null) ws.Cells[2, i + 1].Value = Headings[i].Name;
                    else
                    {
                        var arrHeader = HeaderNames[i].Split('|');
                        ws.Cells[2, i + 1].Value = arrHeader[0];

                        if (arrHeader.Length == 2)
                        {
                            switch(arrHeader[1])
                            {
                                case "LINK":
                                    for(int nRow = 3; nRow <list.Count() + 3; nRow ++)
                                    {
                                        if (ws.Cells[nRow, i + 1].Value != null && ws.Cells[nRow, i + 1].Value.ToString() !="")
                                        {
                                            ws.Cells[nRow, i + 1].Formula = "HYPERLINK(\"" + ws.Cells[nRow, i + 1].Value + "\",\"" +  "보기" + "\")";
                                            ws.Cells[nRow, i + 1].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                            ws.Cells[nRow, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    if (Headings[i].PropertyType == typeof(DateTime?)
                        || Headings[i].PropertyType == typeof(DateTime)
                        )
                    {
                        ws.Column(i + 1).Style.Numberformat.Format = "yyyy/mm/dd hh:mm AM/PM";
                    }
                    /*  else if (Headings[i].PropertyType == typeof(int?)
                          || Headings[i].PropertyType == typeof(int))
                      {
                          ws.Column(i + 1).Style.Numberformat.Format = "#,##0";
                      }*/
                    else if (Headings[i].PropertyType == typeof(double?)
                        || Headings[i].PropertyType == typeof(double)
                        || Headings[i].PropertyType == typeof(float?)
                        || Headings[i].PropertyType == typeof(float)
                        || Headings[i].PropertyType == typeof(decimal?)
                        || Headings[i].PropertyType == typeof(decimal)

                        )
                    {
                        ws.Column(i + 1).Style.Numberformat.Format = "#,##0.00";
                    }
                    else if (Headings[i].Name.ToUpper().Contains("_CODE"))
                    {
                        ws.Column(i + 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    else if (Headings[i].PropertyType == typeof(int)
                        || Headings[i].PropertyType == typeof(int?)
                        || Headings[i].PropertyType == typeof(long)
                        || Headings[i].PropertyType == typeof(long?))
                    {
                        ws.Column(i + 1).Style.Numberformat.Format = "#,##0";
                    }
                }
                
                //
                #endregion
                //Format the header
                using (ExcelRange rng = ws.Cells[2, 1, 2, Headings.Count()])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(243, 243, 243));

                }

                using (ExcelRange rng = ws.Cells[2, 1, total + 2, Headings.Count()])
                {
                    rng.AutoFitColumns();
                    var border = rng.Style.Border;
                    border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    border.Top.Color.SetColor(System.Drawing.Color.DarkGray);
                    border.Left.Color.SetColor(System.Drawing.Color.DarkGray);
                    border.Right.Color.SetColor(System.Drawing.Color.DarkGray);
                    border.Bottom.Color.SetColor(System.Drawing.Color.DarkGray);
                }
                
                //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //HttpContext.Current.Response.Charset = "utf-8";
                //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + Title + ".xlsx");
                //HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());

                var result = new FileContentResult(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                result.FileDownloadName = Title + ".xlsx";
                return result;

            }
        }
       
        #region >> Sample Excel

        // Creating Excel Worksheet
        private static ExcelWorksheet CreateSheet(ExcelPackage excelPkg, string sheetName)
        {
            ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets.Add(sheetName);
            // Setting default font for whole sheet
            oSheet.Cells.Style.Font.Name = "Calibri";
            // Setting font size for whole sheet
            oSheet.Cells.Style.Font.Size = 11;
            return oSheet;
        }

        /// <summary>
        /// Creating formatted header of excel sheet
        /// </summary>
        /// <param name="oSheet">The ExcelWorksheet object</param>
        /// <param name="rowIndex">The row number where the header will put</param>
        /// <param name="dt">The DataTable object from where header values will come</param>
        private static void CreateHeader(ExcelWorksheet oSheet, ref int rowIndex, DataTable dt)
        {
            int colIndex = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                var cell = oSheet.Cells[rowIndex, colIndex];

                // Setting the background color of header cells to Gray
                var fill = cell.Style.Fill;
                fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);

                // Setting top/left, right/bottom border of header cells
                var border = cell.Style.Border;

                border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                cell.Style.Font.Size = 12;
                cell.Style.Font.Bold = true;
                // Setting value in cell
                cell.Value = dc.ColumnName;

                colIndex++;
            }
        }

        /// <summary>
        /// Putting Data into Excel Cells
        /// </summary>
        /// <param name="oSheet">The ExcelWorksheet object</param>
        /// <param name="rowIndex">The row number from where data will put</param>
        /// <param name="dt">The DataTable object from where data will come</param>
        private static void CreateData(ExcelWorksheet oSheet, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataRow dr in dt.Rows)
            {
                colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = oSheet.Cells[rowIndex, colIndex];

                    // Setting value in the cell
                    cell.Value = Convert.ToInt32(dr[dc.ColumnName]);

                    // Setting border of the cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    colIndex++;
                }
            }
        }

        /// <summary>
        /// Creating formatted footer in the excel sheet
        /// </summary>
        /// <param name="oSheet">The ExcelWorksheet object</param>
        /// <param name="rowIndex">The row number where the footer will put</param>
        /// <param name="dt">The DataTable object from where footer values will come</param>
        private static void CreateFooter(ExcelWorksheet oSheet, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            // Creating Formula in Footer
            foreach (DataColumn dc in dt.Columns)
            {
                colIndex++;
                var cell = oSheet.Cells[rowIndex, colIndex];

                // Setting Sum Formula for each cell
                // Usage: Sum(From_Addres:To_Address)
                // e.g. - Sum(A3:A6) -> Sums the value of Column 'A' From Row 3 to Row 6
                cell.Formula = "Sum(" + oSheet.Cells[3, colIndex].Address + ":" + oSheet.Cells[rowIndex - 1, colIndex].Address + ")";

                // Setting Background Fill color to Gray
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);
            }
        }

        /// <summary>
        /// Adding custom comment in specified cell of specified excel sheet
        /// </summary>
        /// <param name="oSheet">The ExcelWorksheet object</param>
        /// <param name="rowIndex">The row number of the cell where comment will put</param>
        /// <param name="colIndex">The column number of the cell where comment will put</param>
        /// <param name="comment">The comment text</param>
        /// <param name="author">The author name</param>
        private static void AddComment(ExcelWorksheet oSheet, int rowIndex, int colIndex, string comment, string author)
        {
            // Adding a comment to a Cell
            oSheet.Cells[rowIndex, colIndex].AddComment(comment, author);
        }
        #endregion
        #endregion
    }
}