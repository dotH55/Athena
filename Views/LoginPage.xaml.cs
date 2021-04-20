using Athena_REST.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Athena_REST.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Get List of Tech
            User.Query_Users_REST();

            // Focus Username Entry
            Username_En.CursorPosition = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!User.GetConnectivity_Status())
            {
                ErrorMsg_Lb.Text = "Error: Internet Connectivity";
                ErrorMsg_Lb.IsVisible = true;
            }
        }

        /// <summary>
        /// This method Logs User in after
        /// checking credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LogIn(object sender, EventArgs e)
        {
            // Reset 
            ErrorMsg_Lb.Text = "";
            ErrorMsg_Lb.IsVisible = false;

            // Animation: Loading
            ActivityIndicator.IsEnabled = true;
            ActivityIndicator.IsRunning = true;
            ActivityIndicator.IsVisible = true;

            Device.StartTimer(new TimeSpan(0, 0, 2), () =>
            {
                // do something every 2 seconds
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        if (await User.CheckUserCredentials(e, Username_En.Text.ToUpper(), Password_En.Text.ToString()).ConfigureAwait(true))
                        {
                            Application.Current.MainPage = new MainPage();
                        }
                        else
                        {
                            ErrorMsg_Lb.Text = "Error: Incorrect Credentials!";
                            ErrorMsg_Lb.IsVisible = true;
                        }
                        ActivityIndicator.IsEnabled = false;
                        ActivityIndicator.IsRunning = false;
                        ActivityIndicator.IsVisible = false;
                    }
                    catch
                    {
                        ErrorMsg_Lb.Text = "Error: Check Internet & Credentials";
                        ErrorMsg_Lb.IsVisible = true;
                    }

                });
                // runs again, or false to stop
                return false;
            });
        }
    }
}