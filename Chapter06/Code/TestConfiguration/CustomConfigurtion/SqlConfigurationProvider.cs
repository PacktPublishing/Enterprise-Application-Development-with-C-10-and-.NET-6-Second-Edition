using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace TestConfiguration.CustomConfiguration
{
    public class SqlConfigurationProvider : ConfigurationProvider
    {
        public SqlConfigurationSource Source { get; }

        public SqlConfigurationProvider(SqlConfigurationSource source)
        {
            Source = source;
        }

        public override void Load()
        {
            // create a connection object  
            SqlConnection sqlConnection = new SqlConnection(Source.ConnectionString);

            // Create a command object  
            SqlCommand sqlCommand = new SqlCommand(Source.Query, sqlConnection);
            sqlConnection.Open();

            // Call ExecuteReader to return a DataReader  
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Data.Add(sqlDataReader.GetString(0), sqlDataReader.GetString(1));
            }
            sqlDataReader.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();
        }
    }
}