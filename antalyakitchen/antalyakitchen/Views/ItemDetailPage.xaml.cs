using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using antalyakitchen.Models;
using antalyakitchen.ViewModels;

namespace antalyakitchen.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
               
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ItemsList.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var selectedItem = menuItem.CommandParameter as Item;
            
        }
    }
}