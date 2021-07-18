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
    public class EmployeeOwner
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string link { get; set; }
        public DateTime date_add { get; set; }
        public DateTime birthday { get; set; }
        public string position { get; set; }
        public int id_owner { get; set; }
        public bool accepted { get; set; }
        public string specialization { get; set; }
    }
}