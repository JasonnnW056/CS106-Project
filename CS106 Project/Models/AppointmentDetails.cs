using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS106_Project.Models
{
    [BsonIgnoreExtraElements]
    public class AppointmentDetails
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("patient_details")]
        public PatientDetails PatientDetails { get; set; }

        [BsonElement("doctor_details")]
        public DoctorDetails DoctorDetails { get; set; }

        [BsonElement("type_of_appointment")]
        public string TypeOfAppointment { get; set; }

        [BsonElement("illness_description")]
        public string IllnessDescription { get; set; }

        [BsonElement("appointment_date")]
        public DateTime AppointmentDate { get; set; }

       

        public AppointmentDetails(string first, string last, string phone, string email, string doctorName, string doctorSpecialty, string type, DateTime date, string illness)
        {
            this.PatientDetails = new PatientDetails
            {
                FirstName = first,
                LastName = last,
                PhoneNumber = phone,    
                Email = email,
            };

            this.DoctorDetails = new DoctorDetails
            {
                Name= doctorName,
                Specialty= doctorSpecialty,
            };

            this.TypeOfAppointment = type;

            this.IllnessDescription = illness;

            this.AppointmentDate = date;

           
        }

    }

    

    [BsonIgnoreExtraElements]
    public class PatientDetails
    {
        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }


    }

    public class DoctorDetails
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("specialty")]
        public string Specialty { get; set; }
    }


}
