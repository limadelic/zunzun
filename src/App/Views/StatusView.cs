using System.Windows;

namespace Zunzun.App.Views {
    public interface StatusView {
        string UpdateText { get; set; }
        Visibility UpdateVisibility{ get; set; }
    }
}