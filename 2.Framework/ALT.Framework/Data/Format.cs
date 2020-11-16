using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using ALT.Framework;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ALT.Framework.Data
{
    public class Format
    {

        //public string ToString(object value, string defaultValue = "", bool bHtml = false)
        //{


        //    if (Global.Format.IsNumeric(value))
        //        return (value == null || (value.GetType().Name.ToUpper() != "STRING" && value.ToString() == "0")) ? defaultValue : value.ToString();
        //    else
        //    {
        //        if (value == null)
        //        {
        //            return defaultValue;
        //        }
        //        else
        //        {
        //            string[] arrData = value.ToString().Split('-');
        //            if (arrData.Length == 3 && value.ToString().Length < 20) //전화번호 날짜일때 제외
        //            {
        //                return value.ToString();
        //            }
        //            else
        //            {
        //                if (bHtml)
        //                    return (value == null) ? defaultValue : Global.SecurityInfo.getGetSafeHtml(value.ToString());
        //                else
        //                    return (value == null) ? defaultValue : Global.SecurityInfo.getSqlInjectIon(value.ToString());
        //            }
        //        }

        //    }
        //}



      
        /// <summary>
        /// 나이가져오기
        /// </summary>
        /// <param name="birth"></param>
        /// <returns></returns>
        public int GetAge(string birth)
        {

            try
            {
                birth = birth.Substring(0, 4);
                return Convert.ToInt32(DateTime.Now.Year - Convert.ToInt32(birth.Substring(0, 4)) + 1);
            }
            catch (Exception)
            { }
            return 0;
        }
        /// <summary>
        /// 만 나이가져오기
        /// </summary>
        /// <param name="birth"></param>
        /// <returns></returns>
        public int GetAge2(string birth)
        {

            try
            {
                if (string.IsNullOrEmpty(birth) || birth.Length < 8)
                    return 0;
                birth = this.RemoveDateFormat(birth);
                birth = birth.Substring(0, 4) + "-" + birth.Substring(4, 2) + "-" + birth.Substring(6, 2);
                TimeSpan ts = DateTime.Now - Convert.ToDateTime(birth);
                decimal dYear = Convert.ToDecimal(ts.Days) / Convert.ToDecimal(365);

                return Convert.ToInt32(Math.Floor(dYear));
            }
            catch (Exception)
            { }
            return 0;
        }
        
        /// <summary>
        /// 유일한 문자 만들기
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public string MakeUniqueString(int maxSize)
        {
            char[] chars = new char[45];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider identity = new RNGCryptoServiceProvider();
            identity.GetNonZeroBytes(data);
            data = new byte[maxSize];
            identity.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        #region >> 숫자를 체크하는 함수
        public bool IsNumeric(object value)
        {
            try
            {
                int numChk = 0;
                bool isNum = int.TryParse(value.ToString(), out numChk);
                if (!isNum)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception) {
                return false;
            }
            
        }
        #endregion

        #region >> 숫자만 가져오는 함수
        public string GetOnlyNumber(string value)
        {
            // System.Text.RegularExpressions
            return Regex.Replace(value, @"\D", "");
        }

        #endregion

        #region >> 리스트를 데이터 테이블로 변환
        public DataTable ConvertToDataTable<T>(IList<T> list)
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

        public IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public IList<T> ConvertToList<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
        #endregion

        #region >>특정월에 요일에 해당 하는 일자 가져오기
        /// <summary>
        /// 특정월에 요일에 해당 하는 일자 가져오기
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        public List<DateTime> GetDates(int year, int month, params int[] weeks)
        {
            var list = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             .Select(day => new DateTime(year, month, day))
                             ;
            foreach (int nweek in weeks)
            {
                list = list.Where(w => w.DayOfWeek == (DayOfWeek)nweek);
            }
            return list.ToList();
        }
        public List<DateTime> GetDates(string year, string month, params int[] weeks)
        {
            var list = Enumerable.Range(1, DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month)))
                             .Select(day => new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), day))
                             ;
            foreach (int nweek in weeks)
            {
                list = list.Where(w => w.DayOfWeek == (DayOfWeek)nweek);
            }
            return list.ToList();
        }
        #endregion

      

        public string ConvertFromToFormatDate(string frDate, string toDate, string format = "yyyy.mm.dd", string gubun = "~")
        {
            return (string.IsNullOrEmpty(frDate) ? "" : frDate.ToString(format)) + gubun + (string.IsNullOrEmpty(toDate) ? "" : frDate.ToString(toDate));
        }
        /// <summary>
        /// 날짜포맷 없애기
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string RemoveDateFormat(string sDate)
        {
            if (string.IsNullOrEmpty(sDate))
                return string.Empty;

            try
            {
                DateTime dt;
                bool chk = DateTime.TryParse(sDate, out dt);
                if (chk)
                    return dt.ToString("yyyyMMdd");
                else
                    return string.Empty;
            }
            catch (Exception) { }
            sDate = sDate.Replace("/", "");
            sDate = sDate.Replace(".", "");
            sDate = sDate.Replace("-", "");
            return sDate;
        }
        /// <summary>
        /// 날짜 포맷으로 만들기
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="dtFormat"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>
        public string ConvertToDateString(string sDate, GlobalEnum.DateFormat dtFormat, string gubun)
        {

            sDate = this.RemoveDateFormat(sDate);
            try
            {
                switch (dtFormat)
                {
                    case GlobalEnum.DateFormat.yyyy:
                        break;
                    case GlobalEnum.DateFormat.yyyyMM:
                        sDate = sDate.Substring(0, 4) + gubun + sDate.Substring(4, 2);
                        break;
                    case GlobalEnum.DateFormat.yyyyMMdd:
                        sDate = sDate.Substring(0, 4) + gubun + sDate.Substring(4, 2) + gubun + sDate.Substring(6, 2);
                        break;
                    case GlobalEnum.DateFormat.yyyyMMddKor:
                        sDate = sDate.Substring(0, 4) + "년 " + Convert.ToInt32(sDate.Substring(4, 2)).ToString() + "월 " + Convert.ToInt32(sDate.Substring(6, 2)) + "일";
                        break;
                    default:
                        sDate = string.Empty;
                        break;
                }
            }
            catch { return string.Empty; }
            return sDate;
        }
    }
}
