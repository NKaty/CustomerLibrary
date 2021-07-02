using System.Data.SqlClient;

namespace CustomerLibrary.Data.Repositories
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
    }
}