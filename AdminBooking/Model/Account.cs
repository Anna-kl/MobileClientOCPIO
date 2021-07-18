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
  public  class Account
    {
        public string first_name { get; set; }
        public string last_name { get; set; }

    }
    public class SendAccount
    {
        public string token { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }
}