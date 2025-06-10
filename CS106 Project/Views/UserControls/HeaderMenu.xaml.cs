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
    }
}
