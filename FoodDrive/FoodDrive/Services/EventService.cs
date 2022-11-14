using FoodDrive.Context;
using FoodDrive.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodDrive.Services
{
    public class EventService : IEventService<Event>
    {
        public List<Event> events { get; private set; }
        public EventProd eventsProd { get; private set; }
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        public EventService()
        {
            var token = App.UserManager.GetToken();
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = authHeader;
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
            events = new List<Event>();
            eventsProd = new EventProd();
            Uri uri = new Uri(string.Format(EventContext.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Using json-server in development mode
                    // Json format
                    //Users = JsonSerializer.Deserialize<List<Event>>(content, serializerOptions);
                    // Bson format
                    //events = BsonSerializer.Deserialize<List<Event>>(content);
                    // Using aws in production mode
                    eventsProd = BsonSerializer.Deserialize<EventProd>(content);
                    events = eventsProd.dataInfo;
                    //Console.WriteLine(events);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return await Task.FromResult(events);
        }

        public async Task<IEnumerable<Event>> GetMyItemsAsync(bool forceRefresh = false)
        {
            events = new List<Event>();
            eventsProd = new EventProd();
            Uri uri = new Uri(string.Format(MyEventContext.RestUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    // Using json-server in development mode
                    // Json format
                    //Users = JsonSerializer.Deserialize<List<Event>>(content, serializerOptions);
                    // Bson format
                    //events = BsonSerializer.Deserialize<List<Event>>(content);
                    // Using aws in production mode
                    eventsProd = BsonSerializer.Deserialize<EventProd>(content);
                    string id = App.UserManager.GetUserId();
                    var unFilter = eventsProd.dataInfo;
                    foreach (var item in unFilter)
                    {
                        if (item.AssignUser.Equals(id))
                        {
                            events.Add(item);
                        }
                    }
                    //Console.WriteLine(events);
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
