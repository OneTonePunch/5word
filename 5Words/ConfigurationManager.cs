using _5Words.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            Configuration = ApplicationConfiguration.LoadConfiguration();
        }
        public static ApplicationConfiguration Configuration { get; private set; }
    }
}
