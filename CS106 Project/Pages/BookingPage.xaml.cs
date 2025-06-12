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
        private readonly IMongoCollection<AppointmentDetails> Collection;

        private string DoctorName = string.Empty;
        private string DoctorSpecialty = string.Empty;

        //private string FirstName = string.Empty;
        //private string LastName = string.Empty;
        //private string PhoneNumber = string.Empty;
        //private string Email = string.Empty;
        //private string Type = string.Empty;
        //private DateTime AppointmentDate = DateTime.MinValue;
        //private string IllnessDescription = string.Empty;
        

        public BookingPage(Doctors doctorData)
        { 
            InitializeComponent();

            DoctorData = doctorData;
            BookingContext.CurrentDoctor = DoctorData;

            Collection = Connection.DB.GetCollection<AppointmentDetails>("appointments");
            var Filter = Builders<AppointmentDetails>.Filter.Empty;
            var Result = Collection.Find(Filter).ToList();

            if (Result.Count() > 0)
            {
                MessageBox.Show("Connected apointmentdetails");
            }
            else
            {
                MessageBox.Show("Nah");
            }




            LoadBookedDoctorData();

            BookingFormControl.DataSent += InsertDBBookingInformation;

        }

        private void LoadBookedDoctorData()
        {
            DoctorName = DoctorData.Name;
            DoctorSpecialty = DoctorData.Specialty;
            MessageBox.Show(DoctorName);
        }

        /*private void LoadPatientData(object? sender, UserBookingData e)
        {
            string FirstName = e.FirstName;
            string LastName = e.LastName;
            string PhoneNumber = e.PhoneNumber;
            string Email = e.Email;
            string Type = e.Type;
            DateTime AppointmentDate = e.Date;
            string IllnessDescription = e.IllnessDescription;
        }*/

        private void InsertDBBookingInformation(object? sender, UserBookingData e)
        {
            Collection.InsertOne(

                new AppointmentDetails(LoginManager.UserID ,e.FirstName, e.LastName, e.PhoneNumber, e.Email, DoctorName, DoctorSpecialty, e.Type, e.Date, e.IllnessDescription)

            );
        }
    }
}
