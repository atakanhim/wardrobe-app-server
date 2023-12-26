using Microsoft.Extensions.Configuration;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {

                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/wardrobe.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("MsSQL");
            }
        }
    }
}
