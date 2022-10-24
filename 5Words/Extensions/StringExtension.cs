using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Extensions
{
    public static class StringExtension
    {
        public static bool StartWithAny(this string message, List<string> commands)
        {
            foreach (var command in commands)
            {
                if (message.StartsWith(command))
                    return true;
            }

            return false;
        }

        public static string ReplaceAll(this string message, List<string> commands)
        {
            foreach (var command in commands)
            {
                message = message.Replace(command, "");
            }
            return message;
        }
    }
}
