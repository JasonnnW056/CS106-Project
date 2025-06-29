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

namespace CS106_Project.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AdminAddNewDoctor.xaml
    /// </summary>
    public partial class AdminAddNewDoctor : Page
    {
        public AdminAddNewDoctor()
        {
            InitializeComponent();
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            string Name = DoctorNameBox.Text;
            string Specialty = string.Empty;
            if (OurSpecialtiesComboBox.SelectedItem is ComboBoxItem selectedItem)
            {


                string? specialty = selectedItem.Content?.ToString();
                if (specialty != null)
                {
                    Specialty = specialty;

                }
            }
            string StartTime = StartTimeBox.Text;
            string EndTime = EndTimeBox.Text;

            if (!Validations.TimeFormatValidation(StartTime))
            {

                ErrorMessage.Content = "Invalid Start Time Format!";
                ErrorMessage.Visibility = Visibility.Visible;
                return;

            }
            else
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            }
            
            if (!Validations.TimeFormatValidation(EndTime))
            {
                ErrorMessage.Content = "Invalid End Time Format!";
                ErrorMessage.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Collapsed;
            }

            Connection.DoctorsCollection.InsertOne(new Doctors(Name, Specialty, StartTime, EndTime));
           
            MessageBox.Show("Doctor Added Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information); 
            
            MoveToAdminDoctorList();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            MoveToAdminDoctorList();
        }

        private void MoveToAdminDoctorList()
        {
            var Navigation = NavigationService.GetNavigationService(this);
            var Page = new AdminDoctorListPage();

            NavigationService.Navigate(Page);
        }
    }
}
