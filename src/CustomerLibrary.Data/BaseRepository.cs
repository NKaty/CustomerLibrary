using System.Collections.Generic;
using System.Data.SqlClient;

namespace CustomerLibrary.Data
{
    public abstract class BaseRepository
    {
        public SqlConnection GetConnection()
        {
            var connectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"Desktop",
                InitialCatalog = "CustomerLib_Naida",
                IntegratedSecurity = true
            };

            var connection = new SqlConnection(connectionBuilder.ConnectionString);
            connection.Open();
            return connection;
        }

        public SqlTransaction GetTransaction(SqlConnection connection)
        {
            var transaction = connection.BeginTransaction();
            return transaction;
        }
    }
}