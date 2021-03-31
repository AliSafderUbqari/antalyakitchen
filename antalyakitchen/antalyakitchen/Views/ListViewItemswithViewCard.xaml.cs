using Android.Support.V7.App;
using System;
using System.Collections.Generic;

using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            ListDetails = GetTables();
            //ListDetails = new ObservableCollection<CardView>
            //{
            //    new CardView{ItemImage="logo.png", itemName_En="Bar BQ"},
            //    new CardView{ItemImage="as.png", itemName_En="Pizza"},
            //    new CardView{ItemImage="at.png", itemName_En="Burgers"}
            //};
            BindingContext = this;
        }

        public ObservableCollection<CardView> GetTables()
        {
            ObservableCollection<CardView> result = new ObservableCollection<CardView>();
            DataTable dtItems = new DataTable();
            string connectionString = "Data Source=192.168.10.4;Database=CertaNew;User Id=sa;Password=123";

            string selectQuery = String.Format("select itemName_En,ItemImage from items where ItemImage is not null");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    //connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // this will query your database and return the result to your datatable
                    da.Fill(dtItems);
                    connection.Close();
                    da.Dispose();

                    //var cardview = new ObservableCollection<CardView>();
                    foreach (DataRow row in dtItems.Rows)
                    {
                        result.Add(new CardView()
                        {
                            //ItemImage = row["ItemImage"],
                            ItemImage = byteArrayToImage((byte[])row["ItemImage"]),
                            itemName_En = (string)row["itemName_En"],
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                //ex.Message.ToString();
                App.Current.MainPage.DisplayAlert("Exception", ex.Message.ToString(), "OK");
                return result;
                
            }
        }

        public ImageSource byteArrayToImage(byte[] byteArrayIn)
        {
            Image image = new Image();
            //byte[] bytes = Convert.FromBase64String()
            //MemoryStream ms = new MemoryStream(byteArrayIn);
            image.Source = ImageSource.FromStream(() => new MemoryStream(byteArrayIn));
            return image.Source;
        }

        //public Image LoadImage(String base_64)
        //{
        //    byte[] bytes = Convert.FromBase64String(base_64);

        //    Image image;
        //    using (MemoryStream ms = new MemoryStream(bytes))
        //    {
        //        image = System.Drawing.FromStream(ms);
        //    }

        //    return image;
        //}
    }

    

    public class CardView
    {
        public ImageSource ItemImage { get; set; }
        public string itemName_En { get; set; }

    }
}