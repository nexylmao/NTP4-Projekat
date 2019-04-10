using System;
using System.Data.SqlClient;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    public class Service : IService
    {
        private SqlConnection _sqlConnection;

        public Service()
        {
            _sqlConnection = new DatabaseInit().Init();
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }

        ~Service()
        {
            _sqlConnection.Close();
        }
    }
}