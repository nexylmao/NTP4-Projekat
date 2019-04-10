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

        private SqlConnection _sqlConnection;

        public SqlConnection Init()
        {
            _sqlConnection = new SqlConnection(ConnectionString);
            _sqlConnection.Open();
            return _sqlConnection;
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}