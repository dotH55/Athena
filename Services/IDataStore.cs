using System.Collections.Generic;
using System.Threading.Tasks;

namespace Athena_REST.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetItemAsync(string id);
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
