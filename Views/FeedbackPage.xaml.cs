using Athena_REST.Models;
using System;
using System.ComponentModel;
using System.Net.Mail;
using Xamarin.Forms;

namespace Athena_REST.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FeedbackPage : ContentPage
    {
        //private readonly FeedbackViewModel viewModel;

        public FeedbackPage()
        {
            InitializeComponent();
            //BindingContext = viewModel = new FeedbackViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Focus controlID_Lb
            Title_En.CursorPosition = 0;
        }

        private async void SendEmail()
        {
            try
            {
                if (Feedback_Ed.Text.ToString().Equals("") || Title_En.Text.ToString().Equals(""))
                {
                    await Application.Current.MainPage.DisplayAlert("Athena", "Check Fields", "OK");
                }
                else
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(SQLServer.GetIT_INTERN_EMAIL());
                    mail.To.Add(SQLServer.GetIT_INTERN_EMAIL());
                    mail.Subject = "*[ATHENA]: Feedback [TECH]: " + User.LOGGED_USER.Username + "[TITLE]: " + Title_En.Text;
                    mail.Body = "*[PROBLEM]: " + Feedback_Ed.Text;

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(SQLServer.GetIT_INTERN_EMAIL(), SQLServer.GetIT_INTERN_PW());

                    // Send
                    SmtpServer.Send(mail);
                    // Clear
                    Feedback_Ed.Text = "";
                    Title_En.Text = "";

                    //var context = Android.App.Application.Context;
                    //var tostMessage = "Thank you!";
                    //var durtion = ToastLength.Long;
                    //Toast.MakeText(context, tostMessage, durtion).Show();
                    Application.Current.MainPage = new MainPage();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Failed", ex.Message, "OK");
            }
        }
        /// <summary>
        /// This function creates a new Service Call
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Send_Feedback(object sender, EventArgs e)
        {
            SendEmail();
        }

        /// <summary>
        /// This function closes the current page
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">EventArgs</param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}