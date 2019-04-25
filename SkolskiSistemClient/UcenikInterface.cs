using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SkolskiSistemCommon;

namespace SkolskiSistemClient
{
    public class UcenikInterface
    {
        private static void PrintMenu(Skola skolaPointer, Smer smerPointer, Ucenik ucenikPointer)
        {
            Console.WriteLine("========== M E N U ==========");
            if (skolaPointer != null)
            {
                Console.WriteLine("Zapamcena skola : ");
                Console.WriteLine(JsonConvert.SerializeObject(skolaPointer, Formatting.Indented));
            }
            if (smerPointer != null)
            {
                Console.WriteLine("Zapamcen smer : ");
                Console.WriteLine(JsonConvert.SerializeObject(smerPointer, Formatting.Indented));
            }
            if (ucenikPointer != null)
            {
                Console.WriteLine("Zapamcen ucenik : ");
                Console.WriteLine(JsonConvert.SerializeObject(ucenikPointer, Formatting.Indented));
            }

            Console.WriteLine("a) Ucitaj sve ucenike");
            Console.WriteLine("b) Zapamti ucenika");
            Console.WriteLine("c) Kreiraj ucenika");
            Console.WriteLine("d) Edituj ucenika");
            Console.WriteLine("x) Nazad");
        }

        private static void CreateUcenik(IService methods, Skola skolaPointer, Smer smerPointer, ref Ucenik ucenikPointer)
        {
            if (skolaPointer == null)
            {
                Console.WriteLine("Morate izabrati neku skolu.");
                return;
            }
            if (smerPointer == null)
            {
                Console.WriteLine("Morate izabrati neki smer.");
                return;
            }

            Ucenik ucenik = new Ucenik();
            Console.Write("Unesite ime ucenika\n> ");
            ucenik.Ime = Console.ReadLine();
            Console.Write("Unesite prezime ucenika\n> ");
            ucenik.Prezime = Console.ReadLine();
            Console.Write("Unesite jmbg ucenika (13 cifara)\n> ");
            string input = Console.ReadLine();
            if (!Regex.IsMatch(input, "[0-9]{13}"))
            {
                Console.WriteLine("Jmbg mora da bude 13 cifara dugacak.");
                return;
            }

            ucenik.Jmbg = input;


            Console.Write("Unesite datum rodjenja ucenika (gggg-mm-dd)\n> ");
            input = Console.ReadLine();
            if (!Regex.IsMatch(input, "[0-9]{4}-[0-9]{2}-[0-9]{2}"))
            {
                Console.WriteLine("Datum nije ispravno formatiran.");
                return;
            }

            var parts = input.Split('-');
            var year = int.Parse(parts[0]);
            if (year < 1900 || year > DateTime.Now.Year)
            {
                Console.WriteLine("Godina mora biti izmedju 1990 i {0}.", DateTime.Now.Year);
                return;
            }
            var month = int.Parse(parts[1]);
            if (month > 12 || month < 1)
            {
                Console.WriteLine("Mesec mora biti izmedju 01 i 12.");
                return;
            }

            var day = int.Parse(parts[2]);
            if (day < 1 || day > 31)
            {
                Console.WriteLine("Dan mora biti izmedju 01 i 31.");
                return;
            }

            ucenik.DatumRodjenja = DateTime.Parse(input);

            Console.Write("Unesite adresu ucenika\n> ");
            ucenik.Adresa = Console.ReadLine();
            Console.Write("Unesite mobilni telefon ucenika\n> ");
            input = Console.ReadLine();
            if (input.Length > 15)
            {
                Console.WriteLine("Broj telefona ne moze biti toliko dugacak.");
                return;
            }
            ucenik.MobilniTelefon = input;

            ucenik.IdSkole = skolaPointer.Id;
            ucenik.IdSmera = smerPointer.Id;

            ucenik = methods.PostUcenik(ucenik);
            if (ucenik == null)
            {
                Console.WriteLine("Nesto nije poslo kako treba.");
            }

            ucenikPointer = ucenik;
            Console.WriteLine("Kreiran ucenik {0} {1}.", ucenikPointer.Ime, ucenikPointer.Prezime);
            Console.ReadKey(true);
        }

