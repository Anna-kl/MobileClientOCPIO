using System;
using System.Collections.Generic;
using System.Net.Http;
using AdminBooking.Model;
using AdminBooking.Model.Auth;
using Android.App;
using Android.Content;
using System.Linq;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.CardView.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using AndroidX.RecyclerView.Widget;
using Booking;
using Calligraphy;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Card;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using Plugin.Media;
using static Android.Provider.Settings;
using AdminBooking.Model.Staff;

namespace AdminBooking
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        RecyclerView mRecyclerView;
        PhotoAlbum mPhotoAlbum;
        List<Category> MainCategory;
        string number = null;
        RecyclerView.LayoutManager mLayoutManager;
        PokazAlbumAdapter mAdapter;
        /// <summary>
        /// StaffShow
        /// </summary>
        ShowStaff mShowStaff;
        List<Staff> MainStaff;
        ShowStaffAdapter mStaffAdapter;
        string mainName;
        Category mcategory=null;
        private Dialog popupDialog;
        private ImageView _avatar;
        string current;
        SendAccount User = null;
        List<Category> subcategory;
        private OrganizationList mOrganizationList;
        public static readonly int PickImageId = 1000;
        string token = null;
        string Url = "http://185.220.35.179/api/";
      //  string Url = "http://10.0.2.2:5000/api/";
       // string Url_indicator = "http://10.0.2.2:5002/api/";
        string Url_indicator = "https://sheltered-temple-10051.herokuapp.com/api/";
        static readonly List<SampleActivityMetaData> SampleMetaDataList = new List<SampleActivityMetaData>
                                                                          {
                                                                              new SampleActivityMetaData(Resource.String.hello1,
                                                                                                         Resource.String.hello2,
                                                                                                          typeof(HelloActivity)),
                                                                               new SampleActivityMetaData(Resource.String.hello1,
                                                                                                         Resource.String.hello2,
                                                                                                          typeof(AddStaffActivity)),
                                                                        new SampleActivityMetaData(Resource.String.hello1,
                                                                        Resource.String.hello2,
                                                                        typeof(MainStaffActivity))


    };

        public HttpClient PostClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (User != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + User.token);
            return client;
        }

      
        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
        //    {
        //        Android.Net.Uri uri = data.Data;
        //        _avatar.SetImageURI(uri);
        //    }
        //}
       
        private HttpClient GetClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
           
            return client;
        }

      

        List<int> LAYOUT =new List<int>() { Resource.Layout.hello1, Resource.Layout.hello2, Resource.Layout.hello3, Resource.Layout.hello4 };
           
        void OnItemClick(object sender, int position)
        {
            // Display a toast that briefly shows the enumeration of the selected photo:
            int photoNum = position + 1;
            Toast.MakeText(this, "This is photo number " + photoNum, ToastLength.Short).Show();
        }
        private void SetHello(int index, DisplayMetrics metrics)
        {
            SetContentView(LAYOUT[index]);
            current = "hello";
            ImageView image = FindViewById<ImageView>(Resource.Id.imageView1);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, Convert.ToInt32(metrics.HeightPixels * 0.7));
            image.LayoutParameters = param;
            var hello = FindViewById(Resource.Id.cardView1);
            LinearLayout.LayoutParams param_hello = new LinearLayout.LayoutParams(Convert.ToInt32(metrics.WidthPixels * 0.3),
                Convert.ToInt32(metrics.HeightPixels * 0.05));
            param_hello.SetMargins(Convert.ToInt32(metrics.WidthPixels * 0.4), 0, 0, 0);
            hello.LayoutParameters = param_hello;
            hello.Click += delegate
            {
                if (index < 3)
                {
                    index += 1;
                    SetHello(index, metrics);
                }
                else
                {
                    AddInformation();
                }
            };
        }
        private async void spinner_ItemSelected0(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }
            private async void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var item = spinner.GetItemAtPosition(e.Position);
            var i = MainCategory.Find(x => x.name == item.ToString());
            var client = PostClient();
            try
            {
                var aa = String.Format("http://185.220.35.179/api/categories/subcategory?level=1&parent={0}"
                    , i.id);
                var data = await client.GetStringAsync(String.Format("http://185.220.35.179/api/categories/subcategory?level=1&parent={0}"
                    , i.id));
                var res = JsonConvert.DeserializeObject<Response>(data);
                var items = JsonConvert.DeserializeObject<List<Category>>(res.responce.ToString());
         //       Spinner spinner2 = FindViewById<Spinner>(Resource.Id.spinner2);
         //       spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);

         //       var adap = new List<string>();
         //       foreach (var a in items)
         //       {
         //           adap.Add(a.name);
         //       }
         //       var adapter = new ArrayAdapter<string>(this,
         //Android.Resource.Layout.SimpleSpinnerItem, adap);
         //       spinner2.Adapter = adapter;
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
          

           
        }
        private async void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var item = spinner.GetItemAtPosition(e.Position);
           
        }
        public void AddInformationSMS(string number)
        {
            SetContentView(Resource.Layout.hello6);
            TextView text = FindViewById<TextView>(Resource.Id.textView1);
            text.Text = number;
            EditText edit1 = FindViewById<EditText>(Resource.Id.editText1);
            
            var liner = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            liner.Click += delegate
            {
               // NameCompany();
            };
            EditText edit2 = FindViewById<EditText>(Resource.Id.editText2);
            EditText edit3 = FindViewById<EditText>(Resource.Id.editText3);
            EditText edit4 = FindViewById<EditText>(Resource.Id.editText4);
            edit1.TextChanged += delegate
            {
                if (edit1.Text.Length == 1)
                {
                    edit1.ClearFocus();
                    edit2.RequestFocus();
                }
               
            };
            edit2.TextChanged += delegate
            {
                if (edit1.Text.Length == 1)
                {
                    edit2.ClearFocus();
                    edit3.RequestFocus();
                }
            };
            edit3.TextChanged += delegate
            {
                if (edit1.Text.Length == 1)
                {
                    edit3.ClearFocus();
                    edit4.RequestFocus();
                }
            };

            bool flag = false;
            edit4.TextChanged += delegate
            {
                flag = true;
            };
            CheckBox check = FindViewById<CheckBox>(Resource.Id.checkBox1);
            check.CheckedChange += delegate
            {
                if (check.Checked)
                {
                    liner.SetBackgroundColor(Color.ParseColor("#008E74"));
                }
            };
           
        }
        public void AddInformation()
        {
            SetContentView(Resource.Layout.hello5);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected0);
            List<int> image = new List<int> { Resource.Drawable.ru, Resource.Drawable.en };
            var myAdapter = new MyAdapter(this, image);
            spinner.Adapter = myAdapter;
            EditText edit = FindViewById<EditText>(Resource.Id.editText1);
            CheckBox box = FindViewById<CheckBox>(Resource.Id.checkBoxColor);
            var card = FindViewById(Resource.Id.cardView1);
        
            card.Click += delegate
              {
                  if (box.Checked && edit.Text.Length == 12)
                  {
                      number = edit.Text;
                      AddInformationSMS("+7"+edit.Text);
                  }
              };
            box.CheckedChange += delegate
            {
                if (box.Checked && edit.Text.Length == 12)
                {
                    card.SetBackgroundColor(Color.ParseColor("#008E74"));
                }
            };
            edit.TextChanged += delegate
            {
                if (edit.Text.Length == 1)
                {
                    edit.Text = "(" + edit.Text;
                    edit.SetSelection(2);
                }
                if (edit.Text.Length == 4)
                {
                    edit.Text = edit.Text + ")";
                    edit.SetSelection(5);
                }
                if (edit.Text.Length > 12)
                {
                    string toast = string.Format("Телефон указан не верно");
                    Toast.MakeText(this, toast, ToastLength.Long).Show();
                }
            };
        }

        private PlotModel CreatePlotModel(Zakaz zakaz)
        {
         
            var check = zakaz.complete + zakaz.canceled;
            if (check == 0)
            {
                var modelP1 = new PlotModel { Title = "Записи", Subtitle = "Записей не найдено" };
                return modelP1;
            }
            else
            {
                var modelP1 = new PlotModel { Title = "Записи" };

                var seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

                seriesP1.Slices.Add(new PieSlice("Завершенные", zakaz.complete) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
                seriesP1.Slices.Add(new PieSlice("Отказы", zakaz.canceled) { IsExploded = true });


                modelP1.Series.Add(seriesP1);

                return modelP1;
            }
        }
        private PlotModel CreatePlotModel1(Zakaz zakaz)
        {
            var plotModel = new PlotModel { Title = "Клиенты" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
            
                MarkerStroke = OxyColors.White
            };
            var series2 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,

                MarkerStroke = OxyColors.Red
            };

            //series1.Points.Add(new DataPoint(0.0, zakaz.current*0.5));
            //series1.Points.Add(new DataPoint(1.0, zakaz.current*1));
            //series1.Points.Add(new DataPoint(2.0, zakaz.current * 0.7));
            //series2.Points.Add(new DataPoint(0.0, zakaz.new_c*0.3));
            //series2.Points.Add(new DataPoint(1.0, zakaz.new_c * 3));
            //series2.Points.Add(new DataPoint(2.0, zakaz.new_c * 3));
        

            plotModel.Series.Add(series1);
            plotModel.Series.Add(series2);
            return plotModel;
        }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            var metrics = Resources.DisplayMetrics;
            CalligraphyConfig.InitDefault(
              new CalligraphyConfig.Builder()
                .SetDefaultFontPath("fonts/Montserrat-Regular.ttf")
                .SetFontAttrId(Resource.Attribute.fontPath)
                .Build()
            );
            var id = Android.OS.Build.Serial;
            if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
            {
                try
                {
                    var context = Android.App.Application.Context;
                    id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                }
                catch (Exception ex)
                {
                    Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                }
            }
            var res = GetClient();
            var sendres = await res.GetAsync(Url + "auths/" + id);
            if (sendres.StatusCode == System.Net.HttpStatusCode.NotFound)
                LoadHello(id);
            else
            {
                User = JsonConvert.DeserializeObject<SendAccount>(await sendres.Content.ReadAsStringAsync());
                if (User.role == "owner")
                    Main();
            }


        }
      
        public void AddStaff4()
        {
            SetContentView(Resource.Layout.hello13);
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            TextView text = FindViewById<TextView>(Resource.Id.textView1);
            card.Click += delegate
            {
                AddStaff();
            };
            var s = mainName + ", ваш аккаунт почти готов к работе";
            var span = new SpannableString(mainName+" ваш аккаунт почти готов к работе");

            span.SetSpan(new ForegroundColorSpan(Color.Blue), 0, mainName.Length, 0);  // "My" is red
            span.SetSpan(new ForegroundColorSpan(Color.Black), mainName.Length+1, s.Length-1, 0); // "Label" is blue
            text.SetText(span, TextView.BufferType.Spannable);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            navigation.NavigationItemSelected += delegate
            {
     
            };
        }
        public void AddStaff()
        {
            SetContentView(Resource.Layout.hello10);
            _avatar = FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            current = "add";
            back.Click += delegate
            {
             //   AddAccountInfo();
            };
            _avatar.Click += delegate
            {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
            };
            EditText edit1 = FindViewById<EditText>(Resource.Id.editText1);
            EditText edit2 = FindViewById<EditText>(Resource.Id.editText2);
            EditText edit3 = FindViewById<EditText>(Resource.Id.editText3);
            EditText edit4 = FindViewById<EditText>(Resource.Id.editText4);
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            edit4.TextChanged += delegate
            {
                if (edit4.Text.Length==2 || edit4.Text.Length == 5)
                {
                    edit4.Text += ".";
                    edit4.SetSelection(edit4.Text.Length);
                }
                linear.SetBackgroundColor(Color.ParseColor("#008E74"));
            };
            linear.Click += delegate
            {
                AddStaff2();
            };
            
        }
        public void AddStaff2()
        {
            SetContentView(Resource.Layout.hello11);
            current = "add";
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            back.Click += delegate
            {
                AddStaff();
            };
            card.Click += delegate
            {
                AddStaff3();
            };
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            navigation.NavigationItemSelected += delegate
            {

            };
        }
        public void AddStaff3()
        {
            SetContentView(Resource.Layout.hello12);
            current = "add";
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            back.Click += delegate
            {
                AddStaff2();
            };
            card.Click += delegate
            {
          
            };
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            navigation.NavigationItemSelected += delegate
            {

            };
        }
        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Android.App.Result.Ok)
            {
                token = data.GetStringExtra("token");
                if (token == "token")
                {
                    LoadStaff();
                }
                else
                {
                    Main();
                }
                //put_name and put_position should now hold the results you want, you can do whatever you want with these two values now in your MainActivity
            }
           
        }

        public void LoadHello(string id)
        {
            var sampleToStart = SampleMetaDataList[0];
            sampleToStart.Start(this, id);

        }

        public void LoadSchedule()
        {
            var sampleToStart = SampleMetaDataList[2];
            sampleToStart.Start(this, User.token);

        }
        public void MainLayout()
        {

            SetContentView(Resource.Layout.activity_main);
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

           

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
        }
        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var metrics = Resources.DisplayMetrics;
          
            //  NameCompany();
            SetHello(0, metrics);

            if (current == "hello")
            {
                SetHello(0, metrics);
            }
            if (current == "add")
            {
                AddStaff4();
            }
            //if(drawer.IsDrawerOpen(GravityCompat.Start))
            //{
            //    drawer.CloseDrawer(GravityCompat.Start);
            //}
            //else
            //{
            //    base.OnBackPressed();
            //}
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

      
        public async void Main()
        {
            SetContentView(Resource.Layout.activity_main);
            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.main;
            stub.Inflate();
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            toolbar.SetTitle(Resource.String.mainscreen);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            var head = navigationView.GetHeaderView(0);
            var avatar = head.FindViewById<ImageView>(Resource.Id.imageView1);
            try
            {
                var client = PostClient();
                var im = await client.GetByteArrayAsync(String.Format(Url + "accounts/userpic"));
                Bitmap imageBitmap = null;
                imageBitmap = BitmapFactory.DecodeByteArray(im, 0, im.Length);
                avatar.SetImageBitmap(imageBitmap);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
            List<DataPokaz> data = null;
            var post = PostClient();
            var resp = await post.GetAsync(Url_indicator);
            var r = JsonConvert.DeserializeObject<Response>(await resp.Content.ReadAsStringAsync());
            if (r.status.code == System.Net.HttpStatusCode.OK)
            {
                data = JsonConvert.DeserializeObject<List<DataPokaz>>(r.responce.ToString());
                var main_pokaz = new DataPokaz
                {
                    canceled = data.Sum(x => x.canceled),
                    sum = data.Sum(x => x.sum),
                    clients = data.Sum(x => x.clients),
                    orders = data.Sum(x => x.orders),
                    complete = data.Sum(x => x.complete),
                    new_client = data.Sum(x => x.new_client)
                };
                mPhotoAlbum = new PhotoAlbum(main_pokaz);
                // Get our RecyclerView layout:
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

                mLayoutManager = new LinearLayoutManager(this);

                // Plug the layout manager into the RecyclerView:
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mAdapter = new PokazAlbumAdapter(mPhotoAlbum, this);
                mRecyclerView.SetAdapter(mAdapter);

              var zakaz = new Zakaz
                {

                    canceled = main_pokaz.canceled,
                    complete = main_pokaz.complete,
                   
                };
                PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);

                view.Model = CreatePlotModel(zakaz);
            }
            //............................................................
            // Adapter Setup:

            // Create an adapter for the RecyclerView, and pass it the
            // data set (the photo album) to manage:

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;



        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        void LoadFragment(int id)
        {

            switch (id)
            {
                case Resource.Id.navigation_home:
                    Main();
                    break;

                case Resource.Id.navigation_schedule:
                    LoadSchedule();
                    break;

            }


        }
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public async void LoadStaff()
        {
            SetContentView(Resource.Layout.activity_main);
            ViewStub stub = FindViewById<ViewStub>(Resource.Id.layout_stub);
            stub.LayoutResource = Resource.Layout.MainStaff;
            stub.Inflate();
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            /////////////////////////////////////
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            linear.Click += delegate
            {
                var sampleToStart = SampleMetaDataList[1];
                sampleToStart.Start(this, User.token);
            };
            var client = PostClient();
            var res = await client.GetAsync(Url + "staff");
            var result =JsonConvert.DeserializeObject<Response>(await res.Content.ReadAsStringAsync());
            if (result.status.code == System.Net.HttpStatusCode.OK)
            {
                var sendStaffs = JsonConvert.DeserializeObject<List<SendEmployee>>(result.responce.ToString());
                List<Staff> staff = new List<Staff>();
                foreach (var stf in sendStaffs)
                {
                    var avatar = await client.GetByteArrayAsync(Url + "staff/userpic?id" + stf.id_account);
                    Bitmap imageBitmap = null;
                    imageBitmap = BitmapFactory.DecodeByteArray(avatar, 0, avatar.Length);
                   
                    staff.Add(new Staff(stf.name, stf.position, 5, 0, imageBitmap));
                }
                mShowStaff = new ShowStaff(staff);
                // Get our RecyclerView layout:
                mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

                mLayoutManager = new LinearLayoutManager(this);

                // Plug the layout manager into the RecyclerView:
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mStaffAdapter = new ShowStaffAdapter(mShowStaff, this);
                mRecyclerView.SetAdapter(mStaffAdapter);
            }
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                UserOut();
            }

            return base.OnOptionsItemSelected(item);
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
                LoadStaff();
            }
            else if (id == Resource.Id.finance)
            {

            }
            else if (id == Resource.Id.action_settings)
            {
                UserOut();
            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public async void UserOut()
        {
            if (User != null)
            {
                var client = PostClient();
                var res = await client.GetAsync(Url + "auths/out");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    User = null;
                    var activity = (Activity)this;
                    activity.FinishAffinity();
                }
                else
                {
                    Toast.MakeText(this, "Uid not found", ToastLength.Short).Show();
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


}

