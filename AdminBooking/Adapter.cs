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

namespace AdminBooking
{
    public class MyAdapter : BaseAdapter
    {
        Activity _activity;
        private List<int> total_images;
        public MyAdapter(Activity activity,  List<int> total_)
        {
            _activity = activity;
            total_images = total_;
        }

        public override int Count
        {
            get { return 2; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.custom_spinner, parent, false);
            var imageScre = view.FindViewById<ImageView>(Resource.Id.left_pic);

            // because I have 5 images only
            int r = 0;
           
            while (r < 2){
                imageScre.SetImageResource(total_images[position]);
                r++;
            }

            return view;
        }

    } // end MyAdapter class



    public class CategoryAdapter : BaseAdapter
    {
        Activity _activity;
        private List<string> total_images;
        public CategoryAdapter(Activity activity, List<string> total_)
        {
            _activity = activity;
            total_images = total_;
        }

        public override int Count
        {
            get { return 3; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.custom_spinner_category, parent, false);
            var imageScre = view.FindViewById<TextView>(Resource.Id.left_pic);

            // because I have 5 images only
            int r = 0;

            while (r < 2)
            {
                imageScre.Text = total_images[position];
                r++;
            }

            return view;
        }

    } // end MyAdapter class

}