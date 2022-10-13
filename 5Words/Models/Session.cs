using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Models
{
    public class Session
    {
        public Session(long chatId, string userName, SessionParams prms)
        {
            ChatId = chatId;
            UserName = userName;
            Params = prms;
            LastUpdate = DateTime.Now;
        }
        public string UserName { get; set; }
        public long ChatId { get; set; }

        public DateTime LastUpdate { get; set; }
        public SessionParams Params { get; set; }
    }

    public class SessionParams
    {
        public int Length { get; set; } = -1;

        public Filter Filter { get; set; }
    }
}
