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
using CS106_Project.Views.UserControls;
using MongoDB.Driver;

namespace CS106_Project.Pages
{
    public partial class AppointmentPage : Page
    {
        public AppointmentPage()
        {
            InitializeComponent();

            string? objectId = LoginManager.UserID;

            
            var Filter = Builders<AppointmentDetails>.Filter.Eq(a => a.UserId, objectId);
            var Result = Connection.AppointmentsCollection.Find(Filter).ToList();


            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }


            var FirstTable = new AppointmentListCard();
            var FirstTableContent = new { Date = "Date", DoctorName = "Doctor Name", Type = "Type", Status = "Status" };

            FirstTable.DataContext = FirstTableContent;

            AppointmentContainer.Children.Add(FirstTable);

            foreach (var item in Result)
            {
                var Appointment = new AppointmentListCard();
                Appointment.ParentPage = this;

                var Date = item.AppointmentDate.ToLocalTime();

                var AppointmentCard = new AppointmentCard(item.Id ,Date, item.DoctorDetails.Name, item.TypeOfAppointment, item.Status);
                Appointment.DataContext = AppointmentCard;

                AppointmentContainer.Children.Add(Appointment);
            }

        }

        public void UpdateDatabase(object sender, RoutedEventArgs e)
        {
            Button? CancelButton = sender as Button;
            
            if (CancelButton != null)
            {
                AppointmentCard? CancelledAppointment = CancelButton.DataContext as AppointmentCard;

                var AppointmentId = CancelledAppointment?.Id.ToString();

                var IdFilter = Builders<AppointmentDetails>.Filter.Eq(a=>a.Id, AppointmentId);
               
                var Update = Builders<AppointmentDetails>.Update.Set(a=>a.Status, "CANCELLED");
                var UpdateResult = Connection.AppointmentsCollection.UpdateOne(IdFilter, Update);

                
            }
        }
    }
}
