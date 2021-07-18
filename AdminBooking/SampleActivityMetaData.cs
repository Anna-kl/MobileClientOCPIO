using Android.App;
using Android.Content;
namespace Booking
{
    using System;

    /// <summary>
    ///     This class holds meta-data about the various activities that are used in this application.
    /// </summary>
    class SampleActivityMetaData
    {
        public SampleActivityMetaData(int titleResourceId, int descriptionId, Type activityToLaunch)
        {
            ActivityToLaunch = activityToLaunch;
            TitleResource = titleResourceId;
            DescriptionResource = descriptionId;
        }

        public Type ActivityToLaunch { get; }
        public int DescriptionResource { get; }
        public int TitleResource { get; }

        public void Start(Activity context, string id)
        {
            var i = new Intent(context, ActivityToLaunch);
            i.PutExtra("id", id);
            context.StartActivityForResult(i,0);
        }
    }
}