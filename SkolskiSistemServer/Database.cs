using System.Data;
using System.Data.SqlClient;

namespace SkolskiSistemServer
{
    public static class Database
    {
        public static SqlConnection sqlConnection;

        public static DataSet dataSet;
    }
}