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
  public  class StaffHelpersIcon
    {
        public Bitmap idIcon { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class StaffAlbum
    {

        // Array of photos that make up the album:
        private List<StaffHelpersIcon> mPhotos;

        public StaffAlbum(List<StaffHelpersIcon> data)
        {
            mPhotos = data;

        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos.Count; }
        }

        // Indexer (read only) for accessing a photo:
        public StaffHelpersIcon this[int i]
        {
            get { return mPhotos[i]; }
        }

       
    }

    public class StaffIconViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Icon { get;  set; }
        public TextView Name { get;  set; }

        // Get references to the views defined in the CardView layout.
        public StaffIconViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            Icon = itemView.FindViewById<ImageView>(Resource.Id.imageView1);
            Name = itemView.FindViewById<TextView>(Resource.Id.textView1);
            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class StaffIconAlbumAdapter : RecyclerView.Adapter
    {
        private MainStaffActivity mainActivity;
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        StaffAlbum mPhotoAlbum;


        // Load the adapter with the data set (photo album) at construction time:
        public StaffIconAlbumAdapter(StaffAlbum photoAlbum, MainStaffActivity mainActivity)
        {
            mPhotoAlbum = photoAlbum;
            this.mainActivity = mainActivity;
        }

        // Create a new photo CardView (invoked by the layout manager): 
        public override RecyclerView.ViewHolder
            OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.staffImage, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            StaffIconViewHolder vh = new StaffIconViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            StaffIconViewHolder vh = holder as StaffIconViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            vh.Icon.SetImageBitmap(mPhotoAlbum[position].idIcon);
            vh.Name.Text = mPhotoAlbum[position].Name;
        }

        // Return the number of photos available in the photo album:
        public override int ItemCount
        {
            get { return mPhotoAlbum.NumPhotos; }
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