using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceModel;
using skolski_sistem_common;

namespace skolski_sistem_server
{
    public class Service : IService
    {
        private SqlConnection _sqlConnection;
        private SqlDataAdapter _skolaAdapter;
        private SqlDataAdapter _smerAdapter;
        private SqlDataAdapter _ucenikAdapter;
        private DataSet _dataSet;

        public Service()
        {
            try
            {
                // Connecting to the database
                _sqlConnection = new DatabaseInit().Init();
                // Create tables if they don't exist
                new SqlCommand(SqlQueries.CreateSkola, _sqlConnection).ExecuteNonQuery();
                new SqlCommand(SqlQueries.CreateSmer, _sqlConnection).ExecuteNonQuery();
                new SqlCommand(SqlQueries.CreateUcenik, _sqlConnection).ExecuteNonQuery();
                // Create all the adapters and their insert commands
                // Skola
                _skolaAdapter = new SqlDataAdapter(SqlQueries.SelectSkola, _sqlConnection);
                var skolaInsert = new SqlCommand(SqlQueries.InsertSkola, _sqlConnection);
                skolaInsert.Parameters.Add("@naziv", SqlDbType.NVarChar).SourceColumn = "naziv";
                skolaInsert.Parameters.Add("@adresa", SqlDbType.NVarChar).SourceColumn = "adresa";
                skolaInsert.Parameters.Add("@telefon", SqlDbType.NVarChar).SourceColumn = "telefon";
                skolaInsert.Parameters.Add("@email", SqlDbType.NVarChar).SourceColumn = "email";
                _skolaAdapter.InsertCommand = skolaInsert;
                // Smer
                _smerAdapter = new SqlDataAdapter(SqlQueries.SelectSmer, _sqlConnection);
                var smerInsert = new SqlCommand(SqlQueries.InsertSmer, _sqlConnection);
                smerInsert.Parameters.Add("@naziv", SqlDbType.NVarChar).SourceColumn = "naziv";
                _smerAdapter.InsertCommand = smerInsert;
                // Ucenik
                _ucenikAdapter = new SqlDataAdapter(SqlQueries.SelectUcenik, _sqlConnection);
                var ucenikInsert = new SqlCommand(SqlQueries.InsertUcenik, _sqlConnection);
                ucenikInsert.Parameters.Add("@ime", SqlDbType.NVarChar).SourceColumn = "ime";
                ucenikInsert.Parameters.Add("@prezime", SqlDbType.NVarChar).SourceColumn = "prezime";
                ucenikInsert.Parameters.Add("@jmbg", SqlDbType.Date).SourceColumn = "jmbg";
                ucenikInsert.Parameters.Add("@adresa", SqlDbType.NVarChar).SourceColumn = "adresa";
                ucenikInsert.Parameters.Add("@mobilniTelefon", SqlDbType.NVarChar).SourceColumn = "mobilniTelefon";
                ucenikInsert.Parameters.Add("@idSmera", SqlDbType.Int).SourceColumn = "idSmera";
                ucenikInsert.Parameters.Add("@idSkole", SqlDbType.Int).SourceColumn = "idSkole";
                _ucenikAdapter.InsertCommand = ucenikInsert;
                // Create the DataSet
                _dataSet = new DataSet("Skolski Sistem");
                // Fill the DataSet
                _skolaAdapter.Fill(_dataSet, "Skola");
                _smerAdapter.Fill(_dataSet, "Smer");
                _ucenikAdapter.Fill(_dataSet, "Ucenik");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Skola> GetSkole()
        {
            try
            {
                return _dataSet.Tables["Skola"].Select().Select(row => new Skola(Convert.ToInt32(row["id"]), (string) row["naziv"], (string) row["adresa"], (string) row["telefon"], (string) row["email"])).ToList();
            }
            catch (Exception ex)
            {
                throw new FaultException<Zmaj>(new Zmaj(ex.Message));
            }
        }

        public Skola GetSkola(int id)
        {
            try
            {
                var row = _dataSet.Tables["Skola"].Select($"id = {id}");
                if (row == null)
                {
                    throw new FaultException<Zmaj>(new Zmaj("Oduvao sam tu skolu. Ne postoji vise."));
                }

                Console.WriteLine(row);
                return new Skola(Convert.ToInt32(row["id"]), (string) row["naziv"], (string) row["adresa"],
                    (string) row["telefon"], (string) row["email"]);
            }
            catch (Exception ex)
            {
                throw new FaultException<Zmaj>(new Zmaj(ex.Message));
            }
        }

        public Skola PostSkola(Skola skola)
        {
            try
            {
                var row = _dataSet.Tables["Skola"].NewRow();
                row["naziv"] = skola.Naziv;
                row["adresa"] = skola.Adresa;
                row["telefon"] = skola.Telefon;
                row["email"] = skola.Email;
                _dataSet.Tables["Skola"].Rows.Add(row);
                _skolaAdapter.Update(_dataSet.Tables["Skola"]);
                return _dataSet.Tables["Skola"].Select($"id = {id}").Select(x => new Skola(Convert.ToInt32(x["id"]), (string) x["naziv"], (string) x["adresa"], (string) x["telefon"], (string) x["email"])).ToList()[0];
            }
            catch (Exception ex)
            {
                throw new FaultException<Zmaj>(new Zmaj(ex.Message));
            }
        }

        public bool UpdateSkola(Skola skola)
        {
            try
            {
                var row = _dataSet.Tables["Skola"].Select($"id = {skola.Id}").First();
                row["naziv"] = skola.Naziv;
                row["adresa"] = skola.Adresa;
                row["telefon"] = skola.Telefon;
                row["email"] = skola.Email;
                _skolaAdapter.Update(_dataSet.Tables["Skola"]);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException<Zmaj>(new Zmaj(ex.Message));
            }
        }

        public bool DeleteSkola(int id)
        {
            try
            {
                _dataSet.Tables["Skola"].Select($"id = {id}").First().Delete();
                _skolaAdapter.Update(_dataSet.Tables["Skola"]);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException<Zmaj>(new Zmaj(ex.Message));
            }
        }

        ~Service()
        {
            _sqlConnection.Close();
            _skolaAdapter.Dispose();
            _smerAdapter.Dispose();
            _ucenikAdapter.Dispose();
            _dataSet.Dispose();
        }
    }
}