namespace Zunzun.App.Views {

    public interface UpdateStatusView {

        string UpdateText { get; set; }
        bool IsVisible { get; set; }
        void FocusOnUpdate();
    }
}