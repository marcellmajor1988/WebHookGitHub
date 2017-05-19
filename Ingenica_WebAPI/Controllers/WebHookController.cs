using Ingenica_WebAPI.DynamicsNAVWS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;

namespace Ingenica_WebAPI.Controllers
{
    public class WebHookController : ApiController
    {
        public HttpResponseMessage Post([FromBody]JArray input)
        {            
            System.Xml.XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Row\":" + input + "}", "XmlDocument");
            WebHookTest DynamicsNAVWS = new WebHookTest();
            DynamicsNAVWS.UseDefaultCredentials = false;
            DynamicsNAVWS.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["WebServiceUser"], WebConfigurationManager.AppSettings["WebServiceAccessKey"]);
            DynamicsNAVWS.Url = WebConfigurationManager.AppSettings["WebServiceUrl"];
            string outputstring = "";
            DynamicsNAVWS.ProcessRequest(doc.OuterXml, ref outputstring);            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, outputstring);
            return response;
        }
    }
}
