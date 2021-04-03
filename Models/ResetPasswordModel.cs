using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class ResetPasswordModel : LoginModel
    {       [Required(ErrorMessage =" New password requiredAssemblyLoadEventArgs", AllowEmptyStrings = false)]
       [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public string ResetCode { get; set; }

    }
}