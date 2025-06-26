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
using CS106_Project.Pages;
using CS106_Project.Pages.AdminPages;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for HeaderMenu.xaml
    /// </summary>
    public partial class HeaderMenu : UserControl
    {
        public HeaderMenu()
        {
            InitializeComponent();
            UpdateUI();
           
        }

        public void UpdateUI()
        {
            if (LoginManager.IsLoggedIn)
            { 
                LoginButton.Content = LoginManager.CurrentUser;
                HomeButton.IsEnabled = true;
                AppointmentsButton.IsEnabled = true;
                SearchIconBorder.IsEnabled = true;

                if (LoginManager.IsAdmin == false)
                {
                    //User Login
                    OurSpecialtiesComboBox.IsEnabled = true;
                    
                }
                else
                {
                    //Admin Login
                    HomeButton.Content = "User List";
                    AppointmentsButton.Content = "Doctor List";
                    OurSpecialtiesComboBox.Visibility = Visibility.Collapsed;

                    HomeButton.Click -= OnHomeButtonClick;
                    HomeButton.Click += OnUserListClick;

                    AppointmentsButton.Click -= OnAppointmentClick;
                    AppointmentsButton.Click += OnDoctorListClick;

                }
               
            }
        }

        //User Functionality
        private void OnHomeButtonClick(object sender, RoutedEventArgs e)
        {
            var DoctorPage = new DoctorList();

            var Navigation = NavigationService.GetNavigationService(this);
            Navigation.Navigate(DoctorPage);
        }
        public void OnSearchBarClicked(object sender, MouseButtonEventArgs e)
        {
            if (!SearchBar.IsVisible) {
                SearchBar.Visibility = Visibility.Visible;
            }
            else
            {
                SearchBar.Visibility -= Visibility.Collapsed;
            }
            
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            if (LoginManager.IsLoggedIn)
            {
                var ProfilePage = new ProfilePage();
                var Navigation = NavigationService.GetNavigationService(this);
                Navigation.Navigate(ProfilePage);
            }
        }

        private void OnAppointmentClick(object sender, RoutedEventArgs e)
        {
            var AppointmentPage = new AppointmentPage();
            var Navigation = NavigationService.GetNavigationService(this);
            Navigation.Navigate(AppointmentPage);
        }

        private void OnSelectSpecialty(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    OurSpecialtiesComboBox.SelectedIndex = 0;
                        
                    string? specialty = selectedItem.Content?.ToString();
                    if (specialty != null && specialty != "Our Doctors' Specialties")
                    {
                        
                        var DoctorPage = new DoctorList("specialty",specialty);

                        var Navigation = NavigationService.GetNavigationService(this);
                        Navigation.Navigate(DoctorPage);
                    }
                }
            }
        }

        //Admin Functionality
        private void OnUserListClick(object sender, RoutedEventArgs e)
        {
            var UserListPage = new AdminUserListPage();

            var Navigation = NavigationService.GetNavigationService(this);
            Navigation.Navigate(UserListPage);
        }
        private void OnDoctorListClick(object sender, RoutedEventArgs e)
        {
            var DoctorListPage = new AdminDoctorListPage();

            var Navigation = NavigationService.GetNavigationService(this);
            Navigation.Navigate(DoctorListPage);
        }
    }
}
