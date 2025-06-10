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
        //private Doctors? Doctor;

        /*private string? startTimeText = DoctorData.StartingTime;
        private string? endTimeText = DoctorData.EndingTime;*/

        //public Doctors? DoctorsData
        //{
        //    get => Doctor;

        //    set
        //    {
        //        Doctor = value;
        //        if (Doctor != null)
        //        {
        //            SetDoctorName(Doctor);
        //        }
        //    }
        //}


        public BookingTimePicker()
        {
            //SetDoctorName();

            InitializeComponent();

            /*AddTimeOption();*/

            BlackOutDates();

            BookingContext.DoctorChanged += OnDoctorChanged;

        }
        private void OnDoctorChanged(object? sender, Doctors doctor)
        {
            AddTimeOption(doctor);
        }

        //private void OnDatePickerClick(object sender, MouseButtonEventArgs e)
        //{
        //    var DatePicker = sender as DatePicker;
        //    if(DatePicker != null && !DatePicker.IsDropDownOpen)
        //    {

        //        DatePicker.IsDropDownOpen = true;
        //        e.Handled = true;
        //    }
        //}

        //private void SetDoctorName()
        //{
        //    if (DoctorData.CurrentDoctor != null)
        //    {
        //        startTimeText = DoctorData.CurrentDoctor.Availability.StartTime;
        //        endTimeText = DoctorData.CurrentDoctor.Availability.EndTime;
        //    }
        //}

        private async void AddTimeOption(Doctors d)
        {
            string startTimeText = d.Availability.StartTime;
            string endTimeText = d.Availability.EndTime;

            MessageBox.Show(startTimeText);
            MessageBox.Show(endTimeText);

            //string StartHourText = StartTimeText.Split(':')[0];
            //string StartMinuteText = StartTimeText.Split(":")[1];

            //string EndHourText = EndTimeText.Split(':')[0];
            //string EndMinuteText = EndTimeText.Split(":")[1];

            //int StartHour = int.Parse(StartHourText);
            //int StartMinute = int.Parse(StartMinuteText);

            //int EndHour = int.Parse(EndHourText);


            //string AvailableTime = "";
            //while(StartHour <= EndHour)
            //{

            //    if ( StartMinute == 0) {
            //        AvailableTime = StartHour.ToString("D2") + ":00";

            //        TimePicker.Items.Add(AvailableTime);

            //        StartMinute = 30;
            //    }
            //    else
            //    {
            //        AvailableTime = StartHour.ToString("D2") + ":30";

            //        TimePicker.Items.Add(AvailableTime);

            //        StartHour += 1;
            //        StartMinute = 0;
            //    }

            //    if (AvailableTime == EndTimeText)
            //    {
            //        return;
            //    }
            //}



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
