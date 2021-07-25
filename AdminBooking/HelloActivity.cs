using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AdminBooking.Model;
using AdminBooking.Model.Auth;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Android.Provider;
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

namespace AdminBooking
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class HelloActivity : AppCompatActivity
    {
        RecyclerView mRecyclerView;
        PhotoAlbum mPhotoAlbum;
        List<Category> MainCategory;
        string number = null;
        RecyclerView.LayoutManager mLayoutManager;
        PokazAlbumAdapter mAdapter;
        string mainName;
        Category mcategory = null;
        private Dialog popupDialog;
        private ImageView _avatar;
        string current;
        string token = null;
        Android.Net.Uri url_avatar = null;
     //   string Url = "http://10.0.2.2:5000/api/";
        string Url = "http://185.220.35.179/api/";
        List<Category> subcategory;
        SendRegister user = new SendRegister();
        private OrganizationList mOrganizationList;
        public static readonly int PickImageId = 1000;
        static readonly List<SampleActivityMetaData> SampleMetaDataList = new List<SampleActivityMetaData>
                                                                          {
                                                                              new SampleActivityMetaData(Resource.String.google_map,
                                                                                                         Resource.String.google_map_desc,
                                                                                                          typeof(GoogleMapActivity))
        };

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

        private async void BtnshowPopup_Click(object sender, System.EventArgs e)
        {
            popupDialog = new Dialog(this);
            LoadCategory();
        }
        public async void LoadCategory()
        {

            popupDialog.SetContentView(Resource.Layout.popupCategory);

            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Show();

            // Some Time Layout width not fit with windows size
            // but Below lines are not necessery
            popupDialog.Window.SetLayout(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            popupDialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
            var res = PostClient(null);
            string r;
            int stage = 0;
            ImageButton close = popupDialog.FindViewById<ImageButton>(Resource.Id.imageButton1);
            close.Click += delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
            };

            EditText edit = popupDialog.FindViewById<EditText>(Resource.Id.editText12);

            r = await res.GetStringAsync("http://185.220.35.179/api/categories");


            var data = JsonConvert.DeserializeObject<Response>(r);
            var MainCategory = JsonConvert.DeserializeObject<List<Category>>(data.responce.ToString());
            var mCategoryAlbum = new CategoryList(MainCategory, mcategory);
            edit.TextChanged += delegate
            {
              var  temp_MainCategory = MainCategory.FindAll(x => x.name.ToLower().StartsWith(edit.Text.ToLower()));
                var mCategoryAlbum = new CategoryList(temp_MainCategory, null);
                var mAdapterCategory = new CategoryAdapterList(mCategoryAlbum, this, stage);
                mRecyclerView = popupDialog.FindViewById<RecyclerView>(Resource.Id.recyclerViewcategory);
                mLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mRecyclerView.SetAdapter(mAdapterCategory);
            };
            // Plug in my adapter:
            var mAdapterCategory = new CategoryAdapterList(mCategoryAlbum, this, stage);
            mRecyclerView = popupDialog.FindViewById<RecyclerView>(Resource.Id.recyclerViewcategory);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.SetAdapter(mAdapterCategory);
        }
        private async void BtnshowPopup_Click2(object sender, System.EventArgs e)
        {
            popupDialog = new Dialog(this);
            popupDialog.SetContentView(Resource.Layout.popupCategory);

            popupDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            popupDialog.Show();
            EditText edit = popupDialog.FindViewById<EditText>(Resource.Id.editText12);
            
            // Some Time Layout width not fit with windows size
            // but Below lines are not necessery
            popupDialog.Window.SetLayout(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            popupDialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
            var res = PostClient(null);
            string r;
            int stage = 0;
            ImageButton close = popupDialog.FindViewById<ImageButton>(Resource.Id.imageButton1);
            close.Click += delegate
            {
                popupDialog.Dismiss();
                popupDialog.Hide();
            };

            stage = 1;
            r = await res.GetStringAsync(String.Format("http://185.220.35.179/api/categories/subcategory?level=1&parent={0}"
                , mcategory.id));

            var data = JsonConvert.DeserializeObject<Response>(r);
            var MainCategory = JsonConvert.DeserializeObject<List<Category>>(data.responce.ToString());
            var mCategoryAlbum = new CategoryList(MainCategory, null);
            edit.TextChanged += delegate
            {
               var temp_MainCategory = MainCategory.FindAll(x =>  x.name.ToLower().StartsWith(edit.Text.ToLower()));
                var mCategoryAlbum = new CategoryList(temp_MainCategory, null);
                var mAdapterCategory = new CategoryAdapterList(mCategoryAlbum, this, stage);
                mRecyclerView = popupDialog.FindViewById<RecyclerView>(Resource.Id.recyclerViewcategory);
                mLayoutManager = new LinearLayoutManager(this);
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mRecyclerView.SetAdapter(mAdapterCategory);
            };
            // Plug in my adapter:
            var mAdapterCategory = new CategoryAdapterList(mCategoryAlbum, this, stage);
            mRecyclerView = popupDialog.FindViewById<RecyclerView>(Resource.Id.recyclerViewcategory);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.SetAdapter(mAdapterCategory);

        }
        protected override async  void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((requestCode == PickImageId) && (resultCode == Android.App.Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                _avatar.SetImageURI(uri);
                url_avatar = uri;
               

            } else if (resultCode == Android.App.Result.Ok)
            {
                string put_name = data.GetStringExtra("address");
                var location = FindViewById<EditText>(Resource.Id.editText7);
                location.Text = put_name;
                //put_name and put_position should now hold the results you want, you can do whatever you want with these two values now in your MainActivity
            }
        }

        
        public void NameCompany()
        {
            SetContentView(Resource.Layout.hello7);
            var linear = FindViewById(Resource.Id.textView1);
            current = "add";
            linear.Click += delegate
            {
                SetContentView(Resource.Layout.hello8);
                EditText edit1 = FindViewById<EditText>(Resource.Id.editText1);
                EditText edit2 = FindViewById<EditText>(Resource.Id.editText2);
                var card = FindViewById(Resource.Id.textView1);
                _avatar = FindViewById<ImageView>(Resource.Id.imageView2);
                _avatar.Click += delegate
                {
                    Intent = new Intent();
                    Intent.SetType("image/*");
                    Intent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
                };
                edit2.TextChanged += delegate
                {
                    card.SetBackgroundColor(Color.ParseColor("#008E74"));
                };
                card.Click += delegate
                {
                    mainName = edit1.Text;
                    user.firstname = edit1.Text;
                    user.lasname = edit2.Text;
                    user.role = "owner";
                    AddAccountInfo();
                };
            };
        }
        private HttpClient GetClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public class CategoryAdapterList : RecyclerView.Adapter
        {
            private HelloActivity mainActivity;
            private CategoryList mCategoryList;

            private int Stage;
            public event EventHandler<int> ItemClick;
            public override RecyclerView.ViewHolder
               OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                // Inflate the CardView for the photo:
                View itemView = LayoutInflater.From(parent.Context).
                            Inflate(Resource.Layout.recyclerViewcategory, parent, false);

                // Create a ViewHolder to find and hold these view references, and 
                // register OnClick with the view holder:

                CategoryViewHolder vh = new CategoryViewHolder(itemView, OnClick, mCategoryList.Check);
                return vh;
            }
            public CategoryAdapterList(CategoryList catAlbum, HelloActivity mainActivity, int stage)
            {
                mCategoryList = catAlbum;
                this.mainActivity = mainActivity;
                Stage = stage;

            }
            public override void
                OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                CategoryViewHolder vh = holder as CategoryViewHolder;

                // Set the ImageView and TextView in this ViewHolder's CardView 
                // from this position in the photo album:
                if (mainActivity.mcategory != null)
                    if (mainActivity.mcategory.id == mCategoryList[position].id)
                    {
                        vh.Check.Checked = true;

                    }
                vh.Name.Text = mCategoryList[position].name;
            }

            // Return the number of photos available in the photo album:
            public override int ItemCount
            {
                get { return mCategoryList.NumCategories; }
            }

            // Raise an event when the item-click takes place:
            async void OnClick(int position)
            {
                if (Stage == 0)
                {

                    mainActivity.mcategory = mCategoryList[position];
                    TextView text = mainActivity.FindViewById<TextView>(Resource.Id.editText5);
                    text.Text = mCategoryList[position].name;
                    MaterialCardView mcard = mainActivity.FindViewById<MaterialCardView>(Resource.Id.materialCardView1);
                    mcard.Visibility = ViewStates.Visible;
                    mainActivity.user.level0 = mCategoryList[position].id;
                    mainActivity.user.level1 = new List<int>();
                    mainActivity.LoadCategory();


                }
                if (Stage == 1)
                {
                    if (mainActivity.subcategory == null)
                    {
                        mainActivity.subcategory = new List<Category>();
                        LinearLayout liner = mainActivity.FindViewById<LinearLayout>(Resource.Id.linearLayout1);
                        liner.SetBackgroundColor(Color.ParseColor("#008E74"));
                    }

                    mainActivity.subcategory.Add(mCategoryList[position]);
                    TextView text = mainActivity.FindViewById<TextView>(Resource.Id.editText6);
                    if (text.Text.Length > 0)
                    {
                        text.Text += ", ";
                    }
                    mainActivity.user.level1.Add(mCategoryList[position].id);
                    text.Text += mCategoryList[position].name;

                }
            }
        }

        List<int> LAYOUT = new List<int>() { Resource.Layout.hello1, Resource.Layout.hello2, Resource.Layout.hello3, Resource.Layout.hello4 };
     
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
            var client = PostClient(null);
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
            catch (Exception ee)
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
                if (edit2.Text.Length == 1)
                {
                    edit2.ClearFocus();
                    edit3.RequestFocus();
                }
            };
            edit3.TextChanged += delegate
            {
                if (edit3.Text.Length == 1)
                {
                    edit3.ClearFocus();
                    edit4.RequestFocus();
                }
            };
            bool flag = false;
            edit4.TextChanged += delegate
            {
                if (edit4.Text.Length == 1)
                {
                    edit4.ClearFocus();
                    flag = true;
                } else
                {
                    edit4.Text = edit4.Text.Substring(0, 1);
                    edit4.ClearFocus();
                }
             
            };
            liner.Click += async delegate
            {
                var client = PostClient(null);
                var send = new SendCode
                {
                    code = String.Format("{0}{1}{2}{3}", edit1.Text, edit2.Text, edit3.Text, edit4.Text),
                    phone = number.Replace("+","").Replace("(","").Replace(")","")
                };
                HttpContent cont = new StringContent(JsonConvert.SerializeObject(send, Formatting.Indented), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(string.Format("{0}auths/code", Url), cont);
                var res = JsonConvert.DeserializeObject<Response>(await result.Content.ReadAsStringAsync());
                if (res.status.code == System.Net.HttpStatusCode.OK)
                {
                    var reg = new Register
                    {
                        email = number,
                        password = "1234",
                        role = "owner",
                        uid = user.id
                    };
                    HttpContent smsCont = new StringContent(JsonConvert.SerializeObject(reg, Formatting.Indented), Encoding.UTF8, "application/json");
                    var sms = await client.PostAsync(Url + "auths/token", smsCont);
                    var smsResult = JsonConvert.DeserializeObject<Response>(await sms.Content.ReadAsStringAsync());
                    if (smsResult.status.code == System.Net.HttpStatusCode.NotFound)
                        NameCompany();
                    else
                    {
                        var user = JsonConvert.DeserializeObject<SendAuth>(smsResult.responce.ToString());
                        Intent intent = new Intent(this, typeof(HelloActivity)); //Added the type of Main Activity

                        string put_name = token;

                        intent.PutExtra("token", user.token);

                        SetResult(Android.App.Result.Ok, intent); //added the SetResult method.
                        Finish();
                    }
                }
                else
                {
                    TextView error = FindViewById<TextView>(Resource.Id.textView2);
                    error.Text = "Неверный код";
                }
            };

            CheckBox check = FindViewById<CheckBox>(Resource.Id.checkBox1);
            check.CheckedChange +=async delegate
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

            card.Click +=async delegate
            {
                if (box.Checked && edit.Text.Length == 12)
                {
                    number = edit.Text;
                    var client = PostClient(null);
                    var phone = String.Format("{0}auths/code?phone=7{1}",
                        Url, number.Replace("(", "").Replace(")", ""));
                    var res = await client.GetStringAsync(String.Format("{0}auths/code?phone=7{1}",
                        Url, number.Replace("(", "").Replace(")","")));
                    var result = JsonConvert.DeserializeObject<Response>(res);
                    if (result.status.code == System.Net.HttpStatusCode.OK)
                    {
                        AddInformationSMS("+7" + edit.Text);
                    } else
                    {
                        TextView error = FindViewById<TextView>(Resource.Id.textError);
                        error.Text = "Невозможно сделать звонок";
                    }
                }
            };
            box.CheckedChange += delegate
            {
                if (box.Checked && edit.Text.Length == 12)
                {
                    card.SetBackgroundColor(Color.ParseColor("#008E74"));
                }
            };
            int delete = 0;
            edit.TextChanged += delegate
            {
                if (edit.Text.Length > 12)
                {
                    //string toast = string.Format("Телефон указан не верно");
                    //Toast.MakeText(this, toast, ToastLength.Long).Show();
                    edit.Text = edit.Text.Substring(0, 12);
                    edit.ClearFocus();
                }
                if (edit.Text.Length == 1 && delete < edit.Text.Length)
                {
                    edit.Text = "(" + edit.Text;
                    edit.SetSelection(2);
                }
                if (edit.Text.Length == 4 && delete < edit.Text.Length)
                {
                    edit.Text = edit.Text + ")";
                    edit.SetSelection(5);
                }
                
                delete = edit.Text.Length;
            };
        }
        protected override void OnCreate(Bundle savedInstanceState)
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
            var a = Intent.GetStringExtra("id");
            user.id = a;
            SetHello(0, metrics);

        }
        public void LoadMap()
        {
            var sampleToStart = SampleMetaDataList[0];

            sampleToStart.Start(this, null);

        }
      
        public async void AddAccountInfo()
        {
            SetContentView(Resource.Layout.hello9);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            MaterialCardView mcard = FindViewById<MaterialCardView>(Resource.Id.materialCardView1);
            mcard.Click += BtnshowPopup_Click2;
            mcard.Visibility = ViewStates.Gone;
            var Company = FindViewById<EditText>(Resource.Id.editText1);
            var http = FindViewById<EditText>(Resource.Id.editText2);
            var location = FindViewById<EditText>(Resource.Id.editText7);
            location.Click += delegate
            {
               
                    LoadMap();
         
            };
            http.FocusChange += delegate
              {
                  if (http.Text.Length == 0)
                  {
                      http.Text += "http://";
                      http.SetSelection(http.Text.Length);
                  }
              };
            back.Click += delegate
            {
                NameCompany();
            };
            //       spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            //       var res = PostClient();
            //       var r = await res.GetStringAsync("http://185.220.35.179/api/categories");
            //       var data = JsonConvert.DeserializeObject<Response>(r);
            //       MainCategory = JsonConvert.DeserializeObject<List<Category>>(data.responce.ToString());
            //       var adap = new List<string>();
            //       foreach (var a in MainCategory)
            //       {
            //           adap.Add(a.name);
            //       }
            //       var adapter = new ArrayAdapter<string>(this,
            //Android.Resource.Layout.SimpleSpinnerItem, adap);
            //       spinner.Adapter = adapter;
            TextView category = FindViewById<TextView>(Resource.Id.editText5);
            category.Click += BtnshowPopup_Click;
            var radio1 = FindViewById<RadioButton>(Resource.Id.radio1);
            var radio2 = FindViewById<RadioButton>(Resource.Id.radio2);
            var radio3 = FindViewById<RadioButton>(Resource.Id.radio3);
            radio1.CheckedChange += delegate
            {
                user.single = true;
                radio2.Checked = false;
                radio3.Checked = false;
            };
            radio2.CheckedChange += delegate
            {
                user.single = false;
                radio1.Checked = false;
                radio3.Checked = false;
            };
            radio3.CheckedChange += delegate
            {
                user.single = false;
                radio2.Checked = false;
                radio1.Checked = false;
            };
            LinearLayout liner = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            EditText edit3 = FindViewById<EditText>(Resource.Id.editText13);
            http.TextChanged += delegate
            {
                if (Company.Text.Length > 0)
                {
                    liner.SetBackgroundColor(Color.ParseColor("#008E74"));
                }
            };
            liner.Click += async delegate
            {
                user.name = Company.Text;
                user.site = http.Text;
                user.email = edit3.Text;
                user.address = location.Text;
                user.phone = number.Replace("(", "").Replace(")", "");
                var res = PostClient(null);
                string temp = JsonConvert.SerializeObject(user);
                HttpContent cont = new StringContent(JsonConvert.SerializeObject(user, Formatting.Indented), Encoding.UTF8, "application/json");
                var result = await res.PostAsync(Url + "auths/phone", cont);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    token = JsonConvert.DeserializeObject<string>(await result.Content.ReadAsStringAsync());
                    if (url_avatar != null)
                    {
                        Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, url_avatar);
                        //Bitmap bitmap = BitmapFactory.DecodeFile(path);
                        MemoryStream stream = new MemoryStream();
                        bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                        byte[] bitmapData = stream.ToArray();
                        var client = PostClient(token);
                        var content = new MultipartFormDataContent();
                        var imageContent = new ByteArrayContent(bitmapData);
                        content.Add(imageContent, "file", user.name);
                        var res_avatar = await client.PostAsync(Url + "accounts/userpic", content);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            AddStaff4();
                        }
                    } else
                    {
                        AddStaff4();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Ошибка", ToastLength.Short).Show();
                }
            };
        }
        public void AddStaff4()
        {
            SetContentView(Resource.Layout.hello13);
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            TextView text = FindViewById<TextView>(Resource.Id.textView1);
            card.Click += delegate
            {
                Intent intent = new Intent(this, typeof(HelloActivity)); //Added the type of Main Activity

                string put_name = token;
                if (token != null)
                    intent.PutExtra("token", put_name);
                else 
                    intent.PutExtra("address", "test");

                SetResult(Android.App.Result.Ok, intent); //added the SetResult method.
                Finish();
            };
            // потом удалить
            //  mainName = "Test";
            var s = mainName + ", ваш аккаунт почти готов к работе";
            var span = new SpannableString(mainName + " ваш аккаунт почти готов к работе");

            span.SetSpan(new ForegroundColorSpan(Color.Blue), 0, mainName.Length, 0);  // "My" is red
            span.SetSpan(new ForegroundColorSpan(Color.Black), mainName.Length + 1, s.Length - 1, 0); // "Label" is blue
            text.SetText(span, TextView.BufferType.Spannable);
           

        }
      
       
        public void AddStaff()
        {
            SetContentView(Resource.Layout.hello10);
            _avatar = FindViewById<ImageView>(Resource.Id.imageView2);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            current = "add";
            back.Click += delegate
            {
                AddAccountInfo();
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
                if (edit4.Text.Length == 2 || edit4.Text.Length == 5)
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
