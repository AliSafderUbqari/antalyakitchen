using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace antalyakitchen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewItemswithViewCard : ContentPage
    {
        public ObservableCollection<CardView> ListDetails { get; set; }

        public ListViewItemswithViewCard()
        {
            InitializeComponent();
            ListDetails = new ObservableCollection<CardView>
            {
                new CardView{ImgIcon="logo.png", Name="Bar BQ"},
                new CardView{ImgIcon="logo.png", Name="Pizza"},
                new CardView{ImgIcon="logo.png", Name="Burgers"}
            };
            BindingContext = this;
        }

    }

    public class CardView
    {
        public string ImgIcon { get; set; }
        public string Name { get; set; }

    }
}