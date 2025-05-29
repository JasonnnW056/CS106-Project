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
using DnsClient;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LoginAndSignUp.xaml
    /// </summary>
    public partial class LoginAndSignUp : UserControl
    {
        private LoginForm Login = new LoginForm();
        private SignUpForm SignUp = new SignUpForm();
        public LoginAndSignUp()
        {
            InitializeComponent();

            //Adding events
            Login.SwitchToSignup += (s, e) => ShowSignup();
            SignUp.SwitchToLogin += (s, e) => ShowLogin();

            ShowLogin();
        }
        private void ShowLogin()
        {
            ContentHolder.Content = Login;
        }

        private void ShowSignup()
        {
            ContentHolder.Content = SignUp;
        }
    }
}
