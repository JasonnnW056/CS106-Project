using System;
using System.Collections.Generic;
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
using MongoDB.Driver;

namespace CS106_Project.Pages
{
    /// <summary>  
    /// Interaction logic for BookingPage.xaml  
    /// </summary>  
    public partial class BookingPage : Page
    {
        private Doctors DoctorData;
        private readonly IMongoCollection<UserBookingData> Collection;

        private string DoctorName = string.Empty;
        private string DoctorSpecialty = string.Empty;

        public BookingPage(Doctors doctorData)
        { 
            InitializeComponent();

            Collection = Connection.DB.GetCollection<UserBookingData>("appointments");
            var Filter = Builders<UserBookingData>.Filter.Empty;
            var Result = Collection.Find(Filter).ToList();

            if (Result.Count() > 0)
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Nah");
            }

           
            DoctorData = doctorData;
            LoadBookedDoctorData();

            BookingFormControl.DataSent += InsertDBBookingInformation;

        }

        private void LoadBookedDoctorData()
        {
            DoctorName = DoctorData.Name;
            DoctorSpecialty = DoctorData.Specialty;
            MessageBox.Show(DoctorName);
        }

        private void InsertDBBookingInformation(object? sender, UserBookingData e)
        {
            Collection.InsertOne(e);
        }
    }
}
