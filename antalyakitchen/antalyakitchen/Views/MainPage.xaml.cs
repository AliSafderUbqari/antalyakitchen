using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using antalyakitchen.Models;

namespace antalyakitchen.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.SelectMenu, (NavigationPage)Detail);
            //MenuPages.Add((int)MenuItemType.PopulateMenuwithListandCard, new NavigationPage(new ListViewItemswithViewCard()));
            //NavigateFromMenu(1);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.SelectMenu:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.NewItemPage:
                        MenuPages.Add(id, new NavigationPage(new NewItemPage()));
                        break;
                    case (int)MenuItemType.PopulateMenuwithListandCard:
                        MenuPages.Add(id, new NavigationPage(new ListViewItemswithViewCard()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(50);

                IsPresented = false;
            }
        }
    }
}