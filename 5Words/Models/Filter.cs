using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Models
{
    public class Filter
    {
        public bool EnableContains { get; set; } = false;

        public bool EnableNonContains { get; set; } = false;

        public bool EnableByTemplate { get; set; } = false;

        public bool EnableByAntiTemplate { get; set; } = false;

        public string Contains { get; set; }

        public string NonContains { get; set; }

        public string Template { get; set; }

        public string AntiTemplate { get; set; }


    }
}
