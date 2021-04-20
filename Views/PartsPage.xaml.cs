using Athena_REST.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PartsPage : ContentPage
    {
        private readonly PartsViewModel viewModel;

        public PartsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new PartsViewModel();
        }

        private async void OnItemSelected(object sender, EventArgs args)
        {
            //BindableObject layout = (BindableObject)sender;
            //SvcCall item = (SvcCall)layout.BindingContext;

            // Hold selected Svc
            //Services.MockDataStore.SetSvcSelected(item);
            // Open Page
            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.IsBusy = true;
            }
        }

        public async void ClosePage(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}