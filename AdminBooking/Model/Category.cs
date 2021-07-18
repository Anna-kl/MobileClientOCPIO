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
  public  class Category
    {
        public int id { get; set; }
        public int level { get; set; }
        public string name { get; set; }
        public int parent { get; set; }
    }
}