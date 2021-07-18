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
    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
      
        public string email { get; set; }
      
        public string phone { get; set; }
      
        public int id_user { get; set; }
        public DateTime update { get; set; }
      
    
}
}