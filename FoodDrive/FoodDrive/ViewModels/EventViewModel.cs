﻿using FoodDrive.Models;
using FoodDrive.Services;
using FoodDrive.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodDrive.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        private Event _selectedEvent;
        public ObservableCollection<Event> Events { get; }
        public Command LoadEventsCommand { get; }
        public Command AddEventCommand { get; }
        public Command<Event> EventTapped { get; }
        public IEventService<Event> DataStore => DependencyService.Get<IEventService<Event>>();

        public EventViewModel()
        {
            Title = "Available Events";
            Events = new ObservableCollection<Event>();
            LoadEventsCommand = new Command(async () => await ExecuteLoadEventsCommand());

            EventTapped = new Command<Event>(OnEventSelected);

            AddEventCommand = new Command(OnAddEvent);
        }
        async Task ExecuteLoadEventsCommand()
        {
            IsBusy = true;
            try
            {
                Events.Clear();
                var events = await DataStore.GetItemsAsync(true);
                foreach (var item in events)
                {
                    Events.Add(item);
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
        private async void OnAddEvent(object obj)
        {
            // call event map page to view map
            await Shell.Current.GoToAsync(nameof(EventMapPage));
        }
        private async void OnEventSelected(Event item)
        {
            if (item == null)
                return;
            // go to detail page and pass parameter
            await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?{nameof(EventDetailViewModel.EventId)}={item.Id}");
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public Event SelectedItem
        {
            get => _selectedEvent;
            set
            {
                SetProperty(ref _selectedEvent, value);
                OnEventSelected(value);
            }
        }
    }
}
