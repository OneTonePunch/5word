using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Models
{
    public class CommandsConfiguration
    {
        public List<string> Start { get; set; }

        public List<string> Help { get; set; }

        public List<string> Random { get; set; }

        public List<string> Find { get; set; }

        public List<string> Length { get; set; }

        public List<string> Contains { get; set; }

        public List<string> NonContains { get; set; }

        public List<string> Template { get; set; }

        public List<string> AntiTemplate { get; set; }

        public List<string> StateInfo { get; set; }

        

        public List<string> GetValueByType(CommandType type)
        {
            switch (type)
            {
                case CommandType.Start:
                    return Start;
                case CommandType.Help:
                    return Help;
                case CommandType.Random:
                    return Random;
                case CommandType.Find:
                    return Find;
                case CommandType.Length:
                    return Length;
                case CommandType.Contains:
                    return Contains;
                case CommandType.NonContains:
                    return NonContains;
                case CommandType.Template:
                    return Template;
                case CommandType.AntiTemplate:
                    return AntiTemplate;
                case CommandType.StateInfo:
                    return StateInfo;
                default:
                    return Help;
            }
        }
    }

    public enum CommandType{
        Start, 
        Help, 
        Random,
        Find,
        Length, 
        Contains, 
        NonContains, 
        Template,
        AntiTemplate,
        StateInfo
    }
}
