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
        public IMongoCollection<Users> Collection;
        public ProfilePage()
        {
            InitializeComponent();

            string objectID = "6850e8e17b1f64e977350bdf";

            new Connection(); //Can remove for all page just need to load at the mainwindow

            Collection = Connection.DB.GetCollection<Users>("patients");

            var Filter = Builders<Users>.Filter.Eq(U=>U.Id, objectID);

            var Result = Collection.Find(Filter).ToList();

            foreach (var item in Result)
            {
                this.DataContext = item;
            }
        }

        private void OnEditProfile(object sender, RoutedEventArgs e)
        {
            isEditMode = true;

            EmailBox.IsReadOnly = false;
            PhoneBox.IsReadOnly = false;

            
            LogoutButton.Content =  "Save Profile";
            LogoutButtonBackground.Background = Brushes.LightGray;
            
        }

        private void OnSaveOrLogout(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                // 🔄 Save to MongoDB
                string userId = "6850e8e17b1f64e977350bdf"; // however you're tracking the logged-in user
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

                var Filter = Builders<Users>.Filter.Eq(u => u.Id, userId);
                var Update = Builders<Users>.Update
                    .Set(u => u.Email, NewEmail.ToLower())
                    .Set(u => u.PhoneNumber, NewPhone);

                var Result = Collection.UpdateOne(Filter, Update);

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
                // 🚪 Actual logout logic here
                MessageBox.Show("Logging out...");
                
            }
        }
    }
}
