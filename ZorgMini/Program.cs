﻿using System;


namespace ZorgMini
{
    class Program
    {
        static bool ExitProgram = false;



        static void Main(string[] args)
        {
            RunZorgMini();

        }
        static TheNarrator narrator = new TheNarrator();

        static void RunZorgMini()
        {
            Console.WriteLine(narrator.LookAtRoom());
            while (ExitProgram != true)
            {
                Storyteller();

            }
            Console.WriteLine("\n\tPress any key to exit.");
            Console.ReadKey();
        }

        static void Storyteller()
        {

            Console.WriteLine(narrator.TellNarrator(Console.ReadLine()));

            ExitProgram = narrator.FinalRoom();
        }
    }
}
