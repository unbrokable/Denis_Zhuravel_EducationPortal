using System;
using System.Text.Json;
namespace DL
{
    [Serializable]
   public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
