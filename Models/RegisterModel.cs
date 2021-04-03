using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class RegisterModel : BaseModel
    {
        private int id;
        private string name;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nom { get; set; }

     
        public string Telephone { get; set; } 

        [Required]
        [RegularExpression(@"\w+([-+_']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address ")]
        public string Email { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmePassword { get; set; }

        public RegisterModel()
        {
        }

        public RegisterModel(string nom, string telephone , string nationalite, string email, string password, string confirmePassword)
        {
            Nom = nom;
            Telephone = telephone;

        
            Email = email;
            Password = password;
            ConfirmePassword = confirmePassword;
        }

        public RegisterModel(string email, string password, string name, string Password)
        {
            Email = email;
            this.Password = password;
            this.name = name;
            Password = password;
        }
    }
}