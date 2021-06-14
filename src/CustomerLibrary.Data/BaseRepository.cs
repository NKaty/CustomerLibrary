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

            return new SqlConnection(connectionBuilder.ConnectionString);
        }
    }
}