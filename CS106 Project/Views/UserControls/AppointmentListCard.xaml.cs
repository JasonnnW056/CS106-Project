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
using CS106_Project.Pages;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AppointmentListCard.xaml
    /// </summary>
    public partial class AppointmentListCard : UserControl
    {
        public EventHandler? Cancelled;
        public AppointmentPage? ParentPage { get; set; }
        public AppointmentListCard()
        {
            InitializeComponent();
            
        }

        //private void OnStackPanelMouseEnter(object sender, MouseEventArgs e)
        //{
        //    // Check if booking status is cancelled - if so, don't show cancel button
        //    if (BookingStatus.Content.ToString().ToUpper() != "CANCELLED")
        //    {
        //        CancelButtonBorder.Visibility = Visibility.Visible;
        //    }
        //}

        private void OnStackPanelMouseLeave(object sender, MouseEventArgs e)
        {
            // Only hide if it's currently visible
            if (CancelButtonBorder.Visibility == Visibility.Visible)
            {
                CancelButtonBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void OnStatusClicked(object sender, MouseButtonEventArgs e)
        {
            // Only allow click if not cancelled
            if (BookingStatus.Content.ToString().ToUpper() != "CANCELLED" && BookingStatus.Content.ToString() != "Status")
            {
                CancelButtonBorder.Visibility = Visibility.Visible;
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            BookingStatus.Content = "CANCELLED";
            CancelButtonBorder.Visibility = Visibility.Collapsed;

            ParentPage?.UpdateDatabase(sender, e);
        }
    }
}
