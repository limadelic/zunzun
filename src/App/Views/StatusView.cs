namespace Zunzun.App.Views {

    public interface StatusView {

        string UpdateText { get; set; }
        bool IsVisible { get; set; }
        void FocusOnUpdate();
    }
}