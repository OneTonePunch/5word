using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Models
{
    public class Filter
    {
        private string _contains;

        private string _nonContains;

        private string _template;
        private string _antitemplate;
        public bool EnableContains { get; private set; } = false;

        public bool EnableNonContains { get; private set; } = false;

        public bool EnableByTemplate { get; private set; } = false;

        public bool EnableByAntiTemplate { get; private set; } = false;

        public string Contains
        {
            get
            {
                return _contains;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    EnableContains = true;
                else
                    EnableContains = false;

                _contains = value;
            }
        }

        public string NonContains
        {
            get
            {
                return _nonContains;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    EnableNonContains = true;
                else
                    EnableNonContains = false;

                _nonContains = value;
            }
        }

        public string Template
        {
            get
            {
                return _template;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    EnableByTemplate = true;
                else
                    EnableByTemplate = false;

                _template = value;
            }
        }

        public string AntiTemplate
        {
            get
            {
                return _antitemplate;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    EnableByAntiTemplate = true;
                else
                    EnableByAntiTemplate = false;

                _antitemplate = value;
            }
        }


    }
}
