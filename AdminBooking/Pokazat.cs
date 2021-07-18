using AdminBooking.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AdminBooking
{
    public class PokazAlbumAdapter : RecyclerView.Adapter
    {
        private MainActivity mainActivity;
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        // Underlying data set (a photo album):
        PhotoAlbum mPhotoAlbum;


        // Load the adapter with the data set (photo album) at construction time:
        public PokazAlbumAdapter(PhotoAlbum photoAlbum, MainActivity mainActivity)
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
                        Inflate(Resource.Layout.PokazList, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            PokazViewHolder vh = new PokazViewHolder(itemView, OnClick);
            return vh;
        }

        // Fill in the contents of the photo card (invoked by the layout manager):
        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PokazViewHolder vh = holder as PokazViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            vh.Name.Text=mPhotoAlbum[position].Name;
            vh.SubName.Text = mPhotoAlbum[position].SubName;
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

    public class PhotoAlbum
    {
        // Built-in photo collection - this could be replaced with
        // a photo database:

        static Photo[] mBuiltInPhotos = {
             new Photo ( "Выручка",
                        "0" ),
             new Photo ( "Расходы",
                        "0" ),
                  new Photo ( "Прибыль",
                        "0" ),
        
            };

        // Array of photos that make up the album:
        private Photo[] mPhotos;

        // Random number generator for shuffling the photos:
        Random mRandom;

        // Create an instance copy of the built-in photo list and
        // create the random number generator:
        public PhotoAlbum(DataPokaz data)
        {
            mPhotos = new Photo[3] {
                new Photo("Новые клиенты",
                           data.new_client.ToString()),
             new Photo("Заказов",
                        data.orders.ToString()),
                  new Photo("Прибыль",
                        data.sum.ToString())
                    };
            
        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i]
        {
            get { return mPhotos[i]; }
        }

        // Pick a random photo and swap it with the top:
        //public int RandomSwap()
        //{
        //    // Save the photo at the top:
        //    Photo tmpPhoto = mPhotos[0];

        //    // Generate a next random index between 1 and 
        //    // Length (noninclusive):
        //    int rnd = mRandom.Next(1, mPhotos.Length);

        //    // Exchange top photo with randomly-chosen photo:
        //    mPhotos[0] = mPhotos[rnd];
        //    mPhotos[rnd] = tmpPhoto;

        //    // Return the index of which photo was swapped with the top:
        //    return rnd;
        //}

        // Shuffle the order of the photos:
        //public void Shuffle()
        //{
        //    // Use the Fisher-Yates shuffle algorithm:
        //    for (int idx = 0; idx < mPhotos.Length; ++idx)
        //    {
        //        // Save the photo at idx:
        //        Photo tmpPhoto = mPhotos[idx];

        //        // Generate a next random index between idx (inclusive) and 
        //        // Length (noninclusive):
        //        int rnd = mRandom.Next(idx, mPhotos.Length);

        //        // Exchange photo at idx with randomly-chosen (later) photo:
        //        mPhotos[idx] = mPhotos[rnd];
        //        mPhotos[rnd] = tmpPhoto;
        //    }
        //}
    }


    public class Photo
    {
        public Photo(string Name, string SubName)
        {
            this.Name = Name;
            this.SubName = SubName;
        }

        // Return the ID of the photo:
        public string Name { get; }

        // Return the Caption of the photo:
        public string SubName { get; }
    }
    public class OrganizationList
    {

        private List<Pokaz> mOrganization;
        public OrganizationList(List<Pokaz> categ)
        {

            mOrganization = categ;
        }
        public int NumOrganization
        {
            get { return mOrganization.Count; }
        }
        public Pokaz this[int i]
        {
            get { return mOrganization[i]; }
        }
    }
    public class PokazViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView SubName { get; private set; }

        // Get references to the views defined in the CardView layout.
        public PokazViewHolder(View itemView, Action<int> listener)
            : base(itemView)
        {
            // Locate and cache view references:
            Name = itemView.FindViewById<TextView>(Resource.Id.name);
            SubName = itemView.FindViewById<TextView>(Resource.Id.subname);

            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}