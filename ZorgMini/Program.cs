using System;


namespace ZorgMini
{
    class Program
    {
        static bool ExitProgram = false;

        static void Main(string[] args)
        {
            try
            {
                RunZorgMini();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }       
        }
        static TheNarrator narrator = new TheNarrator();

        static void RunZorgMini()
        {
            Console.WriteLine(narrator.LookAtRoom());
            while (ExitProgram != true)
            {
                Console.WriteLine(narrator.TellNarrator(Console.ReadLine()));

                ExitProgram = narrator.FinalRoom();

            }
            Console.WriteLine("\n\tPress any key to exit.");
            Console.ReadKey();
        }
    }
}
