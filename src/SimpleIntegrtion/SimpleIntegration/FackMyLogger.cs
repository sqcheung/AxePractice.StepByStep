using System.Collections.Generic;
using System.Linq;

namespace SimpleIntegration
{
    public class FakeMyLogger: IMyLogger
    {
        readonly List<string> Logs = new List<string>(); 
        public void Log(string info)
        {
            Logs.Add(info);
        }

        public List<string> GetLogs()
        {
            return Logs.ToList();
        }
    }
}