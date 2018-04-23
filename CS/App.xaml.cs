using System.Windows;

namespace Example {
    public partial class App : Application {
        public App() {
            Startup += OnStartup;
        }
        void OnStartup(object sender, StartupEventArgs e) {
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
