﻿using Android.App;
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
    public class DataPokaz
    {
        public float sum { get; set; }
        public int orders { get; set; }
        public int canceled { get; set; }
        public int clients { get; set; }
        public int complete { get; set; }
        public int new_client {get;set;}
    }
}