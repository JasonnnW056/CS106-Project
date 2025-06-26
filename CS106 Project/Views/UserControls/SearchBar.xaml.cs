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
using CS106_Project.Pages;
using CS106_Project.Pages.AdminPages;

namespace CS106_Project.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
            UpdateUIOnAdminLogin();
        }

        private void UpdateUIOnAdminLogin()
        {
            if (LoginManager.IsAdmin && LoginManager.IsLoggedIn)
            {
                SearchLabel.Text = "Search";
                PlaceholderText.Text = "Enter user name or doctor name...";
                SearchFilter.Items.Clear();

                var userItem = new ComboBoxItem { Content = "User" };
                var doctorItem = new ComboBoxItem { Content = "Doctor" };

                SearchFilter.Items.Add(userItem);
                SearchFilter.Items.Add(doctorItem);

                SearchFilter.SelectedIndex = 0;

            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchBox.Text) ?
                Visibility.Visible : Visibility.Hidden;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderText.Visibility = Visibility.Hidden;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchBox.Text) ?
                Visibility.Visible : Visibility.Hidden;
        }

        private void OnSearch(object sender, MouseButtonEventArgs e)
        {
            
            if (SearchFilter.SelectedItem is ComboBoxItem selectedItem)
            {
                string? Value = selectedItem.Content?.ToString();
                if (Value != null && SearchBox.Text != null)
                {
                    var Keyword = SearchBox.Text.ToString();
                   

                    var mainWindow = Application.Current.MainWindow;
                    var mainFrame = mainWindow.FindName("MainFrame") as Frame;


                    if (mainFrame != null)
                    {
                        if (LoginManager.IsAdmin)
                        {
                           
                            //Admin Login
                            if (Value == "User")
                            {
                                mainFrame.NavigationService.Navigate(new AdminUserListPage(Keyword));
                            }
                            else if (Value == "Doctor")
                            {
                                mainFrame.NavigationService.Navigate(new AdminDoctorListPage(Keyword));
                            }

                        }
                        else
                        {
                            //User Login
                            mainFrame.NavigationService.Navigate(new DoctorList(Value, Keyword));
                        }
                    }
                }
            }
            
        }
    }
}
