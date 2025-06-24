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

            //int[] allowedHours = { 9, 11, 13, 15, 17 }; // 24-hour format
            //foreach (int hour in allowedHours)
            //{
            //    HourPicker.Items.Add(hour.ToString("D2") + ":00");
            //}

            /*MainFrame.Navigate(new Pages.AdminPages.AdminUserListPage());*/
            MainFrame.Navigate(new Pages.LoginPage());
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
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // Only allow booking at specific hours
        //    int[] allowedHours = { 9, 11, 13, 15, 17 }; // 24-hour format
        //    foreach (int hour in allowedHours)
        //    {
        //        HourPicker.Items.Add(hour.ToString("D2") + ":00");
        //    }
        //}

        //private void Submit_Click(object sender, RoutedEventArgs e)
        //{
        //    if (BookingDatePicker.SelectedDate.HasValue && HourPicker.SelectedItem != null)
        //    {
        //        DateTime date = BookingDatePicker.SelectedDate.Value;
        //        string hourText = HourPicker.SelectedItem.ToString().Split(':')[0];
        //        int hour = int.Parse(hourText);

        //        DateTime bookingTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
        //        MessageBox.Show($"Booking confirmed at: {bookingTime}");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select both date and hour.");
        //    }
        //}
    }
}