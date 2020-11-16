using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ALT.Framework.Data
{

    #region >> ExtendFormat 클래스
    public static class ExtendFormat
    {
        /// <summary>
        /// 디폴트 스트링 설정
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="bHtml"></param>
        /// <returns></returns>
        public static string ToString(this object value, string defaultValue = "", bool bHtml = false)
        {
            if (value.isNumeric())
                return (value == null || (value.GetType().Name.ToUpper() != "STRING"
                    && value.GetType().Name.ToUpper().Contains("DATE")
                    )) ? defaultValue : value.ToString();
            else
            {

                if (value == null)
                {
                    return defaultValue;
                }
                else
                {
                    string[] arrData = value.ToString().Split('-');
                    if (arrData.Length == 3 && value.ToString().Length < 20) //전화번호 날짜일때 제외
                    {
                        return value.ToString();
                    }
                    else
                    {
                        if (bHtml)
                            return (value == null) ? defaultValue : Global.SecurityInfo.getGetSafeHtml(value.ToString());
                        else
                            return (value == null || Convert.ToString(value) == "") ? defaultValue : Global.SecurityInfo.getSqlInjectIon(value.ToString());
                    }
                }
            }
        }
        public static long ToLong (this string value)
        {
            long rtn =0;
            long.TryParse(value, out rtn );
            
            return rtn;
        }
        public static long ToLong(this int value)
        {
          return (long)value;
        }
        public static long ToLong(this double value)
        {
             return (long)value;
        }
        /// <summary>
        /// 배열을 구분자가 있는 문자열로 변환
        /// </summary>
        /// <param name="arrVal"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>
        public static string FromArrayToString(this List<int> arrVal, string gubun = ",")
        {
            string rtn = string.Empty;
            if (arrVal == null) return rtn;
            foreach (object param in arrVal)
            {
                if (!string.IsNullOrEmpty(rtn)) rtn += ",";
                rtn += param.ToString();
            }
            return rtn;
        }
        /// <summary>
        /// Bool형을 스트링으로 변환
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToBooleanString(this bool? val, string defaultValue = "")
        {
            return (val == null) ? defaultValue : (val == true ? "1" : "0");
        }
        /// <summary>
        /// Bool형을 스트링으로 변환
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToBooleanString(this bool val)
        {
            return (val == true ? "1" : "0");
        }
        public static string ToInjectionString(this string val)
        {
            val = val.Replace("'", "''");

            return val;
        }

        public static decimal Step(this int val)
        {
            return Convert.ToDecimal(Convert.ToDouble(1) / System.Math.Pow(10, (double)val));
        }
        /// <summary>
        /// 정수형으로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ConvertDecimal(this object value)
        {
            if (value.isNumeric())
            {
                return Convert.ToDecimal(value);
            }
            else
                return 0;
        }

        public static int ConvertInt(this object value)
        {
            if (value.isNumeric())
            {
                return Convert.ToInt32(value);
            }
            else
                return 0;
        }
        public static decimal ToRound(this object value, int degit = 2)
        {
            if (value.isNumeric())
            {
                return Math.Round(Convert.ToDecimal(value) * (decimal)Math.Pow(10, degit)) / (decimal)Math.Pow(10, degit);
            }
            else
                return 0;
        }


        /// <summary>
        /// 특수문자 처리
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLettreString(this string value)
        {
            return value.Replace(value, "\\" + value);
        }
        #region >> 날짜 관련
        public static string RemoveDateString(this string val)
        {
            try
            {
                if (string.IsNullOrEmpty(val))
                    return string.Empty;
                else
                {
                    if (val.Length == 8)
                        return val;
                    else if (val.isDate())
                        return Convert.ToDateTime(val).ToString("yyyyMMdd");
                    else
                    {
                        return new Format().RemoveDateFormat(val);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 날짜형인지 체크
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isDate(this object value)
        {
            if (value == null)
                return false;
            DateTime date;

            if (value.GetType().Name.ToUpper() == "STRING" && value.ToString().Length == 8)
            {
                value = value.ToString().Substring(0, 4) + "-" + value.ToString().Substring(4, 2) + "-" + value.ToString().Substring(6, 2);
            }
            return DateTime.TryParse(value.ToString(), out date);
        }
        public static DateTime? ToDate(this string value, string CultureName = "ko-KR")
        {
            value = value.ToFormatDate();
            if (string.IsNullOrEmpty(value)) return null;
            return Convert.ToDateTime(value, new System.Globalization.CultureInfo(CultureName));
        }

        public static string ToFormatDate(this string value, string format = "yyyy.MM.dd")
        {
            if (value == null || value.Count() < 8) return string.Empty;
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Count() != 8) return string.Empty;

            DateTime dDate = Convert.ToDateTime(value.Substring(0, 4) + "-" + value.Substring(4, 2) + "-" + value.Substring(6, 2));
            return dDate.ToString(format);
        }
        /// <summary>
        /// Datetime Picker에 표시될 시각
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nFixMin"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDefaultMinute(this DateTime dt, int nFixMin = 30, string format = "yyyy.MM.dd HH:mm")
        {
            int nTmp = dt.Minute % nFixMin;
            if ((nFixMin / 2.00) < nTmp) nTmp = nTmp - nFixMin;

            dt = dt.AddMinutes(nFixMin - nTmp);

            return dt.ToString("yyyy.MM.dd HH:mm");
        }

        public static string ToDefaultDateString(this DateTime? dt, string format = "yyyy.MM.dd HH:mm", string defautlVal = "")
        {
            if (dt == null) return defautlVal;
            else
            {
                return dt.Value.ToString(format);
            }
        }
        #endregion



        /// <summary>
        /// 숫자형인지 체크
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isNumeric(this object value)
        {
            if (value == null)
                return false;
            double Num;
            return double.TryParse(value.ToString(), out Num);
        }

        /// <summary>
        /// 헥사 코드에서 유니코드로 변경
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string UnicodeToChar(this string hex)
        {
            int code = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            string unicodeString = char.ConvertFromUtf32(code);
            return unicodeString;
        }

        public static string Serialize<T>(this T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(this string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        public static string ConvertHttpsUrl(this string url)
        {
            
            if (string.IsNullOrEmpty(url)) return "";
            else if (url.IndexOf("localhost") >= 0){ return url; }
            url.Replace("http://106.246.255.132:8004", Global.ConfigInfo.MANAGEMENT_SITE);
            url.Replace("http://106.246.255.132:8002", Global.ConfigInfo.MANAGEMENT_SITE);
            return ConvertHttpsUrl(url, null);
        }
        /// <summary>
        /// url 데이터를 https 형태로 변환
        /// </summary>
        /// <param name="url">확장변수</param>
        /// <param name="keyValuePairs">사용자정의 Replace 데이터</param>
        /// <returns></returns>
        public static string ConvertHttpsUrl(this string url, Dictionary<string, string> pairs)
        {
            if (string.IsNullOrEmpty(url)) return "";
            else if (url.IndexOf("localhost") >= 0) { return url; }
            url = url.Replace("http://", "https://");
            url = url.Replace("www.", "");

            if (pairs != null)
            {
                foreach (var data in pairs)
                {
                    url = url.Replace(data.Key, data.Value);
                }
            }
            return url;
        }

        public static T Clone<T>(this T obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            T newObject = (T)dcSer.ReadObject(memoryStream);
            return newObject;
        }

        /*스트림을 바이트로변환*/
        public static byte[] ConvertToByte(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
    #endregion
}
