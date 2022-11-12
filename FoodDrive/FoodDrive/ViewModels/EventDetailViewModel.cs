using FoodDrive.Models;
using FoodDrive.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodDrive.ViewModels
{
    [QueryProperty(nameof(EventId), nameof(EventId))]
    public class EventDetailViewModel : BaseViewModel
    {
        public IEventService<Event> DataStore => DependencyService.Get<IEventService<Event>>();
        private string eventId;
        private string name;
        private string description;
        private string address;
        private Geocoder _geoCoder;
        private Xamarin.Forms.Maps.Map myMap;
        private Position position;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Address
        {
            get => address;
            set=> SetProperty(ref address, value);
        }
        public string EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                eventId = value;
                LoadEventId(value);
            }
        }
        public EventDetailViewModel(Xamarin.Forms.Maps.Map myMap)
        {
            this.myMap = myMap;
            _geoCoder = new Geocoder();
            //Pin pinNewYork = new Pin()
            //{
            //    Type = PinType.Place,
            //    Label = "Central Park NYC",
            //    Address = "New York City, NY 10022",
            //    Position = new Position(40.78d, -73.96d),
            //};
            //myMap.Pins.Add(pinNewYork);
            //myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinNewYork.Position, Distance.FromMeters(5000)));
        }
        public async void LoadEventId(string eventId)
        {
            try
            {
                var item = await DataStore.GetEventAsync(eventId);
                Name = item.Title;
                Description = item.Description;
                Address = item.FromStreet + ", " + item.FromCity;
                IEnumerable<Position> approximateLocations = await _geoCoder.GetPositionsForAddressAsync(Address);
                //var approximateLocations = await _geoCoder.GetPositionsForAddressAsync(Address);
                position = approximateLocations.FirstOrDefault();
                LoadMap();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
        public void LoadMap()
        {
            Pin pinFromLoc = new Pin()
            {
                Type = PinType.Place,
                Label = Name,
                Address = Address,
                Position = position,
            };
            myMap.Pins.Add(pinFromLoc);
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinFromLoc.Position, Distance.FromMeters(1000)));
        }
    }
}
