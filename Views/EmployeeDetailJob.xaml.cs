using Athena_REST.Models;
using Athena_REST.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Athena_REST.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailJob : ContentPage
    {
        // Svc History Header
        private readonly ObservableCollection<JobGroupViewModel> Contents;
        // Svc History Content
        private ObservableCollection<JobGroupViewModel> ExpandedContents;

        public EmployeeDetailJob()
        {
            InitializeComponent();

            // Get Svc History
            Services.MockDataStore.Retrieve_Svc_History_REST().ConfigureAwait(true);

            ObservableCollection<JobGroupViewModel> items = new ObservableCollection<JobGroupViewModel>();

            foreach (JobClassModel x in Services.MockDataStore.GetSvc_History())
            {
                JobGroupViewModel jobGroup = new JobGroupViewModel(x.DateTimeCompleted);
                JobClassModel jobClass = new JobClassModel
                {
                    Invoice_Number = x.Invoice_Number,
                    Call_Number = x.Call_Number,
                    Problem = x.Problem,
                    Solution = x.Solution,
                    Solution_Parts = x.Solution_Parts,
                    Black_Meter = x.Black_Meter,
                    Color_Meter = x.Color_Meter
                };
                jobGroup.Add(jobClass);
                items.Add(jobGroup);
            }
            Contents = items;
            UpdateContent();
        }

        private void ShowContent(object sender, EventArgs args)
        {
            int index = ExpandedContents.IndexOf(
                ((JobGroupViewModel)((Button)sender).CommandParameter));
            Contents[index].Expanded = !Contents[index].Expanded;
            UpdateContent();
        }

        private void UpdateContent()
        {
            ExpandedContents = new ObservableCollection<JobGroupViewModel>();
            foreach (JobGroupViewModel group in Contents)
            {
                JobGroupViewModel jobs = new JobGroupViewModel(group.Title, group.Expanded)
                {
                    JobItems = group.Count
                };
                if (group.Expanded)
                {
                    foreach (Models.JobClassModel job in group)
                    {
                        jobs.Add(job);
                    }
                }
                ExpandedContents.Add(jobs);
            }
            MyListView.ItemsSource = ExpandedContents;
        }

        private async void ClosePage(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}