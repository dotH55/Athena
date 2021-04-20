//FeedbackViewModel
namespace Athena_REST.ViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        public FeedbackViewModel()
        {
            Title = "Feedback";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://duplicatingsystems.com/"));
        }

        //public ICommand OpenWebCommand { get; }
    }
}
