using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words.Models
{
    public class Session
    {
        public Session(long chatId, string userName, SessionState state, SessionParams prms)
        {
            ChatId = chatId;
            UserName = userName;
            State = state;
            Params = prms;
        }
        public string UserName { get; set; }
        public long ChatId { get; set; }
        public SessionState State { get; set; }
        public SessionParams Params { get; set; }
    }

    public enum SessionState
    {
        StartRandom,
        GetLengthRandom,
        EndRandom,

        StartFind,
        GetLengthFind,
        GetContainsFind,
        GetNonContainsFind,
        GetTemplateFind,
        GetAntiTemplateFind,
        EndFind
    }

    public class SessionParams
    {
        public int Length { get; set; }

        public Filter Filter { get; set; }
    }
}
