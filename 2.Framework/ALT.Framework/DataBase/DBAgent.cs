using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ALT.Framework.DataBase
{
    /// <summary>
    /// SQL XML 파일로드
    /// </summary>
    public class DBAgent : IDBAgentService
    {
        public string LoadSQL(string sqlFullPath, string templateID, params object[] obValues)
        {
            string sql = string.Empty;

            // null 값일 경우 공백으로 처리
            string[] value = obValues.Select(s=> (s == null ? "" : s.ToString())).ToArray();

           
            string file = sqlFullPath;// Path.Combine(Global.ConfigInfo.SqlXmlPath, string.Format(@"{0}"
               //, FileName));

            DBAgentMultiTemplate dbaMulti = null;
             
            XmlSerializer serializer = new XmlSerializer(typeof(DBAgentMultiTemplate));

            XmlReader xr = XmlReader.Create(file);
            dbaMulti = serializer.Deserialize(xr) as DBAgentMultiTemplate;
            xr.Close();

            // templateID 검색
            var query = from a in dbaMulti.DBAgentTemplate
                        where a.TemplateID == templateID
                        select a;

            if (query != null)
            {
                DBAgentTemplate dt = query.First() as DBAgentTemplate;
                sql = MakeSQL(dt, value);
            }
            if (!sql.ToUpper().Contains("NOCOUNT"))
            {
                sql = " SET NOCOUNT ON " + "\n" + sql + "\n" + " SET NOCOUNT OFF ";
            }
            if (!sql.ToUpper().Contains("ARITHABORT"))
            {
                sql = " SET ARITHABORT ON " + "\n" + sql + "\n" + " SET ARITHABORT OFF ";
            }

            return sql;
        }

        public string LoadCondSQL(string sqlFullPath, string templateID, params object[] obValues)
        {
            string sql = string.Empty;

            // null 값일 경우 공백으로 처리
            string[] value = obValues.Select(s => (s == null ? "" : s.ToString())).ToArray();


            string file = sqlFullPath;// Path.Combine(Global.ConfigInfo.SqlXmlPath, string.Format(@"{0}"
                                      //, FileName));

            DBAgentMultiTemplate dbaMulti = null;

            XmlSerializer serializer = new XmlSerializer(typeof(DBAgentMultiTemplate));

            XmlReader xr = XmlReader.Create(file);
            dbaMulti = serializer.Deserialize(xr) as DBAgentMultiTemplate;
            xr.Close();

            // templateID 검색
            var query = from a in dbaMulti.DBAgentTemplate
                        where a.TemplateID == templateID
                        select a;

            if (query != null)
            {
                DBAgentTemplate dt = query.First() as DBAgentTemplate;
                //for (int i = 0; i < value.Length; i++)
                //{
                //    if (!value[i].Contains("''"))
                //    {
                //        value[i] = value[i].Replace("'", "''");
                //    }
                //}
                sql = MakeSQL(dt, value);
            }
            return sql;
        }

        public string LoadSQLXml(string sqlFullPath, string templateID, params string[] value)
        {
            string sql = string.Empty;

            string file = sqlFullPath; //Path.Combine(Global.ConfigInfo.SqlXmlPath, string.Format(@"{0}"
               //, FileName));

            DBAgentMultiTemplate dbaMulti = null;

            XmlSerializer serializer = new XmlSerializer(typeof(DBAgentMultiTemplate));

            XmlReader xr = XmlReader.Create(file);
            dbaMulti = serializer.Deserialize(xr) as DBAgentMultiTemplate;
            xr.Close();

            // templateID 검색
            var query = from a in dbaMulti.DBAgentTemplate
                        where a.TemplateID == templateID
                        select a;

            if (query != null)
            {
                DBAgentTemplate dt = query.First() as DBAgentTemplate;
                for (int i = 0; i < value.Length; i++)
                {
                    if (!value[i].Contains("@@@@"))
                    {
                        value[i] = value[i].Replace("'", "''");
                    }

                    value[i] = value[i].Replace("@@@@", "");
                }
                sql = MakeSQL(dt, value);
            }

            return sql;
        }
        private ProcessFixedSQL getValueCountinFixedSQL(string FixedSQL, params string[] value)
        {
            ProcessFixedSQL pfs = new ProcessFixedSQL();
            pfs.Count = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (FixedSQL.Contains("{" + i.ToString() + "}") == true)
                {
                    FixedSQL = FixedSQL.Replace("{" + i.ToString() + "}", value[i]);
                    pfs.Count++;
                }
            }
            pfs.FixedSQL = FixedSQL;
            return pfs;
        }
        private class ProcessFixedSQL
        {
            public int Count { get; set; }
            public string FixedSQL { get; set; }
        }

        private string MakeSQL(DBAgentTemplate dbaTemp, params string[] value)
        {
            string result = string.Empty;

            switch (dbaTemp.Information.SqlType)
            {
                default:
                case GlobalEnum.DBAgentSQLType.FixedSQL:   // 고정 SQL
                    if (value != null)
                        result = string.Format(dbaTemp.TemplateSQL.FixedSQL, value);
                    else
                        result = dbaTemp.TemplateSQL.FixedSQL;
                    break;
                case GlobalEnum.DBAgentSQLType.DynamicSQL:
                    StringBuilder sb = new StringBuilder();
                    ProcessFixedSQL pfx = getValueCountinFixedSQL(dbaTemp.TemplateSQL.FixedSQL, value);
                    string strSql = pfx.FixedSQL;
                    List<DynamicBodyData> BodyList = dbaTemp.TemplateSQL.DynamicBodys;
                    for (int i = pfx.Count; i < value.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(value[i]) || (BodyList[i - pfx.Count].Gubun != null && BodyList[i - pfx.Count].Gubun == "1"))
                            strSql = strSql.Replace("#[" + BodyList[i - pfx.Count].Key + "]", String.Format(BodyList[i - pfx.Count].Body, value[i]));
                        else
                            strSql = strSql.Replace("#[" + BodyList[i - pfx.Count].Key + "]", string.Empty);

                    }
                    result = strSql;
                    break;
            }
            return result;
        }
    }
}
