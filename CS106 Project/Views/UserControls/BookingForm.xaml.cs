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
using CS106_Project.Models;
using CS106_Project.Classes;
using System.Runtime.InteropServices;
using CS106_Project.Pages;

namespace CS106_Project.Views.UserControls
{
    /// <summary>  
    /// Interaction logic for BookingForm.xaml  
    /// </summary>  
    public partial class BookingForm : UserControl
    {
        public event EventHandler<UserBookingData>? DataSent;

        public BookingForm()
        {
            InitializeComponent();
        }

        private void OnBooking(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text.Trim();
            string lastName = LastNameBox.Text.Trim();
            string phoneNumber = PhoneNumberBox.Text.Trim();
            string email = EmailBox.Text.Trim();
            string type = TypeBox.Text;

            bool AnyError = false;

            if (!Validations.NameValidation(firstName))
            {
                FirstNameError.Visibility = Visibility.Visible;
                FirstNameError.Content = "Invalid name format!";
                AnyError = true;
            }
            else
            {
                FirstNameError.Visibility = Visibility.Collapsed;
            }

            if (!Validations.NameValidation(lastName))
            {
                LastNameError.Visibility = Visibility.Visible;
                LastNameError.Content = "Invalid name format!";
                AnyError = true;
            }
            else
            {
                LastNameError.Visibility = Visibility.Collapsed;
            }

            if (!Validations.PhoneNumberValidation(phoneNumber))
            {
                PhoneNumberError.Visibility = Visibility.Visible;
                PhoneNumberError.Content = "Invalid phone number format!";
                AnyError = true;
            }
            else
            {
                PhoneNumberError.Visibility = Visibility.Collapsed;
            }

            if (!Validations.EmailValidation(email)) 
            {
                EmailError.Visibility = Visibility.Visible;
                EmailError.Content = "Invalid email format!";
                AnyError = true;
            }
            else
            {
                EmailError.Visibility = Visibility.Collapsed;
            }

            if (TypeBox.SelectedItem == null) 
            {
                TypeError.Visibility = Visibility.Visible;
                TypeError.Content = "Please choose the type of appointment!";
                AnyError = true;
            }
            else
            {
                TypeError.Visibility = Visibility.Collapsed;
            }


            DateTime appointmentDate = DateTime.MinValue;
            DateTime? appointmentDateNullable = TimeBox.CombineDate();
            if (appointmentDateNullable != null)
            {
                appointmentDate = appointmentDateNullable.Value;
                TimeError.Visibility = Visibility.Collapsed;
            }
            else
            {
                TimeError.Visibility = Visibility.Visible;
                TimeError.Content = "Please choose the appointment date and time!";
                AnyError = true;

            }

            // Might be empty  
            string illnessDescription = IllnessBox.Text;

            if (AnyError)
            {
                return;
            }

            var UserData = new UserBookingData(firstName, lastName, phoneNumber, email, type, appointmentDate, illnessDescription);

            DataSent?.Invoke(this, UserData);

            //Move to Appointments Page
            var Navigation = NavigationService.GetNavigationService(this);
            var AppointmentsPage = new AppointmentPage();

            Navigation.Navigate(AppointmentsPage);
        }
    }
}
