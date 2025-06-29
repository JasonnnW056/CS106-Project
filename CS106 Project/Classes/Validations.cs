using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CS106_Project.Classes
{
    public class Validations
    {
        public static bool UserNameValidation(string Username)
        {
            // 3-20 characters, letters, numbers, and underscores only
            string UsernamePattern = @"^[a-zA-Z0-9_]{3,20}$";

            if (string.IsNullOrWhiteSpace(Username))
            {
                return false;
            }

            return Regex.IsMatch(Username, UsernamePattern);
        }

        public static bool EmailValidation(string Email)
        {
            //Must contain '@'
            string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";


            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            return Regex.IsMatch(Email, EmailPattern);
        }

        public static bool PasswordValidation(string Password)
        {
            // At least 8 characters, 1 uppercase, 1 lowercase, 1 digit
            string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$";

            if (string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return Regex.IsMatch(Password, PasswordPattern);
        }

        public static bool NameValidation(string Name) {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (Name.Length < 3) { 
                return false;   
            }

            return true;
        }

        public static bool PhoneNumberValidation(string Phone)
        {
            if (string.IsNullOrWhiteSpace(Phone))
            {
                return false;
            }

            if (!IsDigitsOnly(Phone))
            {
                MessageBox.Show("Phone number must contain digits only.");
               
                return false;
            }

            return true;
        }
        private static bool IsDigitsOnly(string text)
        {
            return text.All(char.IsDigit);
        }
        public static bool TimeFormatValidation (string timeString)
        {
            if (string.IsNullOrWhiteSpace(timeString))
            {
                return false;
            } 
            string pattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(timeString, pattern);
        }
    }
}
