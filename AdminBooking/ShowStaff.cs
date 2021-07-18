using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminBooking
{
    public class Staff
    {
        public Staff(string name, string position, int reviews, int countReviews, Bitmap idIcon)
        {
            Name = name;
            Position = position;
            Reviews = reviews;
            IdIcon = idIcon;
            CountReviews = countReviews;
        }

        // Return the ID of the photo:
        public string Name { get; }
        public int Reviews { get; }
        public Bitmap IdIcon { get; }

        // Return the Caption of the photo:
        public string Position { get; }
        public int CountReviews { get; }

    }
    public class ShowStaff
    {

        private List<Staff> mStaffs;
        public ShowStaff(List<Staff> staffs)
        {
            mStaffs = staffs;
        }
        public int NumStaff
        {
            get { return mStaffs.Count; }
        }
        public Staff this[int i]
        {
            get { return mStaffs[i]; }
        }
    }

    public class StaffViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Position { get; private set; }
        public ImageView Icon { get; private set; }
        public TextView Review { get; private set; }
        public TextView ReviewsText { get; private set; }

        // Get references to the views defined in the CardView layout.
        public StaffViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            Name = itemView.FindViewById<TextView>(Resource.Id.textView1);
            Position = itemView.FindViewById<TextView>(Resource.Id.textView2);
            Icon = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
            Review = itemView.FindViewById<TextView>(Resource.Id.textView3);
            ReviewsText = itemView.FindViewById<TextView>(Resource.Id.textView4);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class ShowStaffAdapter : RecyclerView.Adapter
    {
        private MainActivity mainActivity;
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        ShowStaff mShowStaff;
        // Load the adapter with the data set (photo album) at construction time:
        public ShowStaffAdapter(ShowStaff showStaff, MainActivity mainActivity)
        {
            mShowStaff = showStaff;
            this.mainActivity = mainActivity;
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.staffRecycleView, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            StaffViewHolder vh = new StaffViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            StaffViewHolder vh = holder as StaffViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            vh.Name.Text = mShowStaff[position].Name;
            vh.Position.Text = mShowStaff[position].Position;
            vh.Icon.SetImageBitmap(mShowStaff[position].IdIcon);
            vh.Review.Text = mShowStaff[position].Reviews.ToString();
            vh.ReviewsText.Text = mShowStaff[position].CountReviews.ToString();
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return mShowStaff.NumStaff; }
        }

        // Raise an event when the item-click takes place:
        async void OnClick(int position)
        {
            if (position == 0)
            {

            }

        }

    }
}