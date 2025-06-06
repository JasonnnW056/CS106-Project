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

            new Connection();

            var Collection = Connection.DB.GetCollection<Doctors>("doctors");

            AddTimeOption();
            BlackOutDates();
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

        private void AddTimeOption()
        {
            //string StartTimeText = "11:00";
            //string EndTimeText = "15:30";

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

            string startTimeText = "06:00";
            string endTimeText = "15:30";

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
    }
}
