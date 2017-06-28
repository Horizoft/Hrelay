using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Horizoft.Relay.Web.Providers
{
    public class ConfigProvider
    {
        public static string CurrentVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
            }
        }

        public static string ServiceUrl
        {
            get
            {
                string webApiUri = ConfigurationManager.AppSettings["WebApiUri"];

                return webApiUri;
            }
        }

        public static string MenuUrl
        {
            get
            {
                string menuUrl = ConfigurationManager.AppSettings["MenuUrl"];

                return menuUrl;
            }
        }

    }

}