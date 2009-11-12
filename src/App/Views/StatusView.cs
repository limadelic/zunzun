namespace Zunzun.App.Views {

    public interface StatusView {

        string UpdateText { get; set; }
        bool IsUpdateVisible { get; set; }
        void FocusOnUpdate();
    }
}