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
using CS106_Project.Pages;
using CS106_Project.Views.Windows;

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

        //private void OnStackPanelMouseEnter(object sender, MouseEventArgs e)
        //{
        //    // Check if booking status is cancelled - if so, don't show cancel button
        //    if (BookingStatus.Content.ToString().ToUpper() != "CANCELLED")
        //    {
        //        CancelButtonBorder.Visibility = Visibility.Visible;
        //    }
        //}

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
            if (BookingStatus.Content.ToString().ToUpper() != "CANCELLED" && BookingStatus.Content.ToString() != "Status")
            {
                CancelButtonBorder.Visibility = Visibility.Visible;
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            BookingStatus.Content = "CANCELLED";
            CancelButtonBorder.Visibility = Visibility.Collapsed;

            ParentPage?.UpdateDatabase(sender, e);
        }
        private void OnDateClicked(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is AppointmentCard appointment)
            {
                RescheduleAppointment(appointment);
            }
        }

        /*private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppointmentCard appointment)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Are you sure you want to cancel the appointment with {appointment.DoctorName} on {appointment.Date:yyyy-MM-dd HH:mm}?",
                    "Cancel Appointment",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    appointment.Status = "Cancelled";
                    // Update in database
                    // await AppointmentService.UpdateAppointmentStatusAsync(appointment.Id, "Cancelled");
                    MessageBox.Show("Appointment cancelled successfully.", "Success",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            CancelButtonBorder.Visibility = Visibility.Collapsed;
            RescheduleButtonBorder.Visibility = Visibility.Collapsed;
        }*/

        private void OnRescheduleButtonClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is AppointmentCard appointment)
            {
                RescheduleAppointment(appointment);
            }

            CancelButtonBorder.Visibility = Visibility.Collapsed;
            RescheduleButtonBorder.Visibility = Visibility.Collapsed;
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

                RescheduleDialog dialog = new RescheduleDialog(doctorAvailability)
                {
                    Owner = Window.GetWindow(this)
                };

                if (dialog.ShowDialog() == true && dialog.SelectedDateTime.HasValue)
                {
                    DateTime oldDate = appointment.Date;
                    DateTime newDate = dialog.SelectedDateTime.Value;

                    appointment.Date = newDate;

                    // Update in database
                    // await AppointmentService.UpdateAppointmentDateAsync(appointment.Id, newDate);

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

        // Method to get doctor availability - you'll need to implement this based on your service layer
        private async System.Threading.Tasks.Task<Availability> GetDoctorAvailability(string doctorName)
        {
            // This should connect to your MongoDB and get the doctor's availability
            // Example implementation:
            /*
            var doctorService = new DoctorService();
            var doctor = await doctorService.GetDoctorByNameAsync(doctorName);
            return doctor?.Availability;
            */

            // For now, return a default availability (you should replace this)
            return new Availability
            {
                StartTime = "09:00",
                EndTime = "17:00"
            };
        }
    }
}
    
