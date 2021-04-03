using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using System.Linq;

namespace Login.Services
{
    public class Sql
    {

        public string ConnectionString { get; private set; }

        private DbProviderFactory factory;

        public Sql(string ConStringName)
        {// creer une connection à partir du nom du provider

            ConnectionString = ConfigurationManager.
                ConnectionStrings[ConStringName].ConnectionString;
            string providerName = ConfigurationManager.
                ConnectionStrings[ConStringName].ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);//recupère un espace de nom et le passe en param

        }
        public void Execute(string commandText, IEnumerable<Parameter> parameters, bool isStoredProcedure = false)
        {
            using (var con = factory.CreateConnection())
            {
                con.ConnectionString = ConnectionString;
                con.Open();
                using (var command = factory.CreateCommand())
                {
                    command.Connection = con;
                    command.CommandText = commandText;
                    if (isStoredProcedure)
                        command.CommandType = CommandType.StoredProcedure;

                    AddCommandParameters(parameters, command);

                    command.ExecuteNonQuery();
                    UpdateParameters(parameters, command);
                }
            }
        }

        private static void UpdateParameters(IEnumerable<Parameter> parameters, DbCommand command)
        {
            foreach (DbParameter p in command.Parameters)
            {
                var param = parameters.FirstOrDefault(x => x.Name == p.ParameterName);
                if (param != null)
                {
                    param.Value = p.Value;
                }
            }
        }

        private void AddCommandParameters(IEnumerable<Parameter> parameters, DbCommand command)
        {
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    var param = factory.CreateParameter();

                    param.ParameterName = p.Name;
                    param.Value = p.Value;
                    param.DbType = p.Type;
                    param.Direction = p.Direction;

                    command.Parameters.Add(param);
                }
            }
        }

        public DbDataReader Read(string query, IEnumerable<Parameter> parameters, bool isStoredProcedure = false)
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = query;
            if (isStoredProcedure)
                command.CommandType = CommandType.StoredProcedure;

            AddCommandParameters(parameters, command);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);//la fermeture d'une commande entraîne la fermeture de la connection

            UpdateParameters(parameters, command);

            return reader;
        }


        public class Parameter
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public DbType Type { get; set; }
            public ParameterDirection Direction { get; set; }
            public Parameter(string name, object value, ParameterDirection direction = ParameterDirection.Input)
            {
                Name = name;
                Value = value;
                Direction = direction;
            }
            public Parameter(string name, object value, DbType type, ParameterDirection direction = ParameterDirection.Input) :
                this(name, value, direction)
            {
                Type = type;
            }

        }
    }


}
