using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Core.Models;
using Core.DAL.Interfaces;

namespace DAL.Services
{
    public class UserRepository : IRepository
    {
        public List<User> LoadRecords()
        {
            var users = new List<User>();

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(GetJsonData()));
            var ser = new DataContractJsonSerializer(users.GetType());

            try
            {
                users = ser.ReadObject(ms) as List<User>;
            }
            catch (SerializationException exc)
            {
                Console.WriteLine("Один из объектов не подлежит десериализации" + exc.Message);
            }
            finally
            {
                ms.Close();
            }

            return users.Where(u => u.firstName != null && u.lastName != null).ToList();
        }

        private string GetJsonData()
        {
            var json = "[{ \"Age\":40,\"firstName\":\"Fred\",\"lastName\":\"Smith\"},{\"lastName\":\"Jackson\"}]";
            return json;
        }
    }
}