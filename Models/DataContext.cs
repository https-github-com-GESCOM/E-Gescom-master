using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.Entity;

namespace Login.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {
        

        public DataContext()
    : base("DefaultConnection") // Cette 'DefaultConnection' doit être égale au nom de la chaîne de connexion sur Web.config.
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }
        public DbSet<User> Users { get; set; }
    }
}