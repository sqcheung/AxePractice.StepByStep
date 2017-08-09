using System.Collections.Generic;

namespace SimpleIntegration
{
    public class FakeMyLogger: IMyLogger
    {
        readonly Stack<string> Logs = new Stack<string>(); 
        public void Log(string info)
        {
            Logs.Push(info);
        }
    }
}