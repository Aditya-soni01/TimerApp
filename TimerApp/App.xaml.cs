using TimerApp.Views;

namespace TimerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TimerView();
        }
    }
}
