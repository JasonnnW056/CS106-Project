using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CS106_Project.Models
{
    [BsonIgnoreExtraElements]
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
        
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }




        public Users(string username, string phone, string email, string password)
        {

            //Hashing the password
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            //Lowercasing the user email before transferring to database
            email = email.ToLower();

            this.Username = username; this.PhoneNumber = phone; this.Email = email; this.Password = password;

        }
    }
}
