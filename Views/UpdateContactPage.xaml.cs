using Athena_REST.Services;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Athena_REST.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateContactPage : ContentPage
    {
        public UpdateContactPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Initialize Variables
            ServiceContact_Lb.Text = MockDataStore.GetSvcSelected().ServiceContact;
            ServicePhone_Lb.Text = MockDataStore.GetSvcSelected().ServicePhone;
            SvcAddr1_Lb.Text = MockDataStore.GetSvcSelected().SvcAddr1;
            SvcAddr2_Lb.Text = MockDataStore.GetSvcSelected().SvcAddr2;
            SvcCity_Lb.Text = MockDataStore.GetSvcSelected().SvcCity;
            SvcState_Lb.Text = MockDataStore.GetSvcSelected().SvcState;
            SvcZip_Lb.Text = MockDataStore.GetSvcSelected().SvcZip;
        }

        /// <summary>
        /// This method launches google drive
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Update_Contact(object sender, EventArgs args)
        {
            // Update Variables
            MockDataStore.GetSvcSelected().ServiceContact = ServiceContact_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().ServicePhone = ServicePhone_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().SvcAddr1 = SvcAddr1_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().SvcAddr2 = SvcAddr2_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().SvcCity = SvcCity_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().SvcState = SvcState_Lb.Text.ToString();
            MockDataStore.GetSvcSelected().SvcZip = SvcZip_Lb.Text.ToString();

            int result = await Services.MockDataStore.Update_Contact_Svc_REST();
            if (result < 0)
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "Error: " + result, "OK");
            }
            await Navigation.PopModalAsync();
        } // Update_Contact

        /// <summary>
        /// This method launches google drive
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Check_Phone_Format(object sender, EventArgs args)
        {
            if (!IsPhoneNumber(ServicePhone_Lb.Text.ToString()))
            {
                Application.Current.MainPage.DisplayAlert("Athena", "Check Phone Number", "OK");
            }
        } // Check_Phone_Format

        /// <summary>
        /// This method launches google drive
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Close_Page(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        } // Check_Phone_Format

        private static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }
    }
}