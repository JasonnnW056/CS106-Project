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
using CS106_Project.Models;

namespace CS106_Project.Pages
{
    /// <summary>  
    /// Interaction logic for BookingPage.xaml  
    /// </summary>  
    public partial class BookingPage : Page
    {
        private Doctors DoctorData;

        private string DoctorName = string.Empty;
        private string DoctorSpecialty = string.Empty;

        public BookingPage(Doctors doctorData)
        {
            InitializeComponent();
            DoctorData = doctorData;
            LoadBookedDoctorData();
        }

        private void LoadBookedDoctorData()
        {
            DoctorName = DoctorData.Name;
            DoctorSpecialty = DoctorData.Specialty;
        }
    }
}
