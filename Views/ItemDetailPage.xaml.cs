using Athena_REST.Models;
using Athena_REST.Services;
using Athena_REST.ViewModels;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        private static List<Entry> Entries;
        private static List<StackLayout> Stacks;
        private readonly ItemDetailViewModel ViewModel;


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;
            // Orgnanize view components
            OrganizeViewComponents();
        } // ItemDetailPage

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Services.MockDataStore.Retrieve_Issued_Parts_REST().ConfigureAwait(true);
            Fill_Parts_Entries();

            // Signing 
            Logged_User_En.Text = ViewModel.Item.ServiceContact;

            if (ViewModel.Item.Status.Equals("NEW"))
            {
                Solution_Im.IsEnabled = false;
                Solution_Im.IsVisible = false;

                Solution_Frame.IsEnabled = false;
                Solution_Frame.IsVisible = false;

                Part_Label_Stack.IsEnabled = false;
                Part_Label_Stack.IsVisible = false;

                Parts_Frame.IsEnabled = false;
                Parts_Frame.IsVisible = false;

                Part_50.IsEnabled = false;
                Part_50.IsVisible = false;

                Reschedule_Complet_Stack.IsEnabled = false;
                Reschedule_Complet_Stack.IsVisible = false;

                Sign_Frame.IsEnabled = false;
                Sign_Frame.IsVisible = false;

                Signing_Im.IsEnabled = false;
                Signing_Im.IsVisible = false;
            }

            if (ViewModel.Item.Status.Equals("INP"))
            {
                Claim_Icon.IsEnabled = false;
            }

            if (ViewModel.Item.Status.Equals("INP") && !ViewModel.Item.Tech_Initials.Equals(User.LOGGED_USER.Initials))
            {
                Claim_Icon.IsEnabled = false;

                Solution_Im.IsEnabled = false;
                Solution_Im.IsVisible = false;

                Solution_Frame.IsEnabled = false;
                Solution_Frame.IsVisible = false;

                Part_Label_Stack.IsEnabled = false;
                Part_Label_Stack.IsVisible = false;

                Parts_Frame.IsEnabled = false;
                Parts_Frame.IsVisible = false;

                Part_50.IsEnabled = false;
                Part_50.IsVisible = false;

                Reschedule_Complet_Stack.IsEnabled = false;
                Reschedule_Complet_Stack.IsVisible = false;

                Sign_Frame.IsEnabled = false;
                Sign_Frame.IsVisible = false;

                Signing_Im.IsEnabled = false;
                Signing_Im.IsVisible = false;
            }
        }

        /// <summary>
        /// This method puts View components into List Entries & Stacklayouts
        /// </summary>
        private void OrganizeViewComponents()
        {
            // Initialize
            Entries = new List<Entry>();
            Stacks = new List<StackLayout>();

            // Put components in List
            Entries.Add(Part_00_ID_En);
            Entries.Add(Part_00_Quantity_En);

            Entries.Add(Part_01_ID_En);
            Entries.Add(Part_01_Quantity_En);

            Entries.Add(Part_02_ID_En);
            Entries.Add(Part_02_Quantity_En);

            Entries.Add(Part_03_ID_En);
            Entries.Add(Part_03_Quantity_En);

            Entries.Add(Part_04_ID_En);
            Entries.Add(Part_04_Quantity_En);

            Entries.Add(Part_05_ID_En);
            Entries.Add(Part_05_Quantity_En);

            Entries.Add(Part_06_ID_En);
            Entries.Add(Part_06_Quantity_En);

            Entries.Add(Part_07_ID_En);
            Entries.Add(Part_07_Quantity_En);

            Entries.Add(Part_08_ID_En);
            Entries.Add(Part_08_Quantity_En);

            Entries.Add(Part_09_ID_En);
            Entries.Add(Part_09_Quantity_En);

            Entries.Add(Part_10_ID_En);
            Entries.Add(Part_10_Quantity_En);

            Entries.Add(Part_11_ID_En);
            Entries.Add(Part_11_Quantity_En);

            Entries.Add(Part_12_ID_En);
            Entries.Add(Part_12_Quantity_En);

            Entries.Add(Part_13_ID_En);
            Entries.Add(Part_13_Quantity_En);

            Entries.Add(Part_14_ID_En);
            Entries.Add(Part_14_Quantity_En);

            Entries.Add(Part_15_ID_En);
            Entries.Add(Part_15_Quantity_En);

            Entries.Add(Part_16_ID_En);
            Entries.Add(Part_16_Quantity_En);

            Entries.Add(Part_17_ID_En);
            Entries.Add(Part_17_Quantity_En);

            Entries.Add(Part_18_ID_En);
            Entries.Add(Part_18_Quantity_En);

            Entries.Add(Part_19_ID_En);
            Entries.Add(Part_19_Quantity_En);

            Entries.Add(Part_20_ID_En);
            Entries.Add(Part_20_Quantity_En);

            Entries.Add(Part_21_ID_En);
            Entries.Add(Part_21_Quantity_En);

            Entries.Add(Part_22_ID_En);
            Entries.Add(Part_22_Quantity_En);

            Entries.Add(Part_23_ID_En);
            Entries.Add(Part_23_Quantity_En);

            Entries.Add(Part_24_ID_En);
            Entries.Add(Part_24_Quantity_En);

            Entries.Add(Part_25_ID_En);
            Entries.Add(Part_25_Quantity_En);

            Entries.Add(Part_26_ID_En);
            Entries.Add(Part_26_Quantity_En);

            Entries.Add(Part_27_ID_En);
            Entries.Add(Part_27_Quantity_En);

            Entries.Add(Part_28_ID_En);
            Entries.Add(Part_28_Quantity_En);

            Entries.Add(Part_29_ID_En);
            Entries.Add(Part_29_Quantity_En);

            Entries.Add(Part_30_ID_En);
            Entries.Add(Part_30_Quantity_En);

            Entries.Add(Part_31_ID_En);
            Entries.Add(Part_31_Quantity_En);

            Entries.Add(Part_32_ID_En);
            Entries.Add(Part_32_Quantity_En);

            Entries.Add(Part_33_ID_En);
            Entries.Add(Part_33_Quantity_En);

            Entries.Add(Part_34_ID_En);
            Entries.Add(Part_34_Quantity_En);

            Entries.Add(Part_35_ID_En);
            Entries.Add(Part_35_Quantity_En);

            Entries.Add(Part_36_ID_En);
            Entries.Add(Part_36_Quantity_En);

            Entries.Add(Part_37_ID_En);
            Entries.Add(Part_37_Quantity_En);

            Entries.Add(Part_38_ID_En);
            Entries.Add(Part_38_Quantity_En);

            Entries.Add(Part_39_ID_En);
            Entries.Add(Part_39_Quantity_En);

            Entries.Add(Part_40_ID_En);
            Entries.Add(Part_40_Quantity_En);

            Entries.Add(Part_41_ID_En);
            Entries.Add(Part_41_Quantity_En);

            Entries.Add(Part_42_ID_En);
            Entries.Add(Part_42_Quantity_En);

            Entries.Add(Part_43_ID_En);
            Entries.Add(Part_43_Quantity_En);

            Entries.Add(Part_44_ID_En);
            Entries.Add(Part_44_Quantity_En);

            Entries.Add(Part_45_ID_En);
            Entries.Add(Part_45_Quantity_En);

            Entries.Add(Part_46_ID_En);
            Entries.Add(Part_46_Quantity_En);

            Entries.Add(Part_47_ID_En);
            Entries.Add(Part_47_Quantity_En);

            Entries.Add(Part_48_ID_En);
            Entries.Add(Part_48_Quantity_En);

            Entries.Add(Part_49_ID_En);
            Entries.Add(Part_49_Quantity_En);

            Stacks.Add(Part_00);
            Stacks.Add(Part_01);
            Stacks.Add(Part_02);
            Stacks.Add(Part_03);
            Stacks.Add(Part_04);
            Stacks.Add(Part_05);
            Stacks.Add(Part_06);
            Stacks.Add(Part_07);
            Stacks.Add(Part_08);
            Stacks.Add(Part_09);
            Stacks.Add(Part_10);
            Stacks.Add(Part_11);
            Stacks.Add(Part_12);
            Stacks.Add(Part_13);
            Stacks.Add(Part_14);
            Stacks.Add(Part_15);
            Stacks.Add(Part_16);
            Stacks.Add(Part_17);
            Stacks.Add(Part_18);
            Stacks.Add(Part_19);
            Stacks.Add(Part_20);
            Stacks.Add(Part_21);
            Stacks.Add(Part_22);
            Stacks.Add(Part_23);
            Stacks.Add(Part_24);
            Stacks.Add(Part_25);
            Stacks.Add(Part_26);
            Stacks.Add(Part_27);
            Stacks.Add(Part_28);
            Stacks.Add(Part_29);
            Stacks.Add(Part_30);
            Stacks.Add(Part_31);
            Stacks.Add(Part_32);
            Stacks.Add(Part_33);
            Stacks.Add(Part_34);
            Stacks.Add(Part_35);
            Stacks.Add(Part_36);
            Stacks.Add(Part_37);
            Stacks.Add(Part_38);
            Stacks.Add(Part_39);
            Stacks.Add(Part_40);
            Stacks.Add(Part_41);
            Stacks.Add(Part_42);
            Stacks.Add(Part_43);
            Stacks.Add(Part_44);
            Stacks.Add(Part_45);
            Stacks.Add(Part_46);
            Stacks.Add(Part_47);
            Stacks.Add(Part_48);
            Stacks.Add(Part_49);
        } // OrganizeViewComponents

        /// <summary>
        /// This method clears all the parts entries
        /// </summary>
        private void Clear_Parts_Entries()
        {
            try
            {
                for (int i = 0; i < 49 * 2; i += 2)
                {
                    (Entries[(i)]).Text = "";
                    (Entries[(i + 1)]).Text = "";
                }

                for (int i = 0; i < 49; i++)
                {
                    Stacks[i].IsEnabled = false;
                    Stacks[i].IsVisible = false;
                }
            }
            catch (Exception)
            {
                // TODO:
                Debug.WriteLine("Error: ItemDetailPage");
            }
        } // Clear_Parts_Entries

        /// <summary>
        /// This method clears all the parts entries
        /// </summary>
        private void Fill_Parts_Entries()
        {
            try
            {
                //Xamarin.Forms.Button
                //Xamarin.Forms.Editor
                // Clear Enties
                Clear_Parts_Entries();
                if (Services.MockDataStore.GetSvcSolution_Parts().Count > 0)
                {
                    int y = 0;
                    while (y < Services.MockDataStore.GetSvcSolution_Parts().Count)
                    {
                        // Note: Entries[y * 2]: editor is not accessible
                        // Entries[(y * 2)]: only way to access editor
                        //Without the parantheses in Entries[(y * 2)]
                        Entries[(y * 2)].Text = Services.MockDataStore.GetSvcSolution_Part(y).ID;
                        Entries[(y * 2) + 1].Text = Services.MockDataStore.GetSvcSolution_Part(y).Quantity;
                        Stacks[y].IsEnabled = true;
                        Stacks[y++].IsVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        } // Clear_Parts_Entries

        /// <summary>
        /// This method reschules a Svc
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Reschedule_Svc(object sender, EventArgs args)
        {
            //Solution_En.Text.ToString().Equals("")
            if (Solution_En.Text.ToString().Equals(""))
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "You must provide a solution", "OK");
            }
            else
            {
                // Get New Problem
                Services.MockDataStore.GetSvcSelected().SvcNew_Problem = "NEW PROBLEM: " + await DisplayPromptAsync("Athena", "NEW PROBLEM: ");

                // Update Svc
                Services.MockDataStore.GetSvcSelected().Status = "RS";
                Services.MockDataStore.GetSvcSelected().SvcSolution = "SOLUTION: " + Solution_En.Text.ToString();
                Services.MockDataStore.GetSvcSelected().SvcColorMeter = Color_En.Text;
                Services.MockDataStore.GetSvcSelected().SvcBWMeter = BW_En.Text;
                Services.MockDataStore.GetSvcSelected().SvcTimeBegin = Begin_Tp.Time.ToString();
                Services.MockDataStore.GetSvcSelected().SvcTimeEnd = End_Tp.Time.ToString();

                // Update Svc Status
                // Update TimeGiven
                //Athena.Services.MockDataStore.GetSvcSelected().SvcTime_WhenClaimed = DateTime.Now.ToString();
                // Update New Solution
                //Athena.Services.MockDataStore.GetSvcSelected().SvcSolution = Solution_En.Text.ToString();
                // Update Database
                int result = await Services.MockDataStore.Complet_Svc_REST();

                if (result < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Athena", "Result: " + result, "OK");
                }
                // TODO: Remove Svc from SvcCalls
                Services.MockDataStore.Query_Svc_REST();
                // Back to ItemPage
                await Navigation.PopToRootAsync();
            }

        } // Reschedule_Svc

        /// <summary>
        /// This method displays Svc History
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void View_Svc_History(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EmployeeDetailJob()));
        } // View_Svc_History

        private async void Lookup_Parts(object sender, EventArgs e)
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
        /// This method launches google drive
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Launch_Google_Map(object sender, EventArgs args)
        {
            // Launch Google Map
            Launch_Google_Map_Helper();
        } // Launch_Google_Map

        private async void Launch_Google_Map_Helper()
        {
            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    // https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    await Launcher.OpenAsync("http://maps.apple.com/?daddr=" + ViewModel.Item.SvcFullAddress);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                    await Launcher.OpenAsync("http://maps.google.com/?daddr=" + ViewModel.Item.SvcFullAddress);
                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    await Launcher.OpenAsync("bingmaps:?rtp=" + ViewModel.Item.SvcFullAddress);
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Athena", "Error: Incorrect Phone Number", "OK");
            }
        }

        /// <summary>
        /// This method dails Svc contact
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private void Dail_Svc(object sender, EventArgs args)
        {
            Dail_Svc_Helper();
        } // Dail_Svc

        /// <summary>
        /// Dail_Svc Helper
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private void Dail_Svc_Helper()
        {
            try
            {
                IPhoneCallTask phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                {
                    phoneDialer.MakePhoneCall(ViewModel.Item.ServicePhone);
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Athena", "Error: Incorrect Phone Number", "OK");
            }
        }

        /// <summary>
        /// This method alerts the user if selected time is outside of Office work hours
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void SetBeginTime(object sender, EventArgs args)
        {
            if (Begin_Tp.Time.CompareTo(new TimeSpan(6, 0, 0)) < 0)
            {
                Application.Current.MainPage.DisplayAlert("Athena", " Begin Time is outside working hours", "OK");
            }
        }

        /// <summary>
        /// This method alerts user if selected time is outside of Office work hours
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void SetEndTime(object sender, EventArgs args)
        {
            if (End_Tp.Time.CompareTo(new TimeSpan(20, 0, 0)) > 0)
            {
                Application.Current.MainPage.DisplayAlert("Athena", " End Time is outside working hours", "OK");
            }
        }

        /// <summary>
        /// This method claims a Svc
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Claim_Svc(object sender, EventArgs args)
        {
            if (ViewModel.Item.Status.Equals("INP"))
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "This Call has already been claimed", "OK");
            }
            else
            {
                // Update Svc Status
                //ViewModel.Item.Status = "INP";
                Services.MockDataStore.GetSvcSelected().Status = "INP";
                // Update Svc Tech
                Services.MockDataStore.GetSvcSelected().Tech_Initials = User.LOGGED_USER.Initials;

                int result = await Services.MockDataStore.Claim_Svc_REST(true);
                if (result < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Athena", "Result: " + result, "OK");
                }
                else
                {
                    Services.MockDataStore.Query_Svc_REST();
                    Application.Current.MainPage.DisplayAlert("Athena", "Svc Claimed!", "OK");
                }
            }
            // Back to ItemPage
            //await Navigation.PopToRootAsync();
        } // Claim_Svc

        /// <summary>
        /// This method Complets a SvcCall
        /// It first checks to make sure All the fields have been completed
        /// Then it makes an API Request to update the Database
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Complet_Svc(object sender, EventArgs args)
        {
            // Start a new task (this launches a new thread)
            //bool x = false;
            bool y = true;
            if (ViewModel.Item.Status.Equals("NEW"))
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "You must first claim this Svc", "OK");
                y = false;
            }
            else if (Solution_En.Text.ToString().Equals(""))
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "You must provide a solution", "OK");
                y = false;
            }
            else if (Begin_Tp.Time.ToString().Substring(0, 4).Equals("9:01") && End_Tp.Time.ToString().Substring(0, 5).Equals("17:01"))
            {
                await Application.Current.MainPage.DisplayAlert("Athena", "Set Begin & End Time", "OK");
                y = false;
            }

            Stream temp_Image = await SignaturePad_Pv.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);


            MockDataStore.Sign_Svc(temp_Image, IT_Service_Lb.Text.ToString(), IT_Service_Tg.IsToggled);

            if (y)
            {
                Services.MockDataStore.GetSvcSelected().Status = "COM";
                Services.MockDataStore.GetSvcSelected().SvcSolution = "SOLUTION: " + Solution_En.Text.ToString();
                Services.MockDataStore.GetSvcSelected().SvcColorMeter = Color_En.Text;
                Services.MockDataStore.GetSvcSelected().SvcBWMeter = BW_En.Text;
                Services.MockDataStore.GetSvcSelected().SvcTimeBegin = Begin_Tp.Time.ToString();
                Services.MockDataStore.GetSvcSelected().SvcTimeEnd = End_Tp.Time.ToString();

                int result = await Services.MockDataStore.Complet_Svc_REST();
                if (result < 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Athena", "Result: " + result, "OK");
                }

                _ = Services.MockDataStore.Query_Svc_REST();
                await Navigation.PopToRootAsync();
            }
        } // Complet_Svc

        /// <summary>
        /// This method is used to enable entry fields for solution parts
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void AddPart(object sender, EventArgs args)
        {
            try
            {
                if (!Part_50_ID_En.Text.ToString().Equals("") && !Part_50_Quantity_En.Text.ToString().Equals(""))
                {
                    // Add New Parts
                    Services.MockDataStore.Add_SvcSolution_Part(Part_50_ID_En.Text.ToString(), Part_50_Quantity_En.Text.ToString());
                    // Clear Entries
                    Part_50_ID_En.Text = "";
                    Part_50_Quantity_En.Text = "";
                    // Fill Entries
                    Fill_Parts_Entries();
                } //
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        } // AddPart

        /// <summary>
        /// This method removes a part from the solution
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private void Delete_Issued_Part(object sender, EventArgs args)
        {
            try
            {
                Services.MockDataStore.Delete_SvcSolution_Part(int.Parse((((Button)sender).StyleId).Substring(5, 2)));
                Fill_Parts_Entries();
            }
            catch (Exception ex)
            {
                // Do Nothing
                Debug.WriteLine(ex.StackTrace);
            }
        } // Delete_Issued_Part

        /// <summary>
        /// This method prompts to user for action
        /// This method lets the user Lookup Parts, Lookup History, Claim a Svc, Un-Claim a Svc, Call Customer, Open Google Map & Save a note
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void OpenMenu(object sender, EventArgs args)
        {
            try
            {
                //string action = await DisplayActionSheet("Menu", "Cancel", null, "Lookup Parts", "Lookup Svc History", "Logout");
                string action = "";
                if (MockDataStore.GetSvcSelected().Status.Equals("INP") && MockDataStore.GetSvcSelected().Tech_Initials.Equals(User.LOGGED_USER.Initials))
                {
                    action = await DisplayActionSheet("Menu", "Cancel", null, "Lookup Parts", "Lookup Svc History", "Google Map", "Call", "Un-Claim Svc", "Save Notes");
                }
                else
                {
                    action = await DisplayActionSheet("Menu", "Cancel", null, "Lookup Parts", "Lookup Svc History", "Google Map", "Call", "Save Notes");
                }
                await Helper(action);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.StackTrace);
            }
        }

        /// <summary>
        /// OpenMenu Helper
        /// See OpenMenu()
        /// </summary>
        /// <param name="action">Menu Item</param>
        private async Task Helper(string action)
        {
            switch (action)
            {
                case "Lookup Parts":
                    // Look-up parts
                    string temp = await DisplayPromptAsync("Athena", "Item Number: ");
                    if (!temp.ToString().Equals(""))
                    {
                        await MockDataStore.Query_Parts_REST(temp.ToUpper());
                        await Navigation.PushModalAsync(new NavigationPage(new PartsPage()));
                    }

                    break;
                case "Lookup Svc History":
                    // View Service History
                    await Navigation.PushModalAsync(new NavigationPage(new EmployeeDetailJob()));
                    break;
                case "Google Map":
                    // Launch Google Map
                    Launch_Google_Map_Helper();
                    break;
                case "Call":
                    Dail_Svc_Helper();
                    break;
                case "Un-Claim Svc":
                    try
                    {
                        if (ViewModel.Item.Status.Equals("NEW"))
                        {
                            await Application.Current.MainPage.DisplayAlert("Athena", "Svc has not been claimed", "OK");
                        }
                        else
                        {
                            // Update Svc Status
                            Services.MockDataStore.GetSvcSelected().Status = "NEW";
                            // Update Svc Tech
                            Services.MockDataStore.GetSvcSelected().Tech_Initials = "";

                            int result = await Services.MockDataStore.Claim_Svc_REST(false);
                            if (result <= 0)
                            {
                                await Application.Current.MainPage.DisplayAlert("Athena", "Result: " + result, "OK");
                            }

                            // Pop page
                            await Navigation.PopToRootAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.StackTrace);
                    }

                    break;

                case "Save Notes":
                    //Notes_En.Text.ToString().Equals("")

                    if (Notes_En.Text.ToString().Equals(""))
                    {
                        // Inform User
                        await Application.Current.MainPage.DisplayAlert("Athena", "Notes Field is Empty", "OK");
                    }
                    else
                    {
                        // Update Note
                        ViewModel.Item.Notes = Notes_En.Text;
                        Services.MockDataStore.GetSvcSelected().Notes = Notes_En.Text.ToString();

                        // Update Database
                        int result = await Services.MockDataStore.Save_SvcNotes_REST();
                        if (result < 0)
                        {
                            await Application.Current.MainPage.DisplayAlert("Athena", "Note Not Saved!: " + result, "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Athena", "Note Saved!" + result, "OK");
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// This method displays Svc History
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void IT_Service(object sender, EventArgs args)
        {
            if (IT_Service_Tg.IsToggled)
            {
                IT_Service_Lb.Text = "IT Call";
                IT_Service_Lb.TextColor = Color.FromHex("f44336");
                IT_Service_Lb.FontAttributes = FontAttributes.Bold;
            }
            else
            {
                IT_Service_Lb.Text = "Service Call";
                IT_Service_Lb.TextColor = Color.Gray;
                IT_Service_Lb.FontAttributes = FontAttributes.None;
                //f44336
            }
        } // IT_Service

        /// <summary>
        /// This method displays Svc History
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Update_Contact(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UpdateContactPage()));
        } // Edit_Contact

        private bool IsPhoneNumber(string number)
        {
            // private static readonly string PHONE_NUMBER_REGEX = @"^(\d{3}\.?\d{3}\.?\d{4}?){1}";
            // return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
            return Regex.Match(number, @"^(\d{3}\.?\d{3}\.?\d{4}?)$").Success;
        }
    }
}