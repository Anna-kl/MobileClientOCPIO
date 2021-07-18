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

namespace AdminBooking.Model.Auth
{
    public class SendAuth
    {
        public int accountid { get; set; }
        public string token { get; set; }
        public bool isfilled { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }
        public string role { set; get; }
    }
}