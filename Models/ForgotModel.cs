using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Login.Models
{
    public class ForgotModel : BaseModel
    {
        [Required]
        [RegularExpression(@"\w+([-+_']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email address ")]
        public string Email { get; set; }

        
    }
}