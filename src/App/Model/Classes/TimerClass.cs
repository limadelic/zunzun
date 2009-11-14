using System.Timers;

namespace Zunzun.App.Model.Classes {

    public class TimerClass : Timer {
    
        readonly System.Timers.Timer SystemTimer;
        
        public event OnTime Notify;
        
        public TimerClass() {
            SystemTimer = new System.Timers.Timer();
            SystemTimer.Elapsed += Elapsed;
        }

        void Elapsed(object Sender, ElapsedEventArgs E) { 
            if (Notify != null) Notify();
        }

        public void NotifyEvery(int Milliseconds) {
            SystemTimer.Interval = Milliseconds;
            SystemTimer.Enabled = true;
        }
    }
}