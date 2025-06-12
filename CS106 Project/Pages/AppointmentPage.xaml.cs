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
    /// <summary>
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        public AppointmentPage()
        {
            InitializeComponent();

            new Connection();

            string objectId = "6847c69a6753c87133539397";

            var Collection = Connection.DB.GetCollection<AppointmentDetails>("appointments");
            var Filter = Builders<AppointmentDetails>.Filter.Eq(a => a.UserId,objectId);
            var Result = Collection.Find(Filter).ToList();

            
            if (Result.Count() > 0)
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Nah");
            }

            foreach (var item in Result) { 
                var Appointment = new AppointmentListCard();

                var AppointmentCard = new AppointmentCard(item.AppointmentDate, item.DoctorDetails.Name, item.TypeOfAppointment, item.Status);
                Appointment.DataContext = AppointmentCard;

                AppointmentContainer.Children.Add(Appointment);
            }
           

            


        }
    }
}
