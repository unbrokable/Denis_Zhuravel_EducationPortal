using DL.Interfaces;
using System;
using System.IO;
using  System.Text.Json;

namespace DL
{
   
    public class Context<T> : IContext<T> where T : class
    {
        string location;
        public Context(string location){
            this.location = location;
        }
        public T Load()
        {
            T data;
            try
            {
                using (var fs = File.OpenRead(location))
                {
                   data =  JsonSerializer.DeserializeAsync<T>(fs).Result;
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            return data;
        }
        public bool Save( T data)
        {
            try
            {
                using (FileStream fs = new FileStream(location, FileMode.Create))
                {
                    JsonSerializer.SerializeAsync(fs, data);
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
