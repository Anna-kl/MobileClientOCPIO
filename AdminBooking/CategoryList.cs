using AdminBooking.Model;
using Android.App;
using Android.Content;
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
    public class CategoryList
    {

        //static Category[] categ =
        //{
        //    new Category (1, "Салоны красоты"),
        //    new Category (2, "Юридические услуги"),
        //         new Category (3, "Автосервисы")
        //};
        private Category current;
        private List<Category> mCategories;
        public CategoryList(List<Category> categ, Category mcat)
        {
            current = mcat;
            mCategories = categ;
        }
        public int Check
        {
            get {
                if (current != null)
                {
                   var temp= mCategories.Find(x=>x.id==current.id);
                    return temp.id-1;
                }
                return -2;
                    }
        }
        public int NumCategories
        {
            get { return mCategories.Count; }
        }
        public Category this[int i]
        {
            get { return mCategories[i]; }
        }
    }

   
    public class CategoryViewHolder : RecyclerView.ViewHolder
    {

        public CheckBox Check { get; private set; }
      
        public TextView Name { get; private set; }
        // Get references to the views defined in the CardView layout.
        public CategoryViewHolder(View itemView, Action<int> listener, int position)
            : base(itemView)
        {
            // Locate and cache view references:

            Name = itemView.FindViewById<TextView>(Resource.Id.text2);

            Check = itemView.FindViewById<CheckBox>(Resource.Id.checkBox1);
            // Detect user clicks on the item view and report which item
            // was clicked (by layout position) to the listener:


            Check.CheckedChange += (sender, e) =>
            {
                if (Check.Checked && position!=base.LayoutPosition)
                    listener(base.LayoutPosition);
            };
                    
        }
    }
}