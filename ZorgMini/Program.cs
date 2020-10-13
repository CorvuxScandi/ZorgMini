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
                RunZorgMini();          //Start a loop in wich rest of the code is runned.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }       
        }
        static TheNarrator narrator = new TheNarrator();            //Creates a new instans of the games narrator who keeps track of the player 
                                                                    // and convayes a dialugue. 
        static void RunZorgMini()
        {
            Console.WriteLine(narrator.LookAtRoom());
            while (ExitProgram != true)                             //Loop for the program
            {
                Console.WriteLine(narrator.TellNarrator(Console.ReadLine()));

                ExitProgram = narrator.FinalRoom();

            }
            Console.WriteLine("\n\tPress any key to exit.");        //Final closing prompt so user can examine the games history
            Console.ReadKey();
        }
    }
}
