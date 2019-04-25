using System;
using Newtonsoft.Json;
using SkolskiSistemCommon;

namespace SkolskiSistemClient
{
    public static class SkolaInterface
    {
        private static void PrintMenu(Skola skolaPointer)
        {
            Console.WriteLine("========== M E N U ==========");
            if (skolaPointer != null)
            {
                Console.WriteLine("Zapamcena skola : ");
                Console.WriteLine(JsonConvert.SerializeObject(skolaPointer, Formatting.Indented));
            }
            Console.WriteLine("a) Ucitaj sve skole");
            Console.WriteLine("b) Zapamti skolu");
            Console.WriteLine("c) Kreiraj skolu");
            Console.WriteLine("d) Edituj skolu");
            Console.WriteLine("x) Nazad");
        }

        private static void CreateSkola(IService methods, ref Skola skolaPointer)
        {
            Skola skola = new Skola();
            Console.Write("Unesite naziv skole\n> ");
            skola.Naziv = Console.ReadLine();
            Console.Write("Unesite adresu skole\n> ");
            skola.Adresa = Console.ReadLine();
            Console.Write("Unesite telefon skole\n> ");
            skola.Telefon = Console.ReadLine();
            Console.Write("Unesite email skole\n> ");
            skola.Email = Console.ReadLine();

            skola = methods.PostSkola(skola);
            if (skola == null)
            {
                Console.WriteLine("Nesto nije poslo kako treba.");
            }

            skolaPointer = skola;
            Console.WriteLine("Kreirana skola {0}.", skolaPointer.Naziv);
            Console.ReadKey(true);
        }

        private static void EditSkola(IService methods, ref Skola skolaPointer)
        {
            if (skolaPointer == null)
            {
                Console.WriteLine("Nemate zapamcenu skolu!");
                Console.ReadKey(true);
            }
            else
            {
                char input;
                do
                {
                    Console.Clear();
                    PrintEditMenu(skolaPointer);
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    switch (input)
                    {
                        case 'a':
                            Console.Write("Unesite novi naziv\n> ");
                            skolaPointer.Naziv = Console.ReadLine();
                            break;
                        case 'b':
                            Console.Write("Unesite novu adresu\n> ");
                            skolaPointer.Adresa = Console.ReadLine();
                            break;
                        case 'c':
                            Console.Write("Unesite novi telefon\n> ");
                            skolaPointer.Telefon = Console.ReadLine();
                            break;
                        case 'd':
                            Console.Write("Unesite novi email\n> ");
                            skolaPointer.Email = Console.ReadLine();
                            break;
                        case 'y':
                            methods.PutSkola(skolaPointer);
                            Console.WriteLine("Promene sacuvane.");
                            Console.ReadKey(true);
                            break;
                        case 'n':
                            skolaPointer = methods.GetSkola(skolaPointer.Id);
                            Console.WriteLine("Promene odbacene.");
                            Console.ReadKey(true);
                            break;
                    }
                } while (input != 'y' && input != 'n');
            }
        }

        private static void PrintEditMenu(Skola skolaPointer)
        {
            Console.WriteLine("========== E D I T ==========");
            if (skolaPointer != null)
            {
                Console.WriteLine("Zapamcena skola : ");
                Console.WriteLine(JsonConvert.SerializeObject(skolaPointer, Formatting.Indented));
            }
            Console.WriteLine("a) Naziv");
            Console.WriteLine("b) Adresa");
            Console.WriteLine("c) Telefon");
            Console.WriteLine("d) Email");
            Console.WriteLine("y) Snimi");
            Console.WriteLine("n) Odbaci");
        }

        public static void Initialize(IService methods, ref Skola skolaPointer)
        {
            char input;
            do
            {
                Console.Clear();
                PrintMenu(skolaPointer);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (input)
                {
                    case 'a':
                        Console.WriteLine(JsonConvert.SerializeObject(methods.GetSkole(), Formatting.Indented));
                        Console.WriteLine("Pritisnite bilo sta da nastavite...");
                        Console.ReadKey(true);
                        break;
                    case 'b':
                        Console.Write("Unesite id skole\n> ");
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

                        var skola = methods.GetSkola(id);
                        if (skola == null)
                        {
                            Console.WriteLine("Ne postoji skola sa tim id-om!");
                            break;
                        }

                        skolaPointer = skola;
                        Console.WriteLine("Zapamcena skola {0}", skola.Naziv);
                        break;
                    case 'c':
                        CreateSkola(methods, ref skolaPointer);
                        break;
                    case 'd':
                        EditSkola(methods, ref skolaPointer);
                        break;
                    case 'x':
                        Console.WriteLine("Goodbye.");
                        break;
                    default:
                        Console.WriteLine("Ta komanda ne postoji!");
                        break;
                }
            } while (input != 'x');
        }
    }
}