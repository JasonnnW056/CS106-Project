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
using CS106_Project.Pages;
using CS106_Project.Views.Windows;
using MongoDB.Driver;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AppointmentListCard.xaml
    /// </summary>
    public partial class AppointmentListCard : UserControl
    {
        public EventHandler? Cancelled;
        public AppointmentPage? ParentPage { get; set; }
        public AppointmentListCard()
        {
            InitializeComponent();
        }

        // New method for date stack panel mouse enter
        private void OnDateStackPanelMouseEnter(object sender, MouseEventArgs e)
        {
            // Only show edit icon if status is not cancelled
            if (BookingStatus.Content.ToString().ToUpper() != "CANCELLED" &&
                BookingStatus.Content.ToString() != "Status")
            {
                EditIconBorder.Visibility = Visibility.Visible;
            }
        }

        // New method for date stack panel mouse leave
        private void OnDateStackPanelMouseLeave(object sender, MouseEventArgs e)
        {
            EditIconBorder.Visibility = Visibility.Collapsed;
        }

        // New method for edit icon click
        private void OnEditIconClicked(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is AppointmentCard appointment)
            {
                RescheduleAppointment(appointment);
            }
        }

        private void OnStackPanelMouseLeave(object sender, MouseEventArgs e)
        {
            // Only hide if it's currently visible
            if (CancelButtonBorder.Visibility == Visibility.Visible)
            {
                CancelButtonBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void OnStatusClicked(object sender, MouseButtonEventArgs e)
        {
            // Only allow click if not cancelled
            if (BookingStatus.Content.ToString() != "CANCELLED" && BookingStatus.Content.ToString() != "Status")
            {
                CancelButtonBorder.Visibility = Visibility.Visible;
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to cancel the appointment?",
                    "Cancel Appointment",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {

                MessageBox.Show("Appointment cancelled successfully.", "Success",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            BookingStatus.Content = "CANCELLED";
            CancelButtonBorder.Visibility = Visibility.Collapsed;

            ParentPage?.UpdateDatabase(sender, e);
        }

        private async void RescheduleAppointment(AppointmentCard appointment)
        {
            try
            {
                // Get doctor's availability from database
                // Assuming you have a service to get doctor details
                var doctorAvailability = await GetDoctorAvailability(appointment.DoctorName);

                if (doctorAvailability == null)
                {
                    MessageBox.Show("Could not load doctor's availability. Please try again.", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                RescheduleDialog dialog = new RescheduleDialog(doctorAvailability, appointment)
                {
                    Owner = Window.GetWindow(this)
                };

                if (dialog.ShowDialog() == true && dialog.SelectedDateTime.HasValue)
                {
                    DateTime oldDate = appointment.Date;
                    DateTime newDate = dialog.SelectedDateTime.Value;

                    appointment.Date = newDate;
                    DateText.Content = dialog.SelectedDateTime.Value;


                    MessageBox.Show(
                        $"Appointment with {appointment.DoctorName} rescheduled from {oldDate:yyyy-MM-dd HH:mm} to {newDate:yyyy-MM-dd HH:mm}",
                        "Appointment Rescheduled",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rescheduling appointment: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<Availability> GetDoctorAvailability(string doctorName)
        {
            new Connection();
            var Collection = Connection.DB.GetCollection<Doctors>("doctors");
            var Filter = Builders<Doctors>.Filter.Eq(d => d.Name, doctorName);

            var Result = Collection.Find(Filter).ToList();
            if (Result.Any())
            {
                foreach (var Doctor in Result)
                {
                    return new Availability
                    {
                        StartTime = Doctor.Availability.StartTime,
                        EndTime = Doctor.Availability.EndTime
                    };
                }
            }
            else
            {
                MessageBox.Show("Doctor Not Found!");
            }

            //If nothing found
            return new Availability
            {
                StartTime = "09:00",
                EndTime = "17:00"
            };
        }
    }
}
    
