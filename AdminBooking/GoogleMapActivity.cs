using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.Snackbar;
using Google.Places;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AdminBooking
{
    [Activity(Label = "Выберите адрес")]

    public class GoogleMapActivity : AppCompatActivity, IOnMapReadyCallback
    {
        GoogleMap googleMap;

        static readonly int REQUEST_PERMISSIONS_LOCATION = 1000;
        static readonly string TAG = "MyLocationActivity";
        ArrayAdapter adapter = null;
        GoogleMapPlaceClass objMapClass;
        GeoCodeJSONClass objGeoCodeJSONClass;
        string autoCompleteOptions;
        string[] strPredictiveText;
        int index = 0;
        AutoCompleteTextView txtSearch;
        const string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        //browser key for place webservice
        const string strGoogleApiKey = "AIzaSyAUWRv0bP2fRntsuodkYCwpg4mFOc52Yi0";
        const string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        public HttpClient PostClient()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            //googleMap.UiSettings.ZoomControlsEnabled = true;
            //googleMap.UiSettings.CompassEnabled = true;
            //googleMap.UiSettings.MyLocationButtonEnabled = true;
            if (this.PerformRuntimePermissionCheckForLocation(REQUEST_PERMISSIONS_LOCATION))
            {
                InitializeUiSettingsOnMap();
            }
            if (!PlacesApi.IsInitialized)
            {
                PlacesApi.Initialize(this, "AIzaSyBSSl0g1vsOq5d8MyqemeFN9miNG0UHXvI");
            }
            //txtSearch = FindViewById<AutoCompleteTextView>(Resource.Id.editText2);
            //txtSearch.TextChanged += async delegate (object sender, Android.Text.TextChangedEventArgs e)
            //{
            //    try
            //    {
            //        autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + txtSearch.Text + "&key=" + strGoogleApiKey);

            //        if (autoCompleteOptions == "Exception")
            //        {
            //            Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
            //            return;
            //        }
            //        objMapClass = JsonConvert.DeserializeObject<GoogleMapPlaceClass>(autoCompleteOptions);
            //        strPredictiveText = new string[objMapClass.predictions.Count];
            //        index = 0;
            //        foreach (Prediction objPred in objMapClass.predictions)
            //        {
            //            strPredictiveText[index] = objPred.description;
            //            index++;
            //        }
            //        adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, strPredictiveText);
            //        txtSearch.Adapter = adapter;
            //    }
            //    catch
            //    {
            //        Toast.MakeText(this, "Unable to process at this moment!!!", ToastLength.Short).Show();
            //    }

            //};
        }
        async Task<string> fnDownloadString(string strUri)
        {
            WebClient webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
                Console.WriteLine(strResultData);
            }
            catch
            {
                strResultData = "Exception";
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
                });
            }
            finally
            {
                webclient.Dispose();
                webclient = null;
            }

            return strResultData;
        }

        private void SearchPlaces(object sender, EventArgs e)
        {
            List<Place.Field> fileds = new List<Place.Field>();
            fileds.Add(Place.Field.Address);
            fileds.Add(Place.Field.LatLng);

            Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fileds).
                SetCountry("RU").
                Build(this);

            StartActivityForResult(intent, 0);
        }

        //private void SearchPlaces(object sender, EventHandler e)
        //{
        //    List<Place.Field> fileds = new List<Place.Field>();
        //    fileds.Add(Place.Field.Address);
        //    fileds.Add(Place.Field.LatLng);

        //    Intent intent = new Autocomplete.IntentBuilder(AutocompleteActivityMode.Overlay, fileds).
        //        SetCountry("RU").
        //        Build(this);

        //    StartActivityForResult(intent, 0);
        //}
         void InitializeUiSettingsOnMap()
        {
            googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.MyLocationEnabled = true;

        }
        //async System.Threading.Tasks.Task AddMarkersToMapAsync()
        //{
        //    var location = await Geolocation.GetLastKnownLocationAsync();
        //    var a = Intent.GetIntExtra("category", 0);
        //    var send = new AccountGeo
        //    {
        //        lat = location.Latitude,
        //        lng = location.Longitude,
        //        category_id = a

        //    };
        //    var client = PostClient();
        //    var d = JsonConvert.SerializeObject(send);
        //    HttpContent cont = new StringContent(d, System.Text.Encoding.UTF8, "application/json");
        //    var responce = await client.PostAsync("http://185.220.35.179/api/geo/near", cont);
        //    string data = await responce.Content.ReadAsStringAsync();
        //    var resp = JsonConvert.DeserializeObject<Response>(data);
        //    if (resp.status.code == System.Net.HttpStatusCode.OK)
        //    {
        //        var res = JsonConvert.DeserializeObject<List<AccountSendGeo>>(resp.responce.ToString());
        //        foreach (var b in res)
        //        {
        //            LatLng VimyRidgeLatLng = new LatLng(b.lat, b.lng);
        //            var vimyMarker = new MarkerOptions();
        //            vimyMarker.SetPosition(VimyRidgeLatLng)
        //                      .SetTitle(String.Format("От {0}", b.price))
        //                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed));
        //            googleMap.AddMarker(vimyMarker);
        //            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(VimyRidgeLatLng, 15);
        //            googleMap.MoveCamera(cameraUpdate);
        //        }
        //    }
        //    LatLng PasschendaeleLatLng = new LatLng(location.Latitude, location.Longitude);



        //    //var passchendaeleMarker = new MarkerOptions();
        //    //passchendaeleMarker.SetPosition(PasschendaeleLatLng)
        //    //                   .SetTitle("PasschendaeleLatLng");
        //    //googleMap.AddMarker(passchendaeleMarker);

        //    // We create an instance of CameraUpdate, and move the map to it.

        //}
        async void AutoCompleteOption_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            //to soft keyboard hide
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(txtSearch.WindowToken, HideSoftInputFlags.NotAlways);
          //  map.Clear();
            if (txtSearch.Text != string.Empty)
            {
                var sb = new StringBuilder();
                sb.Append(strGeoCodingUrl);
                sb.Append("?address=").Append(txtSearch.Text);
                string strResult = await fnDownloadString(sb.ToString());
                if (strResult == "Exception")
                {
                    Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();

                }
                //below used single quote to avoid html interpretation
                objGeoCodeJSONClass = JsonConvert.DeserializeObject<GeoCodeJSONClass>(strResult);
                LatLng Position = new LatLng(objGeoCodeJSONClass.results[0].geometry.location.lat, objGeoCodeJSONClass.results[0].geometry.location.lng);
                //updateCameraPosition(Position);
                //MarkOnMap("MyLocation", Position);
            }

        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapOcpio);
            txtSearch = FindViewById<AutoCompleteTextView>(Resource.Id.txtTextSearch);
            LinearLayout linear = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            linear.Click += delegate {
                Intent intent = new Intent(this, typeof(GoogleMapActivity)); //Added the type of Main Activity

                string put_name = txtSearch.Text;
                intent.PutExtra("address", put_name);
               
                SetResult(Android.App.Result.Ok, intent); //added the SetResult method.
                Finish();
            };
            txtSearch.TextChanged += async delegate { 
                try
                {
                    linear.SetBackgroundColor(Color.ParseColor("#008E74"));
                    autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + txtSearch.Text + "&key=" + strGoogleApiKey);

                    if (autoCompleteOptions == "Exception")
                    {
                        Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
                        return;
                    }
                    objMapClass = JsonConvert.DeserializeObject<GoogleMapPlaceClass>(autoCompleteOptions);
                    strPredictiveText = new string[objMapClass.predictions.Count];
                    index = 0;
                    foreach (Prediction objPred in objMapClass.predictions)
                    {
                        strPredictiveText[index] = objPred.description;
                        index++;
                    }
                    adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, strPredictiveText);
                    txtSearch.Adapter = adapter;
                }
                catch
                {
                    Toast.MakeText(this, "Unable to process at this moment!!!", ToastLength.Short).Show();
                }

            };
            txtSearch.ItemClick += AutoCompleteOption_Click;
            txtSearch.Hint = "Enter source  ";
          

            this.AddMapFragmentToLayout(Resource.Id.map_container);

        }



        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == REQUEST_PERMISSIONS_LOCATION)
            {
                if (grantResults.AllPermissionsGranted())
                {
                    // Permissions granted, nothing to do.
                    // Carry on and let the MapFragment do it's own thing.
                    InitializeUiSettingsOnMap();
                }
                else
                {
                    // Permissions not granted!
                    Log.Info(TAG, "The app does not have location permissions");

                    var layout = FindViewById(Android.Resource.Id.Content);
                    Snackbar.Make(layout, Resource.String.location_permission_missing, Snackbar.LengthLong).Show();
                    Finish();
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }



    }
}

