using System.Text;
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
using CS106_Project.Views.UserControls;
using CS106_Project.Models;
using MongoDB.Driver;

namespace CS106_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.DoctorList());
            //new Connection();

            //var Collection = Connection.DB.GetCollection<Doctors>("doctors");

            //var Filter = Builders<Doctors>.Filter.Empty;

            //var Result = Collection.Find(Filter).ToList();

            //if (Result.Count() > 0)
            //{
            //    MessageBox.Show("Connected");
            //}

            //foreach (var item in Result)
            //{
            //    Card DoctorCard = new Card();
            //    var doctor = new Doctors(item.Name, item.Specialty, item.Availability);

            //    DoctorCard.DataContext = doctor;
            //    CardWrapper.Children.Add(DoctorCard);
            //}
        }
    }
}