using System;
using Newtonsoft.Json;
using SkolskiSistemCommon;

namespace SkolskiSistemClient
{
    public static class Interface
    {
        private static Skola skolaPointer;

        private static Smer smerPointer;

        private static Ucenik ucenikPointer;

        private static void PrintMenu()
        {
            Console.WriteLine("========== M E N U ==========");
            Console.WriteLine("a) Skole");
            Console.WriteLine("b) Smerovi");
            Console.WriteLine("c) Ucenici");
            Console.WriteLine("d) Pokazivaci");
            Console.WriteLine("x) Izlaz");
        }

        public static void Initialize(IService methods)
        {
            char input;
            do
            {
                Console.Clear();
                PrintMenu();
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (input)
                {
                    case 'a':
                        SkolaInterface.Initialize(methods, ref skolaPointer);
                        break;
                    case 'b':
                        SmerInterface.Initialize(methods, ref smerPointer);
                        break;
                    case 'c':
                        UcenikInterface.Initialize(methods, ref skolaPointer, ref smerPointer, ref ucenikPointer);
                        break;
                    case 'd':
                        if (skolaPointer == null)
                            Console.WriteLine("Nema zapamcene skole.");
                        else
                            Console.WriteLine(JsonConvert.SerializeObject(skolaPointer, Formatting.Indented));

                        if (smerPointer == null)
                            Console.WriteLine("Nema zapamcenog smera.");
                        else
                            Console.WriteLine(JsonConvert.SerializeObject(smerPointer, Formatting.Indented));

                        if (ucenikPointer == null)
                            Console.WriteLine("Nema zapamcenog ucenika.");
                        else
                            Console.WriteLine(JsonConvert.SerializeObject(ucenikPointer, Formatting.Indented));

                        Console.WriteLine("Pritisnite bilo sta da nastavite...");
                        Console.ReadKey(true);
                        break;
                    case 'x':
                        Console.WriteLine("Dovidjenja.");
                        break;
                    default:
                        Console.WriteLine("Ta komanda ne postoji!");
                        break;
                }
            } while (input != 'x');
        }
    }
}