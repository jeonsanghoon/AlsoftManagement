using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Optimization;

namespace ALT.Framework.Mvc.Helpers
{
    public class FileHashVersionBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            foreach (var file in response.Files)
            {
                using (FileStream fs = File.OpenRead(HostingEnvironment.MapPath(file.IncludedVirtualPath)))
                {
                    //get hash of file contents
                    byte[] fileHash = new SHA256Managed().ComputeHash(fs);

                    //encode file hash as a query string param
                    string version = HttpServerUtility.UrlTokenEncode(fileHash);
                    file.IncludedVirtualPath = string.Concat(file.IncludedVirtualPath, "?v=", version);
                }
            }
        }
    }
}
