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
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }


        [BsonElement("password")]
        public string Password { get; set; }




        public Users(string name, string email, string password)
        {

            //Hashing the password
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            //Lowercasing the user email before transferring to database
            email = email.ToLower();

            this.Name = name; this.Email = email; this.Password = password;

        }
    }
}
