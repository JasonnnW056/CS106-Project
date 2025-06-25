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
    /// Interaction logic for ResetPasswordPage.xaml
    /// </summary>
    public partial class ResetPasswordPage : Page
    {
        
        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            string NewPassword = NewPasswordBox.Password;
            string ConfirmationPassword = ConfirmationPasswordBox.Password;

            if (!Validations.PasswordValidation(NewPassword))
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Content = "Password should contain at least 8 characters, 1 uppercase, 1 lowercase, 1 digit!";
                return;
            }
            if (ConfirmationPassword != NewPassword)
            {
                ErrorMessage.Visibility = Visibility.Visible;
                ErrorMessage.Content = "Your new password does not match with the confirmation password!";
                return;
            }

            NewPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(NewPassword);
            string? UserID = LoginManager.UserID;

            var Filter = Builders<Users>.Filter.Eq(u => u.Id, UserID);
            var UpdateBuilder = Builders<Users>.Update.Set(u => u.Password, NewPassword);

            var Result = Connection.UsersCollection.UpdateOne(Filter, UpdateBuilder);

            if (Result.ModifiedCount > 0)
            {
                MessageBox.Show("Password updated!");
            }
        }

        private void OnCancelReset(object sender, RoutedEventArgs e)
        {
            var Navigation = NavigationService.GetNavigationService(this);
            var ProfilePage = new ProfilePage();    

            Navigation.Navigate(ProfilePage);
        }
    }
}
