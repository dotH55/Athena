
using Athena_REST.Models;

namespace Athena_REST.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public SvcCall Item { get; set; }
        public ItemDetailViewModel(SvcCall item = null)
        {
            //Title = item?.Text;
            Item = item;
        }
    }
}
