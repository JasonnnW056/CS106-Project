using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using MongoDB.Bson;
using MongoDB.Driver;


namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SignUpForm.xaml
    /// </summary>
    public partial class SignUpForm : UserControl
    {
        public event EventHandler? SwitchToLogin;

        public SignUpForm()
        {
            InitializeComponent();
        }

        private async void OnSignUp(object sender, RoutedEventArgs e)
        {
            string Username = UsernameInput.Text.Trim();
            string Email = EmailInput.Text.Trim();
            string Phone = PhoneInput.Text.Trim();
            string Password = PasswordInput.Password;

            //Resetting Error Text Visibility
            ErrorText.Visibility = Visibility.Collapsed;

            //List of Errors
            List<string> Errors = new List<string>();

            if (!Validations.UserNameValidation(Username))
            {
                Errors.Add("* Username must be 3-20 characters, letters, numbers, and underscores only!");
                UsernameInput.Clear();
                UsernameInput.Focus();
            }

            if (!Validations.PhoneNumberValidation(Phone))
            {
                Errors.Add("* Phone Number should only contain number!");
                EmailInput.Clear();
                EmailInput.Focus(); 
            }

            if (!Validations.EmailValidation(Email))
            {
                Errors.Add("* Invalid email format!");
                EmailInput.Clear();
                EmailInput.Focus();
            }

            if (!Validations.PasswordValidation(Password))
            {
                Errors.Add("* Password should have at least 8 characters, 1 uppercase, 1 lowercase, 1 digit!");
                PasswordInput.Clear();
                PasswordInput.Focus(); 
            }

            if (Errors.Any())
            {
                ErrorText.Visibility = Visibility.Visible;
                ErrorText.Content = $"{string.Join("\n", Errors)}";
                return;
            }


            //Checking if email existed (case insensitive)
            var EmailFilter = Builders<Users>.Filter.Regex(user => user.Email, new BsonRegularExpression(Email, "i"));
            var EmailChecker = Connection.UsersCollection.Find(EmailFilter).ToList();

            if (EmailChecker.Any())
            {
                ErrorText.Visibility = Visibility.Visible;
                ErrorText.Content = "Email has already existed!";
            }
            else
            {
                Connection.UsersCollection.InsertOne(new Users(Username, Phone, Email, Password));
                ErrorText.Visibility = Visibility.Visible;
                ErrorText.Content = "Sign Up Successfully";
                await Task.Delay(2000);
                SwitchToLogin?.Invoke(this, EventArgs.Empty);
            }

        }

        private void OnSwitchingToLogin(object sender, MouseButtonEventArgs e)
        {
            UsernameInput.Clear();
            PhoneInput.Clear();
            EmailInput.Clear();
            PasswordInput.Clear();

            ErrorText.Visibility = Visibility.Collapsed;
            SwitchToLogin?.Invoke(this, EventArgs.Empty);
        }
    }
}
