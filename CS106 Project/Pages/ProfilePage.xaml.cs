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
        public ProfilePage()
        {
            InitializeComponent();

            string objectID = "6850e8e17b1f64e977350bdf";

            new Connection(); //Can remove for all page just need to load at the mainwindow

            var Collection = Connection.DB.GetCollection<Users>("patients");

            var Filter = Builders<Users>.Filter.Eq(U=>U.Id, objectID);

            var Result = Collection.Find(Filter).ToList();

            foreach (var item in Result)
            {
                this.DataContext = item;
            }
        }
    }
}
