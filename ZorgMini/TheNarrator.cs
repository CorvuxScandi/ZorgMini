using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class TheNarrator
    {
        public List<Item> Inventory { get; set; }

        public int RoomTracker = 1;

        public List<string> UserCommand { get; set; }

        public List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };
        
        Rooms room = new Rooms();


        private Rooms GetRoom()
        {
            return room.adventureMap[RoomTracker -1];
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

                    string lookAtRoom = GetRoom().RoomDescription;
                    return lookAtRoom;

                case "GO":
                    string outcome = "";
                    try
                    {
                        RoomTracker = Convert.ToInt32(GetRoom().DoorsInRoom.Where(x => x.Orientation == command[1]).Select(x => x.GoTo));
                        outcome = $"You walk trough the {command[1].ToLower()} door.";
                    }
                        catch (Exception)
                    {
                        outcome = "You cannot go that way.";
                        
                    }
                    return outcome;

                case "PICK UP":

                    string outcome2 = "";
                    if (UserCommand.Contains("KEY"))
                    {
                        try
                        {
                            Inventory.Add((Item)GetRoom().ItemsInRoom.Select(x => x.Description == command[1]));
                            GetRoom().ItemsInRoom.Remove((Item)GetRoom().ItemsInRoom.Select(x => x.Description == command[1]));

                            outcome2 = $"You pick up the {command[1]} {command[2]}";
                        }
                        catch (Exception)
                        {
                            outcome2 = "There is no sutch KEY in this room";
                        }
                    }
                        return outcome2;
                    
            }


        }

    }
}
