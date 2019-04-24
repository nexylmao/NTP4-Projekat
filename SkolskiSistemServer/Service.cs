using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using Newtonsoft.Json;
using SkolskiSistemCommon;

namespace SkolskiSistemServer
{
    public class Service : IService
    {
        public IEnumerable<Skola> GetSkole()
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand("SELECT * FROM SKOLA;", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Skola");
                    }

                    if (dataSet.Tables["Skola"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var list = new List<Skola>();
                    foreach (var row in dataSet.Tables["Skola"].Select())
                    {
                        list.Add(new Skola(Convert.ToInt32(row["id"]), (string) row["naziv"], (string) row["adresa"],
                            (string) row["telefon"], (string) row["email"]));
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public Skola GetSkola(int id)
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand($"SELECT * FROM SKOLA WHERE ID LIKE {id};", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Skola");
                    }

                    if (dataSet.Tables["Skola"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var row = dataSet.Tables["Skola"].Select()[0];
                    return new Skola(Convert.ToInt32(row["id"]), (string) row["naziv"], (string) row["adresa"],
                        (string) row["telefon"], (string) row["email"]);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }
    }
}