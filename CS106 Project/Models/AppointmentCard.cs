using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS106_Project.Models
{
    public class AppointmentCard
    {
        [BsonId]
        public string Id { get; set; }  
        public DateTime Date { get; set; }

        public string DoctorName { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public AppointmentCard(string id, DateTime date, string docctorName, string type, string status) { 
            this.Id = id;
            this.Date = date;
            this.DoctorName = docctorName;
            this.Type = type;
            this.Status = status;
        }
    }
}
