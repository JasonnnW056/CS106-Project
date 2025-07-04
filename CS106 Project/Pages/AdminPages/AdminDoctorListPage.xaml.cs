﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MongoDB.Bson;

namespace CS106_Project.Pages.AdminPages
{
    /// <summary>  
    /// Interaction logic for AdminDoctorListPage.xaml  
    /// </summary>  
    public partial class AdminDoctorListPage : Page 
    {
        public ObservableCollection<Doctors>? DoctorsCollection { get; set; }

        public AdminDoctorListPage()
        {
            InitializeComponent();

            var Filter = Builders<Doctors>.Filter.Empty;
            var Result = Connection.DoctorsCollection.Find(Filter).ToList();

            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            DoctorsCollection = new ObservableCollection<Doctors>(Result);
            DoctorContainer.ItemsSource = DoctorsCollection;
        }

        public AdminDoctorListPage(string UserInput)
        {
            InitializeComponent();
           
            var Filter = Builders<Doctors>.Filter.Regex(d => d.Name, new BsonRegularExpression(UserInput, "i"));
            var Result = Connection.DoctorsCollection.Find(Filter).ToList();

            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            DoctorsCollection = new ObservableCollection<Doctors>(Result);
            DoctorContainer.ItemsSource = DoctorsCollection;
        }

        

        private void OnDeleteDoctors(object sender, RoutedEventArgs e)
        {
            Button? Button = sender as Button;

            if (Button != null)
            {
                Doctors? doctor = Button?.DataContext as Doctors;
                if (doctor != null)
                {
                    var Filter = Builders<Doctors>.Filter.Eq(d => d.Id, doctor?.Id);

                    var Result = Connection.DoctorsCollection?.DeleteOne(Filter);
                    if (Result?.DeletedCount > 0)
                    {
                        MessageBox.Show("Deleted");
                    }
                }
            }
        }

        private void OnClickAddDoctor(object sender, RoutedEventArgs e)
        {
            var Navigation = NavigationService.GetNavigationService(this);
            var Page = new AdminAddNewDoctor();

            NavigationService.Navigate(Page);
        }
    }
}
