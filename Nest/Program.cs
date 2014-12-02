using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Nest
{
    class Program
    {
        const string AUTH_CODE_URL = @"https://home.nest.com/login/oauth2?client_id={0}&state=STATE";
        const string ACCESS_TOKEN_URL = @"https://api.home.nest.com/oauth2/access_token?client_id={0}&code={1}&client_secret={2}&grant_type=authorization_code";
        const string DEV_NET = @"https://developer-api.nest.com/devices/thermostats?auth={0}";
        const string STR_NET = @"https://developer-api.nest.com/structures/{0}?auth={1}";

        static void Main(string[] args)
        {
            string CLIENT_ID = ConfigurationManager.AppSettings["CLIENT_ID"];
            string CLIENT_SECRET = ConfigurationManager.AppSettings["CLIENT_SECRET"];
            string AUTH_CODE = ConfigurationManager.AppSettings["AUTH_CODE"];
            string ACCESS_TOKEN = ConfigurationManager.AppSettings["ACCESS_TOKEN"];

            var device = GetDevice(ACCESS_TOKEN);
            var structure = GetStructure(device.structure_id, ACCESS_TOKEN);

            Console.WriteLine(string.Format("{0} has a current temperature of {1}°F", device.name, device.ambient_temperature_f));
            Console.WriteLine(string.Format("{0} is currently set to {1}", device.name, structure.away));
            Console.ReadLine();
        }

        static Device GetDevice(string token)
        {
            return GetWebRequest<Device>(string.Format(DEV_NET, token));
        }

        static Structure GetStructure(string structureId, string token)
        {
            return GetWebRequest<Structure>(string.Format(STR_NET, structureId, token));
        }

        static TResult GetWebRequest<TResult>(string url)
        {
            string DEVICE_ID = ConfigurationManager.AppSettings["DEVICE_ID"];
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            JavaScriptSerializer js = new JavaScriptSerializer();
            var tmpNestObject = js.Deserialize<dynamic>(reader.ReadToEnd());
            TResult obj = typeof(TResult) == typeof(Device) ?
                js.Deserialize<TResult>(js.Serialize(tmpNestObject[DEVICE_ID])) :
                js.Deserialize<TResult>(js.Serialize(tmpNestObject));
            reader.Close();
            response.Close();
            return obj;
        }
    }
}
