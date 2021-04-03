//using Login.Controllers.Services;
using Login.Services.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Login.Services
{
    public class Authenticate
    {  public Authenticatecommande Command { get; private set; }

        public Authenticate(Authenticatecommande command)
        {
            Command = command;
        }

        public Authenticate()
        {
        }

        public User Execute()
        {
            Sql sql = new Sql("sqlserver");
            var reader = sql.Read("Sp_User_Authenticate", new Sql.Parameter[]
            {
            new  Sql.Parameter("@Email",Command.Email, System.Data.DbType.String),
            new Sql.Parameter("@Password",Command.Password,System.Data.DbType.String)

            }, true
            );
            if (reader != null)
            {
                while (reader.Read())
                {
                    return new User
                        (int.Parse(reader["Id"].ToString()),
                        reader["Email"].ToString(),
                        reader["Password"].ToString(),
                        reader["Name"].ToString(),


                      reader["Status"].ToString().ToLower() == "true" ? true : false,


       (User.Roleoptions)int.Parse(reader["Role"].ToString())

                        // DateTime.Parse(reader["CreateDate"].ToString()),
                        //reader["Status"].ToString()== "1" ? true : false

                        );

                }
                reader.Close();

            }

            return null;

        }


    }

    public class Authenticatecommande
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Authenticatecommande()
        {

        }

        public Authenticatecommande(string email, string password)
        {
            Email = email;
            this.Password = password;
        }
    }
}