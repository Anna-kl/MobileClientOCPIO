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

namespace AdminBooking.Model.Staff
{
  public class SendEmployee
    {
        public int id { get; set; }
        public int id_account { get; set; }
        public string name { get; set; }
        public DateTime lastvisit { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string position { get; set; }
    }
}