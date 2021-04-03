using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class BaseModel
    {

        public bool IsError { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}