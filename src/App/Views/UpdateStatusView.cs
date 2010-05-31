namespace Zunzun.App.Views {

    public interface UpdateStatusView {

        string UpdateText { get; set; }
        bool IsVisible { get; set; }
        int CursorPos { get; }
        string RequestedPhoto { get; }
        void FocusOnUpdate();
    }
}