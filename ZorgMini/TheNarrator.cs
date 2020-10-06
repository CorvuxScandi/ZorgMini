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
        private List<Item> Inventory { get; set; }

        private int RoomTracker = 1;

        private List<string> UserCommand { get; set; }

        private List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };

        private Rooms room = new Rooms();

        private Rooms GetRoom()
        {
            return room.adventureMap[RoomTracker - 1];
        }

        public string TellNarrator(string userIn)
        {
            UserCommand = userIn.ToUpper().Split(' ').ToList();
            string narratorOut = "Unknown command! Type \"help\" if you want some keywords to use.";

            switch (UserCommand[0])
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
                    string inventory = "";
                    foreach (var item in Inventory)
                    {
                        inventory += item + ", ";
                    }
                    narratorOut = inventory;
                    break;

                case "LOOK":
                   
                    narratorOut = LookAtRoom();
                    break;

                case "GO":

                    Doors path = (Doors)GetRoom().DoorsInRoom.Where(x => x.Orientation == UserCommand[1]);

                    if(path.Locked == false)
                    {
                        RoomTracker = path.GoTo;
                        narratorOut = $"You go {UserCommand[1]}";
                    }
                    else if(path.Locked == true)
                    {
                        narratorOut = "Ther's a locked door in your way.";
                    }
                    else
                    {
                        narratorOut = "You can't go in that direction.";
                    }
                    break;

                case "PICK UP":

                    if (GetRoom().ItemsInRoom.Contains((Item)GetRoom().ItemsInRoom.Where(x => x.Name == UserCommand[2]
                                                         && x.CanBePickedUp == true)))
                    {
                        Inventory.Add((Item)GetRoom().ItemsInRoom.Where(x => x.Name == UserCommand[2]));
                        narratorOut = $"You pick up the {UserCommand[1]} {UserCommand[2]}.";
                    }
                    else
                    {
                        narratorOut = $"You cannot pick up the {UserCommand[1]} {UserCommand[2]}.";
                    }
                    break;

                case "USE":

                    if (UserCommand.Contains("KEY") && UserCommand.Contains("DOOR"))
                    {
                        int key = Convert.ToInt32(Inventory.Where(x => x.Name == "KEY" && x.Description == UserCommand[1])
                                                           .Select(x => x.ItemID));
                        int door = Convert.ToInt32(GetRoom().DoorsInRoom.Where(x => x.Orientation == UserCommand[4])
                            .Select(x => x.DoorID));

                        if (key == door)
                        {
                            Doors locked = (Doors)GetRoom().DoorsInRoom.Where(x => x.DoorID == door);
                            locked.Locked = false;
                            narratorOut = $"You unlocked the {UserCommand[3]} door.";
                        }
                    }
                    break;
            }

            return narratorOut;
        }

        public string LookAtRoom()
        {
            string whatYouSee;

            whatYouSee = GetRoom().RoomDescription + "\nAnd in this room you can see ";

            foreach (var item in GetRoom().ItemsInRoom)
            {
                whatYouSee += item.Description + ' ';
                whatYouSee += item.Name + '.';
            }

            return whatYouSee;
        }

    }
        
}
