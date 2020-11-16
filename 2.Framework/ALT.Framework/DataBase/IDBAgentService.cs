using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;

namespace ALT.Framework.DataBase
{
    public interface IDBAgentService
    {
        string LoadSQL(string FileName, string templateID, params object[] AppendParam);
    }

    [XmlRootAttribute("DBAgentMultiTemplate", Namespace = "http://altsoft.kr/", IsNullable = false)]
    public class DBAgentMultiTemplate
    {
        [XmlElement]
        public DBAgentTemplate[] DBAgentTemplate;

    }

    public class DBAgentTemplates
    {
        [XmlAttribute]
        public string TemplateID;

        [XmlElement]
        public SQLInformation Information;

        [XmlElement]
        public TemplateSQL TemplateSQL;
    }

    [XmlRootAttribute("DBAgentTemplate", Namespace = "http://altsoft.kr/", IsNullable = false)]
    public class DBAgentTemplate
    {
        [XmlAttribute]
        public string TemplateID;

        [XmlElement]
        public SQLInformation Information;

        [XmlElement]
        public TemplateSQL TemplateSQL;
    }

    public class TemplateSQL
    {
        [XmlElement]
        public string FixedSQL;
        [XmlElement("DynamicBodys")]
        public List<DynamicBodyData> DynamicBodys;
    }

    public class DynamicBodyData
    {
        public string Gubun; // 1:필수 0:미필(없으면 조건에서 제외)
        public string Key;
        public string Body;
    }


    public class SQLInformation
    {

        [XmlElement]
        public GlobalEnum.DBAgentSQLType SqlType;
        [XmlElement]
        public string Description;
        [XmlElement]
        public string SQLModifier;
        [XmlElement]
        public string LastModifyDate;
    }
}
