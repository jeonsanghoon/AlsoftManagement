using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LanguageToExcel
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            doQuery();
        }

        #region >> 검색
        
        private void btnFile_Click(object sender, EventArgs e)
        {
            doQuery();
        }

        /// <summary>
        /// 조회하기
        /// </summary>
        private void doQuery()
        {

           // string[] files = this.searchSHPFiles(this.textBox1.Text).ToArray(); //Directory.GetFiles(@"D:\Source\SVN\OnlineOrder38_New\IMUManager\5.Web\IMUOnline\Views", "*.xml", SearchOption.AllDirectories);
            
            IList<ListModel> listTotal = new List<ListModel>();
            List<string> files = new List<string>();
            

            if (this.textBox1.Text == "" || !new System.IO.DriveInfo(this.textBox1.Text).IsReady)
            {
                MessageBox.Show("올바른 뷰경로명을 입력하세요");
                this.textBox1.Focus();
                this.textBox1.Select();
                return;
            }

            System.IO.DirectoryInfo Di = new System.IO.DirectoryInfo(this.textBox1.Text);
            var arrUrl = this.textBox1.Text.Split('\\');
         
            this.FileSearch(ref files, Di , "*.xml");

            foreach (string fileUrl in files)
            {
                XElement root = XElement.Load(fileUrl);

                var list = (from el in root.Elements("Code")
                            select new ListModel
                            {
                                FileUrl = fileUrl.Replace(this.textBox1.Text,"")
                                ,Code = (string)el.Attribute("CodeId")
                            }).ToList();

                foreach (ListModel data in list)
                {
                    IEnumerable<XElement> langs =
                     from el in root.Elements("Code")
                     where (string)el.Attribute("CodeId") == data.Code
                     select el;

                    data.En = langs.First().Element("en").FirstNode.ToString().Replace("\r\n", "").Trim();
                    data.Ko = CheckEl(langs, "ko").FirstNode.ToString().Replace("\r\n", "").Trim();
                    data.Zh = CheckEl(langs, "zh").FirstNode.ToString().Replace("\r\n", "").Trim();
                    data.Ja = CheckEl(langs, "ja").FirstNode.ToString().Replace("\r\n", "").Trim();
                    data.Th = CheckEl(langs, "th").FirstNode.ToString().Replace("\r\n", "").Trim();
                    data.Ar = CheckEl(langs, "Ar").FirstNode.ToString().Replace("\r\n", "").Trim();
                }


                listTotal = listTotal.Union(list).ToList();
            }

            this.dataGridView1.DataSource = listTotal;

        }

        /// <summary>
        /// Element 체크 및 Default 설정
        /// </summary>
        /// <param name="langs"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private XElement CheckEl(IEnumerable<XElement> langs, string language)
        {
            return langs.First().Element(language) == null ? langs.First().Element("en") : langs.First().Element(language);
        }

        private void FileSearch(ref List<String> result, System.IO.DirectoryInfo root, string filter)
        {
            System.IO.DirectoryInfo[] subDirs = null;

            // 현재 디랙토리의 하위 디랙토리 목록을 가져옵니다.
            subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                Console.WriteLine(dirInfo.FullName); // <----------------------------------------------------------------출력부분

                foreach (string f in Directory.GetFiles(dirInfo.FullName, filter))
                {
                    result.Add(f);
                }
                // 재귀 호출을 통해 하위 디랙토리의 하위디랙토리를 탐색합니다.
                FileSearch(ref result, dirInfo, filter);
            }
        }
        #endregion

        #region>> Excel Download()
        private void btnExcel_Click(object sender, EventArgs e)
        {
            this.ExcelDownload();
        }


        /// <summary>
        /// ExcelDownload
        /// </summary>
        private void ExcelDownload()
        {
            try
            {
                // creating Excel Application
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();


                // creating new WorkBook within Excel application
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);


                // creating new Excelsheet in workbook
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // see the excel sheet behind the program
                app.Visible = true;

                // get the reference of first sheet. By default its name is Sheet1.
                // store its reference to worksheet
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;

                // changing the name of active sheet
                //worksheet.Name = "Exported from gridview";

                if (this.textBox1.Text.Contains("UPRetail.WebHostingDBManager_Language_noDtc"))
                {
                    worksheet.Name = "USMS_Amazon";
                }
                else if (this.textBox1.Text.Contains("IMUOnline"))
                {
                    worksheet.Name = "OnlineOrder";
                }

                else if (this.textBox1.Text.Contains("OnlineAppointment"))
                {
                    worksheet.Name = "OnlineAppointment";
                }

                // storing header part in Excel
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    worksheet.Cells[1, i].EntireRow.Font.Bold = true;
                    worksheet.Cells[1, i].EntireColumn.ColumnWidth = 30;
                    worksheet.Cells[1, i].Interior.Color = ColorTranslator.ToOle(Color.Yellow);
                }



                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count ; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                worksheet.UsedRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                // save the application
                workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                // Exit from the application
                app.Quit();
            }
            catch { }
        }
        #endregion
    }

    #region >> 조회 모델ListModel
    /// <summary>
    /// 조회 모델
    /// </summary>
    public class ListModel
    {
        public string FileUrl { get; set; }
        public string Code { get; set; }
        public string En { get; set; }
        public string Ko { get; set; }
        public string Zh { get; set; }
        public string Ja { get; set; }
        public string Th { get; set; }
        public string Ar { get; set; }
    }
    #endregion
}
