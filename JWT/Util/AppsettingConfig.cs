using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Util
{
    public class AppsettingConfig
    {
        private static IConfiguration Configuration { get; set; }
        static AppsettingConfig()
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource
                {
                    Path = "appsettings.json",
                    ReloadOnChange = true
                }).Build();
        }

        /// <summary>
        /// Config additional appsetting parms come from JWT's properties
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                foreach (var section in sections)
                {
                    val += section + ':';
                }
                return Configuration[val.TrimEnd(':')];
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
