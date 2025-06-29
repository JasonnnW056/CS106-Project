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
using CS106_Project.Pages;

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

            new Connection();
            Connection.UsersCollection = Connection.DB.GetCollection<Users>("users");
            Connection.DoctorsCollection = Connection.DB.GetCollection<Doctors>("doctors");
            Connection.AppointmentsCollection = Connection.DB.GetCollection<AppointmentDetails>("appointments");
            MainFrame.Navigate(new Pages.LoginPage());
          
        }

    }
}