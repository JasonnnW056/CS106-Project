using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MongoDB.Bson;
using MongoDB.Driver;

namespace CS106_Project.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AdminUserListPage.xaml
    /// </summary>
    public partial class AdminUserListPage : Page
    {
        public IMongoCollection<Users>? Collection;
        public ObservableCollection<Users>? UsersCollection { get; set; }

        public AdminUserListPage()
        {
            InitializeComponent();
            new Connection();

            

            Collection = Connection.DB.GetCollection<Users>("patients");
            var Filter = Builders<Users>.Filter.Empty;
            var Result = Collection.Find(Filter).ToList();


            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            UsersCollection = new ObservableCollection<Users>(Result);
            UsersContainer.ItemsSource = UsersCollection;

        }
        public AdminUserListPage(string UserInput)
        {
            InitializeComponent();
            new Connection();

            Collection = Connection.DB.GetCollection<Users>("patients");
            var Filter = Builders<Users>.Filter.Regex(u => u.Username, new BsonRegularExpression(UserInput, "i"));
            var Result = Collection.Find(Filter).ToList();


            if (!Result.Any())
            {
                NotFoundPage.Visibility = Visibility.Visible;
                return;
            }

            UsersCollection = new ObservableCollection<Users>(Result);
            UsersContainer.ItemsSource = UsersCollection;

        }

        private void OnDeleteUsers(object sender, RoutedEventArgs e)
        {
            Button? Button = sender as Button;

            if (Button != null) { 
                Users? user = Button?.DataContext as Users;
               if (user != null) { 
                var Filter = Builders<Users>.Filter.Eq(u => u.Id, user.Id);
                 
                
                    var Result = Collection?.DeleteOne(Filter);
                    if (Result?.DeletedCount > 0)
                    {
                        MessageBox.Show("Deleted");
                    }
                }
            }
        }
    }
}
