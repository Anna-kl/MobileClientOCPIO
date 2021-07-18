using AdminBooking.Model;
using AdminBooking.Model.Staff;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.CardView.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AdminBooking
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
   public class MainStaffActivity: AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        RecyclerView mRecyclerView;
        string token = String.Empty;
        string Url = "http://185.220.35.179/api/staff/";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            var metrics = Resources.DisplayMetrics;
            token = Intent.GetStringExtra("id");

            SetContentView(Resource.Layout.activity_main);
            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.LayoutSchedule;
            stub.Inflate();
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            this.SetTitle(Resource.String.mainscheduler);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            var head = navigationView.GetHeaderView(0);
            var avatar = head.FindViewById<ImageView>(Resource.Id.imageView1);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            navigation.SelectedItemId = Resource.Id.navigation_schedule;
            var client = PostClient(token);
            var res = await client.GetStringAsync(Url);
            var result = JsonConvert.DeserializeObject<Response>(res);

            if (result.status.code == System.Net.HttpStatusCode.OK)
            {
                var staffs = JsonConvert.DeserializeObject<List<SendEmployee>>(result.responce.ToString());
                List<StaffHelpersIcon> staffIcon = new List<StaffHelpersIcon>();
                foreach (var temp in staffs)
                {
                    var im = await client.GetByteArrayAsync(String.Format(Url + "userpic?id={0}", temp.id));
                    Bitmap imageBitmap = null;
                    imageBitmap = BitmapFactory.DecodeByteArray(im, 0, im.Length);
                    StaffHelpersIcon stIcon = new StaffHelpersIcon
                    {
                        Id = temp.id,
                        Name = temp.name,
                        idIcon = imageBitmap
                    };
                    staffIcon.Add(stIcon);
                }
                StaffAlbum mPhotoAlbum = new StaffAlbum(staffIcon);
                // Get our RecyclerView layout:
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

                var mLayoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
               
                // Plug the layout manager into the RecyclerView:
                mRecyclerView.SetLayoutManager(mLayoutManager);
                var mAdapter = new StaffIconAlbumAdapter(mPhotoAlbum, this);
                mRecyclerView.SetAdapter(mAdapter);
            }
            var linearWeekName = FindViewById<LinearLayout>(Resource.Id.linearLayout2);
            List<string> weeks = new List<string> { "Пон", "Втор", "Сред", "Чет", "Пят", "Суб", "Воск" };
            foreach (var week in weeks)
            {
              //  CardView card = new CardView(this);
                TextView TextWeek = new TextView(this);
                LinearLayout.LayoutParams linear = new LinearLayout.LayoutParams(
                    Convert.ToInt32(metrics.WidthPixels/8), 50);
                linear.SetMargins(10, 10, 5, 5);
                TextWeek.Text = week;
                TextWeek.LayoutParameters = linear;
                linearWeekName.AddView(TextWeek);
                linearWeekName.Invalidate();
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.tarif)
            {
                // Handle the camera action
            }
            else if (id == Resource.Id.sklad)
            {

            }
            else if (id == Resource.Id.staff)
            {
            }
            else if (id == Resource.Id.finance)
            {

            }
            else if (id == Resource.Id.action_settings)
            {
            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {

        }
        public HttpClient PostClient(string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (token != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return client;
        }

    }

}