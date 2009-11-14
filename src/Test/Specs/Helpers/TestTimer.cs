using System.Timers;
using Zunzun.App.Model;
using Timer=Zunzun.App.Model.Timer;

namespace Zunzun.Specs.Helpers {

    public class TestTimer : Timer {
    
        readonly System.Timers.Timer SystemTimer;
        
        public event OnTime Notify;
        
        public TestTimer() {
            SystemTimer = new System.Timers.Timer();
            SystemTimer.Elapsed += Elapsed;
        }

        void Elapsed(object Sender, ElapsedEventArgs E) { 
            if (Notify != null) Notify();
        }

        public void NotifyEvery(int Milliseconds) {
            SystemTimer.Interval = Milliseconds;
            SystemTimer.Start();
        }
    }
}