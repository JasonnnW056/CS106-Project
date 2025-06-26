using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private Doctors? DoctorData;

        private string DoctorName = string.Empty;
        private string DoctorSpecialty = string.Empty;

        public BookingPage(Doctors doctorData)
        { 
            InitializeComponent();

            DoctorData = doctorData;
            BookingContext.CurrentDoctor = DoctorData;

           
            var Filter = Builders<AppointmentDetails>.Filter.Empty;
            var Result = Connection.AppointmentsCollection.Find(Filter).ToList();


            LoadBookedDoctorData();

            BookingFormControl.DataSent += InsertDBBookingInformation;

        }

        private void LoadBookedDoctorData()
        {
            if (DoctorData != null)
            {
                DoctorName = DoctorData.Name;
                DoctorSpecialty = DoctorData.Specialty;
            }
        }
        private void InsertDBBookingInformation(object? sender, UserBookingData e)
        {
            if (LoginManager.UserID != null)
            {
                Connection.AppointmentsCollection.InsertOne(

                    new AppointmentDetails(LoginManager.UserID, e.FirstName, e.LastName, e.PhoneNumber,
                                            e.Email, DoctorName, DoctorSpecialty, e.Type, e.Date, e.IllnessDescription)

                );
            }
        }
    }
}
