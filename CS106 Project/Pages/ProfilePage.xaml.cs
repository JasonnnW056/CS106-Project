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
using MongoDB.Driver;

namespace CS106_Project.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private bool isEditMode = false;
        private string? UserID = LoginManager.UserID;
        public ProfilePage()
        {
            InitializeComponent();

            var Filter = Builders<Users>.Filter.Eq(U=>U.Id, UserID);

            var Result = Connection.UsersCollection.Find(Filter).ToList();

            foreach (var item in Result)
            {
                this.DataContext = item;
            }

            //Check if admin login
            if (LoginManager.IsAdmin)
            {
                UserCategoryLabel.Text = "Admin";
            }
        }

        private void OnEditProfile(object sender, RoutedEventArgs e)
        {
            isEditMode = true;

            EmailBox.IsReadOnly = false;
            PhoneBox.IsReadOnly = false;

            
            LogoutButton.Content =  "Save Profile";
            LogoutButtonBackground.Background = Brushes.Green;
            
        }

        private void OnSaveOrLogout(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                string NewEmail = EmailBox.Text.Trim();
                string NewPhone = PhoneBox.Text.Trim();

                //Give option to try again dawawdawdwdwdwwd
                if (!Validations.EmailValidation(NewEmail))
                {
                    MessageBox.Show("Invalid Email Format!");
                    return;
                }
                if (!Validations.PhoneNumberValidation(NewPhone)) 
                {
                    MessageBox.Show("Invalid Phone Number Format!");
                    return;
                }

                var Filter = Builders<Users>.Filter.Eq(u => u.Id, UserID);
                var Update = Builders<Users>.Update
                    .Set(u => u.Email, NewEmail.ToLower())
                    .Set(u => u.PhoneNumber, NewPhone);

                var Result = Connection.UsersCollection.UpdateOne(Filter, Update);

                if (Result.ModifiedCount > 0)
                {
                    MessageBox.Show("Profile updated!");
                }

                // Exit edit mode
                isEditMode = false;
                EmailBox.IsReadOnly = true;
                PhoneBox.IsReadOnly = true;
                LogoutButton.Content = "Logout";
                LogoutButtonBackground.Background = Brushes.Red;
            }
            else
            {
                //Logout Functionality
                LoginManager.Logout();
                var Navigation = NavigationService.GetNavigationService(this);
                var LoginPage = new LoginPage();
                Navigation.Navigate(LoginPage);
            }
        }
        
        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            var Navigation = NavigationService.GetNavigationService(this);
            var ResetPasswordPage = new ResetPasswordPage();

            Navigation.Navigate(ResetPasswordPage);
        }
    }
}
