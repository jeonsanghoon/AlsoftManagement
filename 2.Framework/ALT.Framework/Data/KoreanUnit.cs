using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.Framework.Data
{
    public class Koreanunit
    {
        /// <summary>
        /// HangulJaso에서 한글자소에 대한 정보를 담고 있다.
        /// </summary>
        public struct HANGUL_INFO
        {
            /// <summary>
            ///  한글여부(H, NH)
            /// </summary>
            public bool isHangul;
            /// <summary>
            /// 분석 한글
            /// </summary>
            public string originalVal;
            /// <summary>
            /// 분리 된 한글(강 -> ㄱ,ㅏ,ㅇ) 
            /// </summary>
            public string chars;
            public string chosung { get; set; }
            public string jungsung { get; set; }
            public string jongsung { get; set; }
            public string splitstring { get; set; }

            public List<string> arrChosung { get { return chosung.TrimEnd().Split(' ').ToList(); } set { } }
            public List<string> arrJungung { get { return jungsung.TrimEnd().Split(' ').ToList(); } set { } }
            public List<string> arrJongung { get { return jongsung.TrimEnd().Split(' ').ToList(); } set { } }

            public List<string> arrWansungs { get { 
                        List<string> list = new List<string>();

                    for (int i=0;  i< chosung.Count(); i++)
                    {
                        if (!(jungsung[i] == ' ' || jongsung[i] == ' '))
                        {
                            list.Add(originalVal[i].ToString());
                        }
                    }
                        
                        return list;} set { } }

        }

        /// <summary>
        /// 한글 분석 클래스
        /// </summary>
        public sealed class HangulJaso
        {
            /// <summary>
            /// 초성 리스트
            /// </summary>
            public static readonly string HTable_ChoSung = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
            /// <summary>
            /// 중성 리스트
            /// </summary>
            public static readonly string HTable_JungSung = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
            /// <summary>
            /// 종성 리스트
            /// </summary>
            public static readonly string HTable_JongSung = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
            private static readonly ushort m_UniCodeHangulBase = 0xAC00;
            private static readonly ushort m_UniCodeHangulLast = 0xD79F;

            /// <summary>
            /// 생성자
            /// </summary>
            public HangulJaso() { }

            /// <summary>
            /// 초성, 충성, 종성으로 이루어진 한글을 한글자의 한글로 만든다.
            /// </summary>
            /// <param name="choSung">초성</param>
            /// <param name="jungSung">중성</param>
            /// <param name="jongSung">종성</param>
            /// <returns>합쳐진 글자</returns>
            /// <remarks>
            /// <para>
            /// 초성, 충성, 종성으로 이루어진 한글을 한글자의 한글로 만든다.
            /// </para>
            /// <example>
            /// <code>
            /// string choSung = "ㄱ", jungSung = "ㅏ", jongSung = "ㅇ";
            /// char hangul = MergeJaso(choSung, jungSung, jongSung);
            /// // 결과 -> 강
            /// </code>
            /// </example>
            /// </remarks>
            public static char MergeJaso(string choSung, string jungSung, string jongSung)
            {
                int ChoSungPos, JungSungPos, JongSungPos;
                int nUniCode;

                ChoSungPos = HTable_ChoSung.IndexOf(choSung);    // 초성 위치
                JungSungPos = HTable_JungSung.IndexOf(jungSung);   // 중성 위치
                JongSungPos = HTable_JongSung.IndexOf(jongSung);   // 종성 위치

                // 앞서 만들어 낸 계산식
                nUniCode = m_UniCodeHangulBase + (ChoSungPos * 21 + JungSungPos) * 28 + JongSungPos;

                // 코드값을 문자로 변환
                char temp = Convert.ToChar(nUniCode);

                return temp;
            }

            /// <summary>
            /// 한글자의 한글을 초성, 중성, 종성으로 나눈다.
            /// </summary>
            /// <param name="hanChar">한글</param>
            /// <returns>분리된 한글에 대한 정보</returns>
            /// <seealso cref="HANGUL_INFO"/>
            /// <remarks>
            /// <para>
            /// 한글자의 한글을 초성, 중성, 종성으로 나눈다.
            /// </para>
            /// <example>
            /// <code>
            /// HANGUL_INFO hinfo = DevideJaso('강');
            /// // hinfo.isHangul -> "H" (한글)
            /// // hinfo.originalChar -> 강
            /// // hinfo.chars[0] -> ㄱ, hinfo.chars[1] -> ㄴ, hinfo.chars[2] = ㅇ
            /// </code>
            /// </example>
            /// </remarks>
            public static HANGUL_INFO DevideJaso(string val)
            {
                int ChoSung, JungSung, JongSung;    // 초성,중성,종성의 인덱스
                ushort temp = 0x0000;                // 임시로 코드값을 담을 변수
                HANGUL_INFO hi = new HANGUL_INFO();
                hi.originalVal = val;
                hi.isHangul = false;
                hi.splitstring = "";
                hi.chosung = "";
                hi.jungsung = "";
                hi.jongsung = "";
                if (!string.IsNullOrEmpty(val))
                {
                    foreach (char hanChar in val)
                    {
                        //Char을 16비트 부호없는 정수형 형태로 변환 - Unicode
                        temp = Convert.ToUInt16(hanChar);

                        // 캐릭터가 한글이 아닐 경우 처리
                        if ((temp < m_UniCodeHangulBase) || (temp > m_UniCodeHangulLast))
                        {

                            hi.splitstring += hanChar;

                            if (!HTable_JungSung.Contains(hanChar))
                            {
                                hi.chosung += hanChar;
                                hi.jungsung += " ";
                            }
                            else
                            {
                                hi.chosung += " ";
                                hi.jungsung += hanChar;
                            }
                            hi.jongsung += " ";
                        }
                        else
                        {
                            hi.isHangul = true;
                            // nUniCode에 한글코드에 대한 유니코드 위치를 담고 이를 이용해 인덱스 계산.
                            int nUniCode = temp - m_UniCodeHangulBase;
                            ChoSung = nUniCode / (21 * 28);
                            nUniCode = nUniCode % (21 * 28);
                            JungSung = nUniCode / 28;
                            nUniCode = nUniCode % 28;
                            JongSung = nUniCode;


                            var tmpChar = new char[] { HTable_ChoSung[ChoSung], HTable_JungSung[JungSung], HTable_JongSung[JongSung] };
                            string tmpData = "";


                            tmpData = HTable_ChoSung[ChoSung].ToString();
                            hi.chosung += string.IsNullOrEmpty(tmpData) ? " " : tmpData;
                            tmpData = HTable_JungSung[JungSung].ToString();
                            hi.jungsung += string.IsNullOrEmpty(tmpData) ? " " : tmpData;
                            tmpData = HTable_JongSung[JongSung].ToString();
                            hi.jongsung += string.IsNullOrEmpty(tmpData) ? " " : tmpData;
                            foreach (var dchar in tmpChar)
                            {
                                hi.splitstring += dchar;
                            }
                        }
                    }
                }
                return hi;
            }
        }
    }
}
