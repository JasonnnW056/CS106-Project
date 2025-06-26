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
using MongoDB.Bson;
using MongoDB.Driver;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BookingTimePicker.xaml
    /// </summary>
    public partial class BookingTimePicker : UserControl
    {
        
        public BookingTimePicker()
        {
            InitializeComponent();

            BlackOutDates();

            BookingContext.DoctorChanged += OnDoctorChanged;

        }
        private void OnDoctorChanged(object? sender, Doctors doctor)
        {
            AddTimeOption(doctor);
        }


        private async void AddTimeOption(Doctors d)
        {
            string startTimeText = d.Availability.StartTime;
            string endTimeText = d.Availability.EndTime;

            TimeSpan startTime = TimeSpan.Parse(startTimeText);
            TimeSpan endTime = TimeSpan.Parse(endTimeText);

            while (startTime < endTime)
            {
                TimePicker.Items.Add(startTime.ToString(@"hh\:mm"));

                // Add 30-minute interval
                startTime = startTime.Add(TimeSpan.FromMinutes(30));
            }

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

        public DateTime? CombineDate()
        {
            if (DateBox.SelectedDate.HasValue && TimePicker.SelectedItem != null)
            {
                string? selectedItem = TimePicker.SelectedItem?.ToString();
                if (selectedItem != null)
                {
                    string hourText = selectedItem.Split(":")[0];
                    string minuteText = selectedItem.Split(":")[1];
                    int hour = int.Parse(hourText);
                    int minute = int.Parse(minuteText);
                    
                    DateTime date = DateBox.SelectedDate.Value;

                    DateTime combinedDateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);

                    return combinedDateTime;
                }

                
            }
            return null;
        }
    }
}
