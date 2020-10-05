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
            string narratorOut = "";

            switch (command[0])
            {
                case "HELP":

                    string userKeywords = "";
                    foreach (var word in Keywords)
                    {
                        userKeywords += word + ", ";
                    }

                    narratorOut = userKeywords;
                    break;

                case "INVENTORY":
                    
                    foreach (var item in Inventory)
                    {
                        narratorOut += item + ", ";
                    }
                    
                    break;
                        
                case "LOOK":

                    string lookAtRoom = GetRoom().RoomDescription;
                    narratorOut = lookAtRoom;
                    break;

                case "GO":
                    
                    try
                    {
                        RoomTracker = Convert.ToInt32(GetRoom().DoorsInRoom.Where(x => x.Orientation == command[1]).Select(x => x.GoTo));
                        narratorOut = $"You walk trough the {command[1].ToLower()} door.";
                    }
                        catch (Exception)
                    {
                        narratorOut = "You cannot go that way.";
                        
                    }
                    break;

                case "PICK UP":


                    if (GetRoom().ItemsInRoom.Contains((Item)GetRoom().ItemsInRoom.Where(x => x.Name == command[2]
                                                         && x.CanBePickedUp == true)))
                    {
                        Inventory.Add((Item)GetRoom().ItemsInRoom.Where(x => x.Name == command[2]));
                        narratorOut = $"You pick up the {command[1]} {command[2]}.";
                    }
                    else
                    {
                        narratorOut = $"You cannot pick up the {command[1]} {command[2]}.";
                    }
                    break;

                case "USE":

                    if (Inventory.Contains((Item)Inventory.Where(x => x.Description == command[1] && x.Name == command[2])))
                    {
                        if (command[3] == "ON")
                        {
                            int ID1 = Convert.ToInt32(Inventory.Where(x => x.Name == command[2]).Select(x => x.CanBeUsedOn));
                            int ID2 = Convert.ToInt32(Inventory.Where(x => x.Name == command[5]).Select(x => x.ItemID));

                            if (ID1 == ID2)
                            {

                            }
                        }
                    }


                    break;
                    
            }

            return narratorOut;
        }

    }
}
