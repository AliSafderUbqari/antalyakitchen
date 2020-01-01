using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using antalyakitchen.Models;

namespace antalyakitchen.Droid
{
    [Activity(Label = "antalyakitchen", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //if (domain == "")
            //    return;

            //var pendingOrder = new;
            //string domain = "https://www.antalyakitchen.co.uk";
            //string WebKey = "ck_c341b74bf4035bae85ef0897786d1c3353d58b35";
            //string WebSecret = "cs_c8ac55eb4386d6decc0696c180618e9a3198bedd";
            //var getNewOrderEndpoint = domain.ToString() + "/wp-json/wc/v3/products/categories";
            ////var getNewOrderEndpoint = domain.ToString() + "/wp-json/wc/v3/orders?status=processing";
            //var getnewOrderUrl = new Uri(getNewOrderEndpoint);


            //string svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(WebKey + ":" + WebSecret));

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //WebRequest request = HttpWebRequest.Create(getnewOrderUrl);
            //request.Method = "GET";
            //request.Headers.Add("Authorization", "Basic " + svcCredentials);

            //WebResponse response = request.GetResponse();

            //var read = new StreamReader(response.GetResponseStream());
            //string raw = read.ReadToEnd();

            //var orderList = JsonConvert.DeserializeObject<List<WooCommerceOnlineOrders.Example>>(raw.ToString());
            //bool foundOrder = false;

            //for (int i = 0; i < orderList.Count; i++)
            //{
            //    LongAlert(orderList[i].id.ToString());
               
            //}


        }

        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}