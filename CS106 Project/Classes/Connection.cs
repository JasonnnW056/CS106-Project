using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using CS106_Project.Models;

namespace CS106_Project.Classes
{
    public class Connection
    {
        public static MongoClient Client { get; set; } = null;
        public static IMongoDatabase DB { get; set; } = null;

        public static IMongoCollection<Users> UsersCollection { get; set; } = null;
        public static IMongoCollection<Doctors> DoctorsCollection { get; set; } = null;
        public static IMongoCollection<AppointmentDetails> AppointmentsCollection { get; set; } = null;

        public static string ConnectionString = "mongodb+srv://Jason:JasonCluster@mydoctorscluster.xxfk2fu.mongodb.net/?retryWrites=true&w=majority&appName=MyDoctorsCluster";

        public Connection()
        {

            Console.WriteLine("Connecting to DB..");
            CreateConnection();
            SetDatabase();
        }

        public void CreateConnection()
        {
            try
            {
                Client = new MongoClient(ConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to DB!", e.Message);
            }
        }


        public void SetDatabase()
        {
            if (Client is MongoClient)
            {
                DB = Client.GetDatabase("my_doctors");
            }
        }

    } 
}
