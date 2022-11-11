using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public interface IEventService<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
