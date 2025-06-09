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
    /// Interaction logic for DoctorList.xaml
    /// </summary>
    public partial class DoctorList : Page
    {
        public DoctorList()
        {
            InitializeComponent();


            new Connection();

            var Collection = Connection.DB.GetCollection<Doctors>("doctors");

            var Filter = Builders<Doctors>.Filter.Empty;

            var Result = Collection.Find(Filter).ToList();

            if (Result.Count() > 0)
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Nah");
            }
                
            foreach (var item in Result)
            {
                Card DoctorCard = new Card();
                var doctor = new Doctors(item.Name, item.Specialty, item.Availability.StartTime, item.Availability.EndTime);

                DoctorCard.DataContext = doctor;
                DoctorCard.Margin = new Thickness(20);
                CardWrapper.Children.Add(DoctorCard);
            }
        }
    }
}
