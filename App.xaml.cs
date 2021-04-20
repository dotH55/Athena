using Athena_REST.Services;
using Athena_REST.Views;
using Xamarin.Forms;

namespace Athena_REST
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new MainPage();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            //MainPage = new LoginPage();
        }
    }
}
