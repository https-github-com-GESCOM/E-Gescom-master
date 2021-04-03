//using Login.Controllers.Services;
using Login.Services.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Login.Services
{
    public class Register
    {  public Registercommande Command { get; private set; }

        public Register(Registercommande command)
        {
            Command = command;
        }
        public User Execute()
        {
            Sql sql = new Sql("sqlserver");

            var parmaters = new Sql.Parameter[]
            {
             new Sql.Parameter("@Id",DBNull.Value,System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput),
            new  Sql.Parameter("@Email",Command.Email, System.Data.DbType.String),
            new Sql.Parameter("@Password",Command.Password,System.Data.DbType.String),
            new Sql.Parameter("@Name",Command.Name,System.Data.DbType.String),
            new Sql.Parameter("@CreateDate",Command.CreateDate,System.Data.DbType.DateTime),
            new Sql.Parameter("@Status",Command.Status,System.Data.DbType.Boolean),
            new Sql.Parameter("@Role",Command.Role,System.Data.DbType.Int32)

            };

            sql.Execute("Sp_User_Insert", parmaters, true);
            var id = int.Parse( parmaters[0].Value.ToString());
            
            
            var reader = sql.Read("Sp_User_Select", new Sql.Parameter[]
            {
             new Sql.Parameter("@Id", id,System.Data.DbType.Int32),
            new  Sql.Parameter("@Email",DBNull.Value),
            new Sql.Parameter("@Password",DBNull.Value),
            new Sql.Parameter("@Name",DBNull.Value),
            new Sql.Parameter("@CreateDate",DBNull.Value),
            new Sql.Parameter("@Status",DBNull.Value),
             new Sql.Parameter("@Role",DBNull.Value)





            }, true
            );
            if (reader != null)
            {
                while (reader.Read())
                {
                    return new User
                        (
                        int.Parse(reader["Id"].ToString()),
                        reader["Email"].ToString(),
                        reader["Password"].ToString(),
                            reader["Name"].ToString(),


                      reader["Status"].ToString() .ToLower()== "true" ? true : false,

                  
       (User.Roleoptions)int.Parse(reader["Role"].ToString() )
                      
                       );

                }
                reader.Close();

            }

            return null;

        }


    }

    public class Registercommande
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public Boolean Status { get; set; }

        public User.Roleoptions Role { get; set; }
         
        public DateTime CreateDate { get; set; }

        public Registercommande()
        {

        }

        public Registercommande(string email, string password, string name, bool status, User.Roleoptions role, DateTime createDate)
        {
            Email = email;
            Password = password;
            Name = name;
            Status = status;
            Role = role;
            CreateDate = createDate;
        }
    }
}