using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkHW2_SwagLabs.Tools
{
    class Utils
    {
        public static readonly IConfigurationRoot appConfig = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(path: "Configurations\\AppSettings.json")
           .Build();

        public static readonly IConfigurationRoot pageElements = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile(path: "Configurations\\PageElementsAndText.json")
           .Build();
    }
}
