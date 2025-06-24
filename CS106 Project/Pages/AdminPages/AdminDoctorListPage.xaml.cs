using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CS106_Project.Classes;
using CS106_Project.Models;
using CS106_Project.Views.UserControls;
using MongoDB.Driver;

namespace CS106_Project.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AdminDoctorListPage.xaml
    /// </summary>
    public partial class AdminDoctorListPage : Page
    {
        public IMongoCollection<Doctors> Collection;
        public ObservableCollection<Doctors> DoctorsCollection { get; set; }

        public AdminDoctorListPage()
        {
            InitializeComponent();
            new Connection();

            string objectId = "6847c69a6753c87133539397";

            Collection = Connection.DB.GetCollection<Doctors>("doctors");
            var Filter = Builders<Doctors>.Filter.Empty;
            var Result = Collection.Find(Filter).ToList();


            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            DoctorsCollection = new ObservableCollection<Doctors>(Result);
            DoctorContainer.ItemsSource = DoctorsCollection;
            
        }

        private void OnDeleteDoctor(object sender, RoutedEventArgs e)
        {

        }
    }
}
