using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using antalyakitchen.Models;
using antalyakitchen.Views;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace antalyakitchen.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Menu";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCategoryCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadCategoryCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                
                //var items = await DataStore.GetItemsAsync(true);
                string domain = "https://www.antalyakitchen.co.uk";
                string WebKey = "ck_c341b74bf4035bae85ef0897786d1c3353d58b35";
                string WebSecret = "cs_c8ac55eb4386d6decc0696c180618e9a3198bedd";
                var getNewOrderEndpoint = domain.ToString() + "/wp-json/wc/v3/products/categories";
                //var getNewOrderEndpoint = domain.ToString() + "/wp-json/wc/v3/orders?status=processing";
                var getnewOrderUrl = new Uri(getNewOrderEndpoint);


                string svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(WebKey + ":" + WebSecret));

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = HttpWebRequest.Create(getnewOrderUrl);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Basic " + svcCredentials);

                WebResponse response = request.GetResponse();

                var read = new StreamReader(response.GetResponseStream());
                string raw = read.ReadToEnd();

                var catListwooCommerce = JsonConvert.DeserializeObject<List<Item>>(raw.ToString());
                
               
                foreach (var item in catListwooCommerce)
                {
                    Items.Add(item);
                }

                //foreach (var item in Items)
                //{
                //    Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}