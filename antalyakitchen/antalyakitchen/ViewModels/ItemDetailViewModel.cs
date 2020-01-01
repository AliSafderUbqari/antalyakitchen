using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using antalyakitchen.Models;
using antalyakitchen.Views;
using Xamarin.Forms;

namespace antalyakitchen.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Item> ItemsList { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            string catId = "";
            catId = item.Id;
            Title = item?.name;

            ItemsList = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(catId));

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, items) =>
            {
                var newItem = items as Item;
                ItemsList.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand(string category)
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
                var getNewOrderEndpoint = domain.ToString() + "/wp-json/wc/v3/products?category=" + category;
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

                var catListwooCommerce = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(raw.ToString());
               
                //for (int i = 0; i < orderList.Count; i++)
                //{
                //    (orderList[i].id.ToString());

                //}
                foreach (var item in catListwooCommerce)
                {
                    ItemsList.Add(item);
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
