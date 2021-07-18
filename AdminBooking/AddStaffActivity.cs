using AdminBooking.Model;
using AdminBooking.Model.Staff;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.CardView.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AdminBooking
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    class AddStaffActivity: AppCompatActivity
    {
       
        private ImageView _avatar;
        string current;
        public static readonly int PickImageId = 1000;
        public EmployeeOwner staff = null;
        string token = null;
        string Url = "http://185.220.35.179/api/staff/";
        Android.Net.Uri url_avatar = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            var metrics = Resources.DisplayMetrics;
            token = Intent.GetStringExtra("id");
            AddStaff();
          //  AddAccountInfo();
            //   SetHello(0, metrics);

        }
        protected override async void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if ((requestCode == PickImageId) && (resultCode == Android.App.Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                _avatar.SetImageURI(uri);
                url_avatar = uri;
            }
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
                if (edit4.Text.Length == 2 || edit4.Text.Length == 5)
                {
                    edit4.Text += ".";
                    edit4.SetSelection(edit4.Text.Length);
                }
                linear.SetBackgroundColor(Color.ParseColor("#008E74"));
            };
            linear.Click += delegate
            {
                try
                {
                    staff = new EmployeeOwner
                    {
                        firstname = edit2.Text,
                        middlename = edit3.Text,
                        lastname = edit1.Text,
                        birthday = DateTime.Parse(edit4.Text),
                    };
                   
                    AddStaff2();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Не все поля заполнены", ToastLength.Short).Show();
                }     
            };

        }
        public void AddStaff2()
        {
            SetContentView(Resource.Layout.hello11);
            current = "add";
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
            EditText edit6 = FindViewById<EditText>(Resource.Id.editText6);
            EditText edit1 = FindViewById<EditText>(Resource.Id.editText1);
            EditText edit2 = FindViewById<EditText>(Resource.Id.editText2);

            edit1.TextChanged += delegate
            {
                if (edit1.Text.Length == 1)
                {
                    if (edit1.Text == "+" || edit1.Text == "8" || edit1.Text == "7")
                    {
                        edit1.Text = "7(";
                        edit1.SetSelection(2);
                    }
                }
                else if (edit1.Text.Length == 5)
                {
                    edit1.Text += ")";
                    edit1.SetSelection(6);
                }
            };

            back.Click += delegate
            {
                AddStaff();
            };
            card.Click += async delegate
            {
                try
                {
                    staff.position = edit6.Text;
                    staff.phone = edit1.Text;
                    staff.email = edit2.Text;
                    var client = PostClient(token);
                    HttpContent cont = new StringContent(JsonConvert.SerializeObject(staff, Formatting.Indented), Encoding.UTF8, "application/json");

                    var res = await client.PostAsync(Url, cont);
                    var result = JsonConvert.DeserializeObject<Response>(await res.Content.ReadAsStringAsync());
                    if (result.status.code == System.Net.HttpStatusCode.Created)
                    {
                        var account = JsonConvert.DeserializeObject<EmployeeOwner>(result.responce.ToString());
                        
                        if (url_avatar != null)
                        {
                            Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, url_avatar);
                            MemoryStream stream = new MemoryStream();
                            bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                            byte[] bitmapData = stream.ToArray();
                            client = PostClient(token);
                            var content = new MultipartFormDataContent();
                            var imageContent = new ByteArrayContent(bitmapData);
                            content.Add(imageContent, "file", staff.firstname);
                            var res_avatar = await client.PostAsync(Url + "userpic?id=" + account.id, content);
                            result = JsonConvert.DeserializeObject<Response>(await res_avatar.Content.ReadAsStringAsync());
                        }
                    }
                    AddStaff3();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Не все поля заполнены", ToastLength.Short).Show();
                }
            };
           
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
        public void AddStaff3()
        {
            SetContentView(Resource.Layout.addStaff2);
            current = "add";
            var card = FindViewById<CardView>(Resource.Id.cardView1);
            ImageView back = FindViewById<ImageView>(Resource.Id.imageView1);
           
            card.Click += async delegate
            {
                Intent intent = new Intent(this, typeof(HelloActivity)); //Added the type of Main Activity

                string put_name = token;

                intent.PutExtra("token", "staff");

                SetResult(Android.App.Result.Ok, intent); //added the SetResult method.
                Finish();
            };
        }
    }
}