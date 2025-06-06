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
    [BsonIgnoreExtraElements]
    internal class UserBookingData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("illness_description")]
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
