using Athena_REST.Models;
using Athena_REST.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Athena_REST.ViewModels
{
    public class PartsViewModel : BaseViewModel
    {
        public ObservableCollection<Part> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PartsViewModel()
        {
            Title = "Lookup";
            Items = new ObservableCollection<Part>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {

            IsBusy = true;

            try
            {
                Items.Clear();
                System.Collections.Generic.List<Part> items = MockDataStore.GetLookup_Parts();
                foreach (Part item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}