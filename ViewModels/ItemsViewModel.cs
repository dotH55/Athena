using Athena_REST.Models;
using Athena_REST.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Athena_REST.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<SvcCall> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<SvcCall>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, SvcCall>(this, "AddItem", async (obj, item) =>
            {
                SvcCall newItem = item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                System.Collections.Generic.IEnumerable<SvcCall> items = await DataStore.GetItemsAsync(true);
                foreach (SvcCall item in items)
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