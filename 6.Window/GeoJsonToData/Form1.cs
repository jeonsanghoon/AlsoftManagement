using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ALT.Framework;
using System.Data.Linq;
using ALT.VO.Common;
using ALT.BizService;

namespace GeoJsonToData
{
    public partial class Form1 : Form
    {
        public DataContext db;
        public Form1()
        {
            InitializeComponent();
            if (db == null) db = new DataContext(Global.ConfigInfo.DefaultDBSource);
          
        }

        public async Task<GEO_INFO> GeoToDB(int geoType)
        {
            string geojson = string.Empty;
            if(geoType == 1) geojson = Global.FileInformation.FromFileToString(@"D:\Source\\20180205\sido.json");
            if (geoType == 2) geojson = Global.FileInformation.FromFileToString(@"D:\Source\\20180205\sigungoo.json");
            if(geoType == 3) geojson =  Global.FileInformation.FromFileToString(@"D:\Source\\20180205\emd.json");

            GEO_INFO geo = new GEO_INFO { GEO_TYPE = geoType, list = new List<T_GEO>() { } };
            var geoObj = JsonConvert.DeserializeObject<dynamic>(geojson).features;
            StringBuilder sbSql = new StringBuilder();
            foreach (var data in geoObj)
            {
                sbSql.Length = 0;

                var coordinates = (data.geometry.ToString() == "") ? "" : data.geometry.coordinates.ToString().Replace(" ","").Replace("\r", "").Replace("\n", "");
                var code = string.Empty;
                
                var name = string.Empty;
                if (geoType == 1)
                {
                    code = data.properties.CTPRVN_CD.ToString();
                    name = data.properties.CTP_KOR_NM.ToString();
                }
                else if (geoType == 2)
                {
                    code = data.properties.SIG_CD.ToString();
                    name = data.properties.SIG_KOR_NM.ToString();
                }
                else if (geoType == 3)
                {
                    code = data.properties.EMD_CD.ToString();
                    name = data.properties.EMD_KOR_NM.ToString();
                }

                T_GEO tmpdata = new T_GEO() { GEO_TYPE = geo.GEO_TYPE,  CODE = code, NAME = name, COORDINATES = coordinates };


                geo.list.Add(tmpdata);
            }
            var list = geo.list;
            int nPage = 1, nPageSize = 100;
            while (true)
            {

                int frRow = (nPage - 1) * nPageSize ;
                
                if (frRow >= list.Count()) break;
                
                await new GeoService().GeoSave(list.Skip(frRow).Take(nPageSize).ToList(),  (nPage == 1 ? "D": ""));
                nPage++;
            }
            return geo;
        }


        public class GEO_INFO
        {
            public int GEO_TYPE { get; set; }
            public List<T_GEO> list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
              Task.Run(async () =>
            {
                await GeoToDB(1);
            }).Wait();
            //MessageBox.Show("1 완료");
            Task.Run(async () =>
            {
                await GeoToDB(2);
            }).Wait();
            //MessageBox.Show("2 완료");
            Task.Run(async () =>
            {
                await GeoToDB(3);
            }).Wait();
            //MessageBox.Show("3 완료");

        }
    }
}
