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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public event EventHandler? SwitchToSignup;
        private readonly IMongoCollection<Users> Collection;
        public LoginForm()
        {
            InitializeComponent();
            new Connection();

            Collection = Connection.DB.GetCollection<Users>("users");
        }
        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string Email = EmailInput.Text.Trim();
            string Password = PasswordInput.Password;

            //Resetting Error Text Visibility
            MessageText.Visibility = Visibility.Collapsed;



            //To check if its email or not
            if (!EmailValidation(Email))
            {
                MessageText.Visibility = Visibility.Visible;
                MessageText.Text = "Invalid email!";
                EmailInput.Clear();
                PasswordInput.Clear();
                EmailInput.Focus(); //Rework this
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                MessageText.Visibility = Visibility.Visible;
                MessageText.Text = "Please input your password!";
                return;
            }


            //Checking Login Details

            var EmailFilter = Builders<Users>.Filter.Regex(user => user.Email, new BsonRegularExpression(Email, "i"));
            var PasswordFilter = Builders<Users>.Filter.Eq(user => user.Password, Password);

            var EmailPasswordFilter = Builders<Users>.Filter.And(EmailFilter, PasswordFilter);
            var LoginValidation = Collection.Find(EmailPasswordFilter).ToList();

            //To check if the email is existed
            var EmailChecker = Collection.Find(EmailFilter).ToList();

            if (EmailChecker.Any())
            {
                foreach (var item in EmailChecker)
                {
                    //Checking user's password input with the hashed password in database
                    bool PasswordChecker = BCrypt.Net.BCrypt.EnhancedVerify(Password, item.Password);

                    if (PasswordChecker)
                    {
                        MessageText.Visibility = Visibility.Visible;
                        MessageText.Text = "Login Successfully!";
                    }
                    else
                    {
                        MessageText.Visibility = Visibility.Visible;
                        MessageText.Text = "Invalid login details!";
                    }
                }
            }
            else
            {
                MessageText.Visibility = Visibility.Visible;
                MessageText.Text = "Email does not exist!";
            }


            /*   if (LoginValidation.Any())
               {
                   MessageText.Visibility= Visibility.Visible;
                   MessageText.Text = "Login Successfully!";
               }
               else
               {
                   MessageText.Visibility = Visibility.Visible;
                   MessageText.Text = "Invalid login details!";
               }*/

        }
        private bool EmailValidation(string Email)
        {
            //Must contain '@'
            string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";


            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            return Regex.IsMatch(Email, EmailPattern);
        }

        private void OnSwitchingToSignUp(object sender, MouseButtonEventArgs e)
        {
            SwitchToSignup?.Invoke(this, EventArgs.Empty);
        }
    }
}
