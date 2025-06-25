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
using CS106_Project.Pages;
using CS106_Project.Pages.AdminPages;
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
        /*private readonly IMongoCollection<Users> Collection;*/
        public LoginForm()
        {
            InitializeComponent();
            
            /*Collection = Connection.DB.GetCollection<Users>("users");*/
        }
        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string Email = EmailInput.Text.Trim();
            string Password = PasswordInput.Password;

            // Resetting Error Text Visibility  
            MessageText.Visibility = Visibility.Collapsed;

            // To check if it's email or not  
            if (!Validations.EmailValidation(Email))
            {
                MessageText.Visibility = Visibility.Visible;
                MessageText.Content = "Invalid email or password!";
                EmailInput.Clear();
                PasswordInput.Clear();
                EmailInput.Focus();
                return;
            }

            // Checking Login Details  
            var EmailFilter = Builders<Users>.Filter.Regex(user => user.Email, new BsonRegularExpression(Email, "i"));
            var PasswordFilter = Builders<Users>.Filter.Eq(user => user.Password, Password);

            var EmailPasswordFilter = Builders<Users>.Filter.And(EmailFilter, PasswordFilter);
            var LoginValidation = Connection.UsersCollection.Find(EmailPasswordFilter).ToList();

            // To check if the email exists  
            var EmailChecker = Connection.UsersCollection.Find(EmailFilter).ToList();

            if (EmailChecker.Any())
            {
                foreach (var item in EmailChecker)
                {
                    // Checking user's password input with the hashed password in the database  
                    bool PasswordChecker = BCrypt.Net.BCrypt.EnhancedVerify(Password, item.Password);

                    if (PasswordChecker)
                    {
                        MessageText.Visibility = Visibility.Visible;
                        MessageText.Content = "Login Successfully!";
                        
                        // Navigation  
                        var mainWindow = Application.Current.MainWindow;
                        var mainFrame = mainWindow.FindName("MainFrame") as Frame;

                        //Checking if admin login or user login
                        if (item.Email.Contains("@admin.mydoctors.com"))
                        {
                            //Admin Login
                            LoginManager.Login(item.Username, item.Id, true);
                            mainFrame?.Navigate(new AdminUserListPage());
                            
                        }
                        else
                        {
                            //User Login
                            LoginManager.Login(item.Username, item.Id, false);
                            mainFrame?.Navigate(new DoctorList());
                            
                        }
                    }
                    else
                    {
                        MessageText.Visibility = Visibility.Visible;
                        MessageText.Content = "Invalid login details!";   
                    }
                }
            }
            else
            {
                MessageText.Visibility = Visibility.Visible;
                MessageText.Content = "Email does not exist!"; 
            }
        }

        private void OnSwitchingToSignUp(object sender, MouseButtonEventArgs e)
        {
            EmailInput.Clear(); 
            PasswordInput.Clear();
            MessageText.Visibility= Visibility.Collapsed;
            SwitchToSignup?.Invoke(this, EventArgs.Empty);
        }
    }
}
