using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    class Program
    {
        static bool exitProgram = false;
        
        static void Main(string[] args)
        {
            RunZorgMini();
            
        }
        TheNarrator narrator = new TheNarrator();

        static void RunZorgMini()
        {
            while (exitProgram != true)
            {
                Storyteller();
            }
            Console.WriteLine("\n\tPress any key to exit.");
            Console.ReadKey();
        }
        
        static void Storyteller()
        {
            Console.WriteLine();
            PlayerInput();

        }

        static void PlayerInput()
        {
              =
           new UserCommand(Console.ReadLine().ToUpper());  


        }
    }
}
