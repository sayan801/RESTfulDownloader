using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Net;
using System.IO;

namespace RESTfulDownloaderApp
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class restfuldownloader
    {

        [WebGet(UriTemplate = "{url}")]
        public bool Get(string url)
        {
            try
            {
                byte[] encodedDataAsBytes  = System.Convert.FromBase64String(url);

                string decodedUrl = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
                //string decodedUrl = System.Text.Encoding.Unicode.GetString(Convert.FromBase64String(url)); //System.Web.HttpUtility.UrlDecode(url);
                string fileName = Path.Combine(Path.GetTempPath(), DateTime.Now.ToOADate().ToString());
                // Create an instance of WebClient
                WebClient client = new WebClient();

                // Start the download and copy the file to c:\temp
                client.DownloadFileAsync(new Uri(decodedUrl), fileName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
