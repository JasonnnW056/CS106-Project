using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CS106_Project.Models
{
   
    public class UserBookingData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string IllnessDescription { get; set; }

        public UserBookingData(string first, string last, string phone, string email, string type, DateTime date, string illness)
        {
            this.FirstName = first; 
            this.LastName = last;
            this.PhoneNumber = phone;
            this.Email = email;
            this.Type = type;
            this.Date = date;
            this.IllnessDescription = illness;
        }
    }
}
