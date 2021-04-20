using Athena_REST.Models;
using Athena_REST.Services;
using Athena_REST.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        private async void OnItemSelected(object sender, EventArgs args)
        {
            BindableObject layout = (BindableObject)sender;
            SvcCall item = (SvcCall)layout.BindingContext;

            // Hold selected Svc
            Services.MockDataStore.SetSvcSelected(item);
            // Open Page
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        }

        /// <summary>
        /// Calls Query_Svc
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Refresh(object sender, EventArgs e)
        {
            viewModel.IsBusy = false;
            // Refresh
            await Services.MockDataStore.Query_Svc_REST();
            // Refresh Listview
            viewModel.IsBusy = true;
        }

        /// <summary>
        /// Logs User out
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Logout(object sender, EventArgs e)
        {
            // Logout
            Logout_Helper();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.IsBusy = true;
            }

            viewModel.IsBusy = false;
            // Calculate Distance
            Services.MockDataStore.GetSvc_Distances().ConfigureAwait(true);
            // Refresh Listview
            viewModel.IsBusy = true;
        }

        /// <summary>
        /// Lookup Parts
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Lookup_Parts(object sender, EventArgs e)
        {
            Lookup_Parts_Helper();
        }

        /// <summary>
        /// Lookup Parts Helper
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Lookup_Parts_Helper()
        {
            try
            {
                string temp = await DisplayPromptAsync("Athena", "Item Number: ");
                if (!temp.ToString().Equals(""))
                {
                    await MockDataStore.Query_Parts_REST(temp.ToUpper());
                    await Navigation.PushModalAsync(new NavigationPage(new PartsPage()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*****************************************************************************");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("*****************************************************************************");
            }
        }

        /// <summary>
        /// Lookup Svc History
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Lookup_SvcHistory_Helper()
        {
            try
            {
                string temp = await DisplayPromptAsync("Athena", "Call ID: ");
                if (!temp.ToString().Equals(""))
                {
                    MockDataStore.SetSvcCallID(temp); //SvcSELECTED.SvcCallID
                    await Services.MockDataStore.Retrieve_Svc_History_REST().ConfigureAwait(true);
                    await Navigation.PushModalAsync(new NavigationPage(new EmployeeDetailJob()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*****************************************************************************");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("*****************************************************************************");
            }
        }

        /// <summary>
        /// Logout Helper
        /// </summary>
        private async void Logout_Helper()
        {
            Application.Current.MainPage = new LoginPage();
        }

        /// <summary>
        /// This method displays Svc History
        /// </summary>
        /// <param name="sender"></param>
        /// /// <param name="args"></param>
        public async void View_Svc_History(object sender, EventArgs args)
        {
            Lookup_SvcHistory_Helper();
        } // View_Svc_History

        /// <summary>
        /// This method displays Svc History
        /// </summary>
        /// <param name="sender"></param>
        /// /// <param name="args"></param>
        public async void Create_Svc(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            //Lookup_SvcHistory_Helper();
        } // Create_Svc

        /// <summary>
        /// This method prompts User for action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OpenMenu(object sender, EventArgs args)
        {
            viewModel.IsBusy = false;
            string action = await DisplayActionSheet("Sort", "Cancel", null, "Lookup Parts", "Lookup Svc History", "Logout");
            await Helper(action);
            // Refresh Listview
            viewModel.IsBusy = true;
        }

        /// <summary>
        /// Menu Helper
        /// </summary>
        /// <param name="action">Menu Action</param>
        private async Task Helper(string action)
        {
            switch (action)
            {
                case "Sort By User":
                    // Bring up User's Svc in progress
                    MockDataStore.Sort_Svc_By_Logged_User();
                    break;
                case "Sort By Time":
                    // Sort using Datetime in descendant order
                    MockDataStore.Sort_Svc_By_DateTimeIn();
                    break;
                case "Sort By Default":
                    // Sort using Datetime in descendant order
                    MockDataStore.Sort_Svc();
                    break;
                case "Sort By City":
                    // Sort using Datetime in descendant order
                    // Get New Problem
                    MockDataStore.Sort_City(await DisplayPromptAsync("Athena", "City: "));
                    break;
                case "Lookup Parts":
                    // View Service History
                    Lookup_Parts_Helper();
                    break;
                case "Lookup Svc History":
                    // Launch Google Map
                    Lookup_SvcHistory_Helper();
                    break;
                case "Logout":
                    Logout_Helper();
                    break;
            }
        }

        /// <summary>
        /// This method filters Svcs
        /// </summary>
        /// <param name="sender"></param>
        /// /// <param name="args"></param>
        public async void Filter_Svc(object sender, EventArgs args)
        {
            try
            {
                viewModel.IsBusy = false;
                string temp = await DisplayActionSheet("Filter", "Cancel", null, "Sort By Default", "Sort By User", "Sort By Time", "Sort By City");
                if (!temp.ToString().Equals(""))
                {
                    await Helper(temp);
                }
                viewModel.IsBusy = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("*****************************************************************************");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("*****************************************************************************");
            } 
        } // Filter_Svc
    }
}