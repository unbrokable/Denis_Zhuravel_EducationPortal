using Application.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Infrastructure
{
    public class BinaryHandler : IHandler
    {
        private readonly string location;

        public BinaryHandler(string location)
        {
            this.location = location;
        }

        public IEnumerable<T> Load<T>() where T : class
        {
            IEnumerable<T> data;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = File.OpenRead(String.Concat(location, @"\", typeof(T).Name, ".dat")))
                {
                    data = (List<T>)formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            return data;
        }

        public bool Save<T>(IEnumerable<T> data) where T : class
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(String.Concat(location, @"\", typeof(T).Name, ".dat"), FileMode.Create))
                {
                    formatter.Serialize(fs, data.ToList());
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
    }
}
