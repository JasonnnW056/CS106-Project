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
using CS106_Project.Views.UserControls;

namespace CS106_Project.Pages
{
    /// <summary>  
    /// Interaction logic for LoginPage.xaml  
    /// </summary>  
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();

            
            LoginSignUpForm.SwitchToLoginOccurred += OnSwitchToLogin;
            LoginSignUpForm.SwitchToSignupOccurred += OnSwitchToSignup;

            OnSwitchToLogin(null, EventArgs.Empty);
        }

        public void OnSwitchToLogin(object? sender, EventArgs e)
        {
            SignUpText.Visibility = Visibility.Collapsed;
            LoginText.Visibility = Visibility.Visible;
            
        }
        private void OnSwitchToSignup(object? sender, EventArgs e)
        {
            LoginText.Visibility = Visibility.Collapsed;
            SignUpText.Visibility = Visibility.Visible;
        }

    }
}
