using _5Words.Models;

namespace _5Words.Managers
{
    internal static class SchedullerManager
    {
        private static Timer _schedullerTimer;

        public static void Start()
        {
            var runParams = new Dictionary<string, object>();
            var scheduleThread = new Thread(Run);
            scheduleThread.Start(runParams);
        }

        private static void Run(object runParams)
        {
            var callBack = new TimerCallback(ClearState);
            _schedullerTimer = new Timer(callBack, runParams, 10000, 1800000);
        }

        public static void ClearState(object state)
        {
            var clearList = SessionStorage.Storage.Where(x => x.Value.LastUpdate.AddMinutes(30) <= DateTime.Now);
            if (clearList != null && clearList.Count() > 0)
            {
                foreach (var clearItem in clearList)
                {
                    SessionStorage.Storage.TryRemove(clearItem);
                }
            }
        }

    }
}