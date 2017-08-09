using System.Collections.Generic;
using System.Linq;

namespace SimpleIntegration
{
    public class FakeMyLogger: IMyLogger
    {
        readonly Stack<string> Logs = new Stack<string>(); 
        public void Log(string info)
        {
            Logs.Push(info);
        }

        public List<string> GetLogs()
        {
            return Logs.ToList();
        }
    }
}