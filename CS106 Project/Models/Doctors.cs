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
    public class Doctors
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("specialty")]
        public string Specialty { get; set; }

        [BsonElement("availability")]
        public string Availability { get; set; }

        public Doctors(string name, string specialty, string availability)
        {
            this.Name = name;
            this.Specialty = specialty;
            this.Availability = availability;
        }
    } 
}
