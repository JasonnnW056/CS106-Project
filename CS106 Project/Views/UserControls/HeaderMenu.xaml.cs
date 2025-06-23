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
                OurDoctorsButton.IsEnabled = true;
                AppointmentsButton.IsEnabled = true;
                SearchButton.IsEnabled = true;
            }
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
    }
}
