using Athena_REST.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private MainPage RootPage => Application.Current.MainPage as MainPage;

        private readonly List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse SvcCalls", Icon = "Solution_Icon.png" },
                new HomeMenuItem {Id = MenuItemType.Feedback, Title="Feedback", Icon = "Feedback_Icon.png" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About", Icon = "Guarantee_Icon.png" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                int id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            // Menupage User profile
            User_Profile_im.Source = GetProfileImage(User.LOGGED_USER.Initials);
            User_name_Lb.Text = User.LOGGED_USER.Username;
            User_Location_Lb.Text = User.LOGGED_USER.Location;
        }

        /// <summary>
        /// Lookup Parts
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private async void Handle_Clicked(object sender, EventArgs e)
        {
            //Lookup_Parts_Helper();
        }

        private string GetProfileImage(string v)
        {
            switch (v)
            {
                case "KWS":
                    return User.LOGGED_USER.Initials + ".png";
                    break;
                case "CK_":
                    return User.LOGGED_USER.Initials + ".png";
                    break;
                case "DAS":
                    return User.LOGGED_USER.Initials + ".png";
                    break;
                case "DR_":
                    return User.LOGGED_USER.Initials + ".png";
                    break;
                case "WL_":
                    return User.LOGGED_USER.Initials + ".png";
                    break;
                default:
                    return "User_Icon.png";
                    break;
            }
        }
    }
}