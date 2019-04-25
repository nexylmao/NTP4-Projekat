using System;
using Newtonsoft.Json;
using SkolskiSistemCommon;

namespace SkolskiSistemClient
{
    public static class SmerInterface
    {
        private static void PrintMenu(Smer smerPointer)
        {
            Console.WriteLine("========== M E N U ==========");
            if (smerPointer != null)
            {
                Console.WriteLine("Zapamcena skola : ");
                Console.WriteLine(JsonConvert.SerializeObject(smerPointer, Formatting.Indented));
            }

            Console.WriteLine("a) Ucitaj sve smerove");
            Console.WriteLine("b) Zapamti smer");
            Console.WriteLine("c) Kreiraj smer");
            Console.WriteLine("d) Edituj smer");
            Console.WriteLine("x) Nazad");
        }

        private static void CreateSmer(IService methods, ref Smer smerPointer)
        {
            Smer smer = new Smer();
            Console.Write("Unesite naziv smera\n> ");
            smer.Naziv = Console.ReadLine();

            smer = methods.PostSmer(smer);
            if (smer == null)
            {
                Console.WriteLine("Nesto nije poslo kako treba.");
            }

            smerPointer = smer;
            Console.WriteLine("Kreiran smer {0}.", smerPointer.Naziv);
            Console.ReadKey(true);
        }

        private static void EditSmer(IService methods, ref Smer smerPointer)
        {
            if (smerPointer == null)
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
                    PrintEditMenu(smerPointer);
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    switch (input)
                    {
                        case 'a':
                            Console.Write("Unesite novi naziv\n> ");
                            smerPointer.Naziv = Console.ReadLine();
                            break;
                        case 'y':
                            methods.PutSmer(smerPointer);
                            Console.WriteLine("Promene sacuvane.");
                            Console.ReadKey(true);
                            break;
                        case 'n':
                            smerPointer = methods.GetSmer(smerPointer.Id);
                            Console.WriteLine("Promene odbacene.");
                            Console.ReadKey(true);
                            break;
                    }
                } while (input != 'y' && input != 'n');
            }
        }

        private static void PrintEditMenu(Smer smerPointer)
        {
            Console.WriteLine("========== E D I T ==========");
            if (smerPointer != null)
            {
                Console.WriteLine("Zapamcen smer : ");
                Console.WriteLine(JsonConvert.SerializeObject(smerPointer, Formatting.Indented));
            }

            Console.WriteLine("a) Naziv");
            Console.WriteLine("y) Snimi");
            Console.WriteLine("n) Odbaci");
        }

        public static void Initialize(IService methods, ref Smer smerPointer)
        {
            char input;
            do
            {
                Console.Clear();
                PrintMenu(smerPointer);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (input)
                {
                    case 'a':
                        Console.WriteLine(JsonConvert.SerializeObject(methods.GetSmerovi(), Formatting.Indented));
                        Console.WriteLine("Pritisnite bilo sta da nastavite...");
                        Console.ReadKey(true);
                        break;
                    case 'b':
                        Console.Write("Unesite id smera\n> ");
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

                        var smer = methods.GetSmer(id);
                        if (smer == null)
                        {
                            Console.WriteLine("Ne postoji smer sa tim id-om!");
                            break;
                        }

                        smerPointer = smer;
                        Console.WriteLine("Zapamcena smer {0}", smer.Naziv);
                        break;
                    case 'c':
                        CreateSmer(methods, ref smerPointer);
                        break;
                    case 'd':
                        EditSmer(methods, ref smerPointer);
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