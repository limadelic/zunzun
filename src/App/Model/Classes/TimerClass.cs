using System;
using System.Windows.Threading;

namespace Zunzun.App.Model.Classes {

    public class TimerClass : Timer {
    
        readonly DispatcherTimer SystemTimer;
        
        public event OnTime Notify;
        
        public TimerClass() {
            SystemTimer = new DispatcherTimer();
            SystemTimer.Tick += Tick;
        }

        void Tick(object Sender, EventArgs E) {
            if (Notify != null) Notify();
        }

        public void NotifyEvery(int Milliseconds) {
            SystemTimer.Interval = new TimeSpan(0, 0, 0, 0, Milliseconds);
            SystemTimer.Start();
        }
    }
}