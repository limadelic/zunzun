namespace Zunzun.App.Model {

    public delegate void OnTime();

    public interface Timer {

        void NotifyEvery(int Milliseconds);
        event OnTime Notify;
    }
}