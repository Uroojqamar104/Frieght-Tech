using System.Configuration;

namespace FreightTech.App.Helpers
{
    public static class AppKeys
    {
        public static string LocalAppDomain { get { return ConfigurationManager.AppSettings["LocalAppDomain"].ToString(); } }
        public static string LocalApiDomain { get { return ConfigurationManager.AppSettings["LocalApiDomain"].ToString(); } }

        public static string AppDomain { get { return ConfigurationManager.AppSettings["AppDomain"].ToString(); } }
        public static string ApiDomain { get { return ConfigurationManager.AppSettings["ApiDomain"].ToString(); } }

        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["FreightTechConnection"].ToString(); } }
        
        //public static string SendGridName { get { return ConfigurationManager.AppSettings["SendGridName"].ToString(); } }
        //public static string SendGridEmail { get { return ConfigurationManager.AppSettings["SendGridEmail"].ToString(); } }
        //public static string SendGridPassword { get { return ConfigurationManager.AppSettings["SendGridPassword"].ToString(); } }
        //public static string ProductImagesPath { get { return ConfigurationManager.AppSettings["ProductImagesPath"].ToString(); } }
        ////
        ////public static string ProductImagesPath { get { return Path.Combine(Server.MapPath("~/Content/Images/Products/")); } }
    }
}
