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

        
            DateTime bookingDate = DateTime.MinValue;
            DateTime? bookingDateNullable = TimeBox.CombineDate();

            if (!Validations.NameValidation(firstName))
            {
                FirstNameBox.Visibility = Visibility.Visible;
                FirstNameBox.Text = "Invalid name";
            }
            else
            {
                FirstNameBox.Visibility = Visibility.Collapsed;
            }

            if (!Validations.NameValidation(lastName))
            {
                return;
            }
            if (!Validations.PhoneNumberValidation(phoneNumber))
            {
                return;
            }

            if (!Validations.EmailValidation(email)) { return; }

            
           
            if (bookingDateNullable != null)
            {
                bookingDate = bookingDateNullable.Value;
            }
            else
            {
                MessageBox.Show("Date is null");
                return; // Exit the method if bookingDate is invalid  
                //change into bool checking for error
            }

            // Might be empty  
            string illnessDescription = IllnessBox.Text;

            var UserData = new UserBookingData(firstName, lastName, phoneNumber, email, type, bookingDate, illnessDescription);

            MessageBox.Show(UserData.Date.ToString());

            DataSent?.Invoke(this, UserData);
        }
    }
}
