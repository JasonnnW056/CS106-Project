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
using MongoDB.Bson;
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

            var Filter = Builders<Doctors>.Filter.Empty;

            var Result = Connection.DoctorsCollection.Find(Filter).ToList();

            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            AddCard(Result);
           
        }

        public DoctorList(string category, string keyword)
        {
            InitializeComponent();

            //Default Value
            var Filter = Builders<Doctors>.Filter.Empty;

            if (category.ToLower() == "name")
            {
               Filter = Builders<Doctors>.Filter.Regex(d=>d.Name, new BsonRegularExpression(keyword, "i"));
            }

            if (category.ToLower() == "specialty")
            {
               Filter = Builders<Doctors>.Filter.Regex(d => d.Specialty, new BsonRegularExpression(keyword, "i"));
            }

            var Result = Connection.DoctorsCollection.Find(Filter).ToList();

            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            AddCard(Result);
        }

        public void AddCard(List<Doctors> Result)
        {
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
