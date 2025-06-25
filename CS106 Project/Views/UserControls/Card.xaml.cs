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
using CS106_Project.Pages;

namespace CS106_Project.Views.UserControls
{
    
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        private void OnBook(object sender, RoutedEventArgs e)
        {
            var card = sender as FrameworkElement;
            var cardData = card?.DataContext as Doctors;

            if (cardData != null)
            {
                var bookingPage = new BookingPage(cardData);

                var navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(bookingPage);
                    
                }
                
                
            }
        }
    }
}
