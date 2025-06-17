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
using CS106_Project.Pages;

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
                    MessageBox.Show(Value); 
                    
                    var Keyword = SearchBox.Text.ToString();
                   
                    MessageBox.Show(Keyword);

                    var mainWindow = Application.Current.MainWindow;
                    var mainFrame = mainWindow.FindName("MainFrame") as Frame;

                    if (mainFrame != null)
                    {
                        mainFrame.NavigationService.Navigate(new DoctorList(Value, Keyword));
                    }
                }
            }
        }
    }
}
