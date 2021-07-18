using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminBooking.Model
{
    public class Status
    {
        public System.Net.HttpStatusCode code { get; set; }
        public string message { get; set; }
    }
    public class Response
    {
        public Status status { get; set; }

        public object responce { get; set; }

    }
}