using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public Skola PostSkola(Skola skola)
        {
            try
            {
                if (skola.Id != int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Ta skola vec ima id."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand($"INSERT INTO SKOLA(naziv, adresa, telefon, email) VALUES ('{skola.Naziv}', '{skola.Adresa}', '{skola.Telefon}', '{skola.Email}');", connection);
                    var rows = command.ExecuteNonQuery();

                    Console.WriteLine("Rows affected (INSERT INTO SKOLA) {0}.", rows);
                    return GetSkole().Where((skola1, i) =>
                    {
                        return skola1.Naziv == skola.Naziv &&
                               skola1.Adresa == skola.Adresa &&
                               skola1.Telefon == skola.Telefon &&
                               skola1.Email == skola.Email;
                    }).First();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public int PutSkola(Skola skola)
        {
            try
            {
                if (skola.Id == int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Ta skola nije u bazi podataka."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand("UPDATE SKOLA\n" +
                                                 $"SET naziv = '{skola.Naziv}', adresa = '{skola.Adresa}', telefon = '{skola.Telefon}', email = '{skola.Email}'\n" +
                                                 $"WHERE ID = {skola.Id};", connection);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public IEnumerable<Smer> GetSmerovi()
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand("SELECT * FROM SMER;", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Smer");
                    }

                    if (dataSet.Tables["Smer"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var list = new List<Smer>();
                    foreach (var row in dataSet.Tables["Smer"].Select())
                    {
                        list.Add(new Smer(Convert.ToInt32(row["id"]), (string)row["naziv"]));
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public Smer GetSmer(int id)
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand($"SELECT * FROM SMER WHERE ID LIKE {id};", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Smer");
                    }

                    if (dataSet.Tables["Smer"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var row = dataSet.Tables["Smer"].Select()[0];
                    return new Smer(Convert.ToInt32(row["id"]), (string) row["naziv"]);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public Smer PostSmer(Smer smer)
        {
            try
            {
                if (smer.Id != int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Taj smer vec ima id."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand($"INSERT INTO SMER(naziv) VALUES ('{smer.Naziv}');", connection);
                    var rows = command.ExecuteNonQuery();

                    Console.WriteLine("Rows affected (INSERT INTO SMER) {0}.", rows);
                    return GetSmerovi().Where((smer1, i) => { return smer1.Naziv == smer.Naziv; }).First();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public int PutSmer(Smer smer)
        {
            try
            {
                if (smer.Id == int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Taj smer nije u bazi podataka."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand("UPDATE SMER\n" +
                                                 $"SET naziv = '{smer.Naziv}'\n" +
                                                 $"WHERE ID = {smer.Id};", connection);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public IEnumerable<Ucenik> GetUcenici()
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand("SELECT * FROM UCENIK;", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Ucenik");
                    }

                    if (dataSet.Tables["Ucenik"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var list = new List<Ucenik>();
                    foreach (var row in dataSet.Tables["Ucenik"].Select())
                    {
                        list.Add(new Ucenik(Convert.ToInt32(row["id"]), (string)row["ime"], (string)row["prezime"], (string)row["jmbg"], DateTime.Parse(row["datumRodjenja"].ToString()), (string)row["adresa"], (string)row["mobilniTelefon"], Convert.ToInt32(row["idSmera"]), Convert.ToInt32(row["idSkole"])));
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public Ucenik GetUcenik(int id)
        {
            try
            {
                using (var dataSet = new DataSet())
                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    using (var command = new SqlCommand("SELECT * FROM UCENIK \n" +
                                                        $"WHERE ID = {id};", connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet, "Ucenik");
                    }

                    if (dataSet.Tables["Ucenik"].Rows.Count < 1)
                    {
                        return null;
                    }

                    var row = dataSet.Tables["Ucenik"].Select().First();
                    return new Ucenik(Convert.ToInt32(row["id"]), (string)row["ime"], (string)row["prezime"], (string)row["jmbg"],
                        DateTime.Parse(row["datumRodjenja"].ToString()), (string)row["adresa"], (string)row["mobilniTelefon"], Convert.ToInt32(row["idSmera"]), Convert.ToInt32(row["idSkole"]));
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public Ucenik PostUcenik(Ucenik ucenik)
        {
            try
            {
                if (ucenik.Id != int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Ucenik/ca vec ima id."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand("INSERT INTO UCENIK(ime, prezime, jmbg, datumRodjenja, adresa, mobilniTelefon, idSmera, idSkole) " +
                                                 $"VALUES ('{ucenik.Ime}', '{ucenik.Prezime}', '{ucenik.Jmbg}', '{ucenik.DatumRodjenja.GetDateTimeFormats()[5]}', " +
                                                 $"'{ucenik.Adresa}', '{ucenik.MobilniTelefon}', {ucenik.IdSmera}, {ucenik.IdSkole});", connection);
                    var rows = command.ExecuteNonQuery();

                    Console.WriteLine("Rows affected (INSERT INTO UCENIK) {0}.", rows);
                    Console.WriteLine(JsonConvert.SerializeObject(GetUcenici()));
                    var result = GetUcenici().Where((ucenik1, i) => { return ucenik1.Jmbg == ucenik.Jmbg; }).First();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }

        public int PutUcenik(Ucenik ucenik)
        {
            try
            {
                if (ucenik.Id == int.MinValue)
                {
                    throw new FaultException<Komunizam>(new Komunizam("Taj ucenik nije u bazi podataka."));
                }

                using (var connection = new SqlConnection(Program.SqlString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                        throw new Exception("Connection to the database öppnade inte!");

                    var command = new SqlCommand("UPDATE UCENIK\n" +
                                                 $"SET ime = '{ucenik.Ime}', prezime = '{ucenik.Prezime}', jmbg = '{ucenik.Jmbg}', datumRodjenja = '{ucenik.DatumRodjenja.GetDateTimeFormats()[5]}',\n" +
                                                 $"adresa = '{ucenik.Adresa}', mobilniTelefon = '{ucenik.MobilniTelefon}', idSmera = {ucenik.IdSmera}, idSkole = {ucenik.IdSkole}\n" +
                                                 $"WHERE ID = {ucenik.Id};", connection);
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Komunizam>(new Komunizam(ex.Message));
            }
        }
    }
}