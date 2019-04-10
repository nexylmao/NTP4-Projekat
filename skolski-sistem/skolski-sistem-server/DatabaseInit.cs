using System;
using System.Data.SqlClient;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    public class DatabaseInit : IDisposable
    {
        private string connectionString =
            @"Server=tcp:astrihale.database.windows.net,1433;Initial Catalog=SkolskiSistem;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string ConnectionString =>
            connectionString.Replace("{your_username}", Environment.GetEnvironmentVariable("db_username"))
                .Replace("{your_password}", Environment.GetEnvironmentVariable("db_password"));

        private SqlConnection sqlConnection;

        public SqlConnection Init()
        {
            sqlConnection = new SqlConnection(ConnectionString);
            (new SqlCommand(Skola.SqlQueries.Create, sqlConnection)).ExecuteNonQuery();
            (new SqlCommand(Smer.SqlQueries.Create, sqlConnection)).ExecuteNonQuery();
            (new SqlCommand(Ucenik.SqlQueries.Create, sqlConnection)).ExecuteNonQuery();
            return sqlConnection;
        }

        public void Dispose()
        {
            sqlConnection?.Dispose();
        }
    }
}