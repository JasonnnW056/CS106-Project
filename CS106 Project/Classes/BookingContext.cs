using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS106_Project.Models;

namespace CS106_Project.Classes
{
    public static class BookingContext
    {
        private static Doctors? _currentDoctor;
        public static event EventHandler<Doctors>? DoctorChanged;

        public static Doctors? CurrentDoctor
        {
            get => _currentDoctor;
            set
            {
                _currentDoctor = value;
                DoctorChanged?.Invoke(null, value);
            }
        }
    }
}
