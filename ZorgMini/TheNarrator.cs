using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class TheNarrator
    {
        public List<Item> Inventory { get; set; }

        public int RoomTracker { get; set; }

        public List<string> UserCommand { get; set; }

        public List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };

        Rooms rooms = new Rooms();
        
        private void RoomBreakdow()
        {
            rooms.adventureMap.Select(x => x.DoorsInRoom);
        }
        

        public string TellNarrator(string userIn)
        {

            string[] command = userIn.ToUpper().Split(' ');

            switch (command[0])
            {
                case "HELP":
                    string userKeywords = "";

                    foreach (var word in Keywords)
                    {
                        userKeywords += word + ", ";
                    }

                    return userKeywords;

                case "INVENTORY":
                    
                    string userInventory = "";

                    foreach (var item in Inventory)
                    {
                        userInventory += item + ", ";
                    }
                    return userInventory;
                        
                case "LOOK":

                    string lookAtRoom = rooms.adventureMap[RoomTracker].RoomDescription;
                    return lookAtRoom;

                case "GO":

                    

                    
            }


        }

    }
}
