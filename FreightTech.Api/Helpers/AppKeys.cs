using System.Configuration;

namespace FreightTech.Api
{
    public class AppKeys
    {
        public static string OneSignalAppId { get { return ConfigurationManager.AppSettings["OneSignalAppId"].ToString(); } }
        public static string OneSignalRestAppKey { get { return ConfigurationManager.AppSettings["OneSignalRestAppKey"].ToString(); } }
    }
}