        private static void EditUcenik(IService methods, ref Skola skolaPointer, ref Smer smerPointer, ref Ucenik ucenikPointer)
        {
            if (ucenikPointer == null)
            {
                Console.WriteLine("Nemate zapamcenog ucenika!");
                Console.ReadKey(true);
            }
            else
            {
                char input;
                do
                {
                    Console.Clear();
                    PrintEditMenu(skolaPointer, smerPointer, ucenikPointer);
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    switch (input)
                    {
                        case 'a':
                            Console.Write("Unesite novo ime\n> ");
                            ucenikPointer.Ime = Console.ReadLine();
                            break;
                        case 'b':
                            Console.Write("Unesite novo prezime\n> ");
                            ucenikPointer.Prezime = Console.ReadLine();
                            break;
                        case 'c':
                            Console.Write("Unesite datum rodjenja ucenika (gggg-mm-dd)\n> ");
                            string i = Console.ReadLine();
                            if (!Regex.IsMatch(i, "[0-9]{4}-[0-9]{2}-[0-9]{2}"))
                            {
                                Console.WriteLine("Datum nije ispravno formatiran.");
                                Console.ReadKey(true);
                                break;
                            }

                            var parts = i.Split('-');
                            var year = int.Parse(parts[0]);
                            if (year < 1900 || year > DateTime.Now.Year)
                            {
                                Console.WriteLine("Godina mora biti izmedju 1990 i {0}.", DateTime.Now.Year);
                                Console.ReadKey(true);
                                break;
                            }
                            var month = int.Parse(parts[1]);
                            if (month > 12 || month < 1)
                            {
                                Console.WriteLine("Mesec mora biti izmedju 01 i 12.");
                                Console.ReadKey(true);
                                break;
                            }

                            var day = int.Parse(parts[2]);
                            if (day < 1 || day > 31)
                            {
                                Console.WriteLine("Dan mora biti izmedju 01 i 31.");
                                Console.ReadKey(true);
                                break;
                            }

                            ucenikPointer.DatumRodjenja = DateTime.Parse(i);
                            break;
                        case 'd':
                            Console.Write("Unesite novu adresu\n> ");
                            ucenikPointer.Adresa = Console.ReadLine();
                            break;
                        case 'e':
                            Console.Write("Unesite novi telefon\n> ");
                            string o = Console.ReadLine();
                            if (o.Length > 15)
                            {
                                Console.WriteLine("Telefon ne moze biti toliko dugacak.");
                                Console.ReadKey(true);
                                break;
                            }
                            ucenikPointer.MobilniTelefon = o;
                            break;
                        case 'f':
                            if (skolaPointer == null)
                            {
                                Console.WriteLine("Vi nemate zapamcenu skolu.");
                                Console.ReadKey(true);
                                break;
                            }
                            if (skolaPointer.Id == ucenikPointer.IdSkole)
                            {
                                Console.WriteLine("Taj ucenik vec pohadja zapamcenu skolu.");
                                Console.ReadKey(true);
                                break;
                            }

                            ucenikPointer.IdSkole = skolaPointer.Id;
                            break;
                        case 'g':
                            if (smerPointer == null)
                            {
                                Console.WriteLine("Vi nemate zapamcen smer.");
                                Console.ReadKey(true);
                                break;
                            }
                            if (smerPointer.Id == ucenikPointer.IdSmera)
                            {
                                Console.WriteLine("Taj ucenik vec pohadja zapamceni smer.");
                                Console.ReadKey(true);
                                break;
                            }

                            ucenikPointer.IdSmera = smerPointer.Id;
                            break;
                        case 'y':
                            methods.PutUcenik(ucenikPointer);
                            Console.WriteLine("Promene sacuvane.");
                            Console.ReadKey(true);
                            break;
                        case 'n':
                            ucenikPointer = methods.GetUcenik(ucenikPointer.Id);
                            Console.WriteLine("Promene odbacene.");
                            Console.ReadKey(true);
                            break;
                    }
                } while (input != 'y' && input != 'n');
            }
        }

        private static void PrintEditMenu(Skola skolaPointer, Smer smerPointer, Ucenik ucenikPointer)
        {
            Console.WriteLine("========== E D I T ==========");
            if (ucenikPointer != null)
            {
                Console.WriteLine("Zapamcen ucenik : ");
                Console.WriteLine(JsonConvert.SerializeObject(ucenikPointer, Formatting.Indented));
            }

            Console.WriteLine("a) Ime");
            Console.WriteLine("b) Prezime");
            Console.WriteLine("c) Datum Rodjenja");
            Console.WriteLine("d) Adresa");
            Console.WriteLine("e) Telefon");
            if (skolaPointer == null || skolaPointer.Id == ucenikPointer.IdSkole)
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("f) Id Skole" + (skolaPointer != null && skolaPointer.Id != ucenikPointer.IdSkole ? string.Format(" (Postaviti na {0} (ID: {1}))", skolaPointer.Naziv, skolaPointer.Id) : ""));
            Console.ResetColor();
            if (smerPointer == null || smerPointer.Id == ucenikPointer.IdSmera)
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("g) Id Smera" + (smerPointer != null && smerPointer.Id != ucenikPointer.IdSmera ? string.Format(" (Postaviti na {0} (ID: {1}))", smerPointer.Naziv, smerPointer.Id) : ""));
            Console.ResetColor();
            Console.WriteLine("y) Snimi");
            Console.WriteLine("n) Odbaci");
        }

        public static void Initialize(IService methods, ref Skola skolaPointer, ref Smer smerPointer, ref Ucenik ucenikPointer)
        {
            char input;
            do
            {
                Console.Clear();
                PrintMenu(skolaPointer, smerPointer, ucenikPointer);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (input)
                {
                    case 'a':
                        Console.WriteLine(JsonConvert.SerializeObject(methods.GetUcenici(), Formatting.Indented));
                        Console.WriteLine("Pritisnite bilo sta da nastavite...");
                        Console.ReadKey(true);
                        break;
                    case 'b':
                        Console.Write("Unesite id ucenika\n> ");
                        int id;
                        if (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Morate uneti broj.");
                            break;
                        }

                        if (id < 0)
                        {
                            Console.WriteLine("Morate uneti pozitivan broj.");
                            break;
                        }

                        var ucenik = methods.GetUcenik(id);
                        if (ucenik == null)
                        {
                            Console.WriteLine("Ne postoji ucenik sa tim id-om!");
                            break;
                        }

                        ucenikPointer = ucenik;
                        Console.WriteLine("Zapamcen ucenik {0} {1}", ucenik.Ime, ucenik.Prezime);
                        break;
                    case 'c':
                        CreateUcenik(methods, skolaPointer, smerPointer, ref ucenikPointer);
                        break;
                    case 'd':
                        EditUcenik(methods, ref skolaPointer, ref smerPointer, ref ucenikPointer);
                        break;
                    case 'x':
                        break;
                    default:
                        Console.WriteLine("Ta komanda ne postoji!");
                        break;
                }
            } while (input != 'x');
        }
    }
}