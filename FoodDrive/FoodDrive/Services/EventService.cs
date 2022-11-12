using FoodDrive.Context;
using FoodDrive.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDrive.Services
{
    public class EventService : IEventService<Event>
    {
        public List<Event> events { get; private set; }
        //readonly List<Event> events;
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        public EventService()
        {
            events = new List<Event>();
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<Event> GetEventAsync(string id)
        {
            return await Task.FromResult(events.FirstOrDefault(s => s.Id == id));
        }
        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            Uri uri = new Uri(string.Format(EventContext.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Json format
                    //Users = JsonSerializer.Deserialize<List<Event>>(content, serializerOptions);
                    // Bson format
                    events = BsonSerializer.Deserialize<List<Event>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return await Task.FromResult(events);
        }
    }
}
