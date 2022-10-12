
using _5Words.Models;
using System.Collections.Concurrent;

namespace _5Words
{
    public static class SessionStorage
    {
        static SessionStorage()
        {
            Storage = new ConcurrentDictionary<long, Session>();
        }
        public static ConcurrentDictionary<long, Session> Storage { get; private set; }

        public static void AddOrUpdate(long chatId, Session session = null)
        {
            if (Storage.TryGetValue(chatId, out Session storageSession))
            {
                storageSession = session;
            }
            else
            {
                Storage.TryAdd(chatId, session);
            }
        }
    }
}
