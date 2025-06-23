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
using System.Windows.Shapes;
using CS106_Project.Classes;
using CS106_Project.Models;
using MongoDB.Driver;

namespace CS106_Project.Views.Windows
{
    /// <summary>
    /// Interaction logic for RescheduleDialog.xaml
    /// </summary>
    public partial class RescheduleDialog : Window
    {
        public DateTime? SelectedDateTime { get; private set; }
        private readonly Availability _availability;
        private readonly AppointmentCard _appointment;

        public RescheduleDialog(Availability availability, AppointmentCard appointment)
        {
            InitializeComponent();
            _availability = availability;
            _appointment = appointment;
            DateBox.SelectedDate = DateTime.Today;
            LoadAvailableTimes();
        }

        private void DateBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAvailableTimes();
            UpdateSelectionSummary();
        }

        private void TimePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSelectionSummary();
        }

        private void LoadAvailableTimes()
        {
            TimePicker.Items.Clear();

            if (!DateBox.SelectedDate.HasValue || _availability == null)
                return;

            try
            {
                string startTimeText = _availability.StartTime;
                string endTimeText = _availability.EndTime;

                TimeSpan startTime = TimeSpan.Parse(startTimeText);
                TimeSpan endTime = TimeSpan.Parse(endTimeText);

                while (startTime < endTime)
                {
                    TimePicker.Items.Add(startTime.ToString(@"hh\:mm"));
                    startTime = startTime.Add(TimeSpan.FromMinutes(30));
                }

                if (TimePicker.Items.Count > 0)
                {
                    TimePicker.SelectedIndex = 0;
                }

                BlackOutDates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading available times!");
            }

            ValidateSelection();
        }
        private void BlackOutDates()
        {
            DateBox.DisplayDateStart = DateTime.Today;
            DateBox.DisplayDateEnd = DateTime.Today.AddDays(60);

            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today.AddDays(60);

            for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    DateBox.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }
        private void UpdateSelectionSummary()
        {
            if (DateBox.SelectedDate.HasValue && TimePicker.SelectedItem != null)
            {
                DateTime selectedDate = DateBox.SelectedDate.Value;
                string? selectedTime = TimePicker.SelectedItem.ToString();
                SelectionSummary.Text = $"Appointment rescheduled for:\n{selectedDate:dddd, MMMM dd, yyyy} at {selectedTime}";
            }
            else
            {
                SelectionSummary.Text = "Please select both date and time to continue";
            }

            ValidateSelection();
        }

        private void ValidateSelection()
        {
            OkButton.IsEnabled = DateBox.SelectedDate.HasValue && TimePicker.SelectedItem != null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedDateTime = CombineDate();
            if (SelectedDateTime.HasValue)
            {
                UpdateAppointmentDate(SelectedDateTime);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select both date and time.", "Invalid Selection");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public DateTime? CombineDate()
        {
            if (DateBox.SelectedDate.HasValue && TimePicker.SelectedItem != null)
            {
                string? selectedItem = TimePicker.SelectedItem?.ToString();
                if (selectedItem != null)
                {
                    string hourText = selectedItem.Split(':')[0];
                    string minuteText = selectedItem.Split(':')[1];
                    int hour = int.Parse(hourText);
                    int minute = int.Parse(minuteText);

                    DateTime date = DateBox.SelectedDate.Value;
                    DateTime combinedDateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                    return combinedDateTime;
                }
            }
            return null;
        }

        public void UpdateAppointmentDate(DateTime? NewAppointmentDate)
        {
            new Connection();
            var Collection = Connection.DB.GetCollection<AppointmentDetails>("appointments");
            var Filter = Builders<AppointmentDetails>.Filter.Eq(a=>a.Id, _appointment.Id);

            var UpdateBuilder = Builders<AppointmentDetails>.Update.Set(a => a.AppointmentDate, NewAppointmentDate);
            var UpdateResult = Collection.UpdateOne(Filter, UpdateBuilder);

            if (UpdateResult.ModifiedCount != 0) {
                MessageBox.Show("Updated Appointment Date Successfully!");
            }
        }
    }
}

// Example Service Methods (you'll need to implement these based on your MongoDB setup)
/*
namespace CS106_Project.Services
{
    public class AppointmentService
    {
        public static async Task UpdateAppointmentDateAsync(string appointmentId, DateTime newDate)
        {
            // MongoDB update logic here
            // Update both AppointmentCard and AppointmentDetails if needed
        }

        public static async Task UpdateAppointmentStatusAsync(string appointmentId, string status)
        {
            // MongoDB update logic here
        }
    }

    public class DoctorService
    {
        public async Task<Doctors> GetDoctorByNameAsync(string doctorName)
        {
            // MongoDB query to get doctor by name
            // Return the Doctors object with Availability
        }
    }
}
*/