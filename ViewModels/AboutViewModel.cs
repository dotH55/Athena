using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Athena_REST.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://duplicatingsystems.com/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}