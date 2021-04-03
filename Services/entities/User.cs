using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Services.entities
{
    public class User
    {
        public enum Roleoptions 
        {
            Admin,
            Customer,
            Provider
        
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Roleoptions Role { get; set; }

        public User()
        {
        }

        public User(int id, string email, string password, string name,bool Status ,Roleoptions role)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Role = role;
            this.Status = Status;
        }
    }
}