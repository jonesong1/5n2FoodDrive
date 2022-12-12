using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public interface IEventService<T>
    {
        Task<T> GetEventAsync(string id);
        Task<List<T>> GetEventsAsync();
        Task<bool> PostEventAsync(string eventId);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetMyItemsAsync(bool forceRefresh = false);
    }
}
