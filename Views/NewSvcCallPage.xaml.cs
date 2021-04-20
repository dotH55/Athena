using Athena_REST.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public SvcCall newSvc { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            /**Item = new SvcCall
            {
                Text = "Item name",
                Description = "This is an item description."
            };**/

            newSvc = new SvcCall
            {
                Notes = "",
                Model = "",
                SvcZip = "",
                Rowguid = "",
                SvcCity = "",
                SvcAddr1 = "",
                SvcAddr2 = "",
                SvcState = "",
                SvcCounty = "",
                SvcCallID = "",
                CallNumber = "",
                SvcBWMeter = "0",
                SvcTimeEnd = "",
                SvcSolution = "",
                SvcTimeBegin = "",
                ServicePhone = "",
                SerialNumber = "",
                SvcColorMeter = "0",
                ServiceContact = "",
                SvcNew_Problem = ""
            };

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Focus controlID_Lb
            controlID_Lb.CursorPosition = 0;
        }

        /// <summary>
        /// This function creates a new Service Call
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Create_newSvc(object sender, EventArgs e)
        {
            try
            {
                // Check if Fields are not empty
                string controlID = controlID_Lb.Text.ToString();
                string problem = Svc_Problem_Ed.Text.ToString();

                if (!controlID.Equals("") && !problem.Equals(""))
                {
                    newSvc.Status = "NEW";
                    newSvc.Problem = problem.ToUpper();
                    newSvc.SvcName = controlID.ToUpper();
                    newSvc.ControlID = controlID.ToUpper();
                    newSvc.TakenBy = User.LOGGED_USER.Initials;
                    DateTime today = DateTime.Now;
                    newSvc.DateTimeIn = today.ToString();

                    newSvc.DateTimeIn = today.ToString();
                    newSvc.SvcDate = today.ToString("MM/dd/yyyy");
                    newSvc.SvcTime = today.ToString("HH:MM");

                    MessagingCenter.Send(this, "AddItem", newSvc);
                    int result = await Services.MockDataStore.Create_Svc_REST(controlID.ToUpper(), problem.ToUpper());
                    if (result < 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Athena", "An Exception Occurred", "OK");
                    }
                    //await Services.MockDataStore.Query_Svc_REST();
                    await Navigation.PopModalAsync();
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "Field Missing!", "OK");
            }
        }

        /// <summary>
        /// This function closes the current page
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}