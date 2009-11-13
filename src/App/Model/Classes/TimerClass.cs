namespace Zunzun.App.Model.Classes {

    public class TimerClass : Timer {

        int Period;
        System.Threading.Timer SystemTimer;
        
        public event OnTime Notify;
        
        public void NotifyEvery(int Milliseconds) {
            Period = Milliseconds;
            Start();
        }

        void Start() {
            SystemTimer = new System.Threading.Timer(Callback, null, 0, Period);
        }

        void Callback(object State) {
            if (Notify != null) Notify();
        }
    }
}