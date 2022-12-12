using FoodDrive.Models;
using FoodDrive.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FoodDrive.ViewModels
{
    public class EventMapModel : BaseViewModel
    {
        public IEventService<Event> DataStore => DependencyService.Get<IEventService<Event>>();
        private Geocoder _geoCoder;
        private Xamarin.Forms.Maps.Map myMap;
        public EventMapModel(Xamarin.Forms.Maps.Map myMap)
        {
            this.myMap = myMap;
            _geoCoder = new Geocoder();
            LoadEvents();
        }
        public async void LoadEvents()
        {
            try
            {
                var events = await DataStore.GetEventsAsync();
                foreach (var item in events)
                {
                    var Name = item.Title;
                    var Description = item.Description;
                    var Address = item.FromStreet + ", " + item.FromCity;
                    IEnumerable<Position> approximateLocations = await _geoCoder.GetPositionsForAddressAsync(Address);
                    var position = approximateLocations.FirstOrDefault();
                    Pin pinFromLoc = new Pin()
                    {
                        Type = PinType.Place,
                        Label = Name,
                        Address = Address,
                        Position = position,
                    };
                    myMap.Pins.Add(pinFromLoc);
                    myMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinFromLoc.Position, Distance.FromMeters(8000)));
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
