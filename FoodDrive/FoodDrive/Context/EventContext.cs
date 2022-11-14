﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace FoodDrive.Context
{
    public static class EventContext
    {
        // URL of REST service
        public static string RestUrl = "http://ec2-18-116-213-84.us-east-2.compute.amazonaws.com/api/v1/events{0}";

        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production
        //public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:3000/events/{0}" : "http://localhost:3000/events/{0}";
    }
}
