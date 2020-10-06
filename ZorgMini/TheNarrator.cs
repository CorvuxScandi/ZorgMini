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
        private List<Item> Inventory = new List<Item>();

        private int RoomTracker = 1;

        private List<string> UserCommand { get; set; }

        private List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };

        private List<Rooms> adventureMap = new List<Rooms>()
        {
            new Rooms() //first Room
            {
                RoomID = 1,

                ItemsInRoom = new List<Item>()
                {
                   new Item("table", "wooden"),
                   new Item("chair", "wooden"),
                   new Item("KEY", "rusty", 14) {CanBePickedUp = true}
                },

                DoorsInRoom = new List<Doors>()
                {
                    new Doors(14, true, "NORTH", 2)
                },


                RoomDescription = "You are in a damp and low lit room made of stone."

            }

            //new Rooms() //second Room
            //{
            //    RoomID = 2,


            //}

        };

        private Doors Getpath()
        {
            Doors path = new Doors();
            
            foreach (var door in GetRoom().DoorsInRoom)
            {
                if(door.Orientation == UserCommand[1])
                {
                    path = door;
                }
            }
            return path;
        }

        private Rooms GetRoom()
        {
            return adventureMap[RoomTracker - 1];
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
                        inventory += item.Description + ", " + item.Name + ' ';
                    }
                    narratorOut = inventory;
                    break;

                case "LOOK":
                   
                    narratorOut = LookAtRoom();
                    break;

                case "GO":
                    Doors path = Getpath();

                    if(path.Locked == false && path.Orientation != null)
                    {
                        RoomTracker = path.GoTo;
                        narratorOut = $"You go {UserCommand[1]}";
                    }
                    else if(path.Locked == true && path.Orientation != null)
                    {
                        narratorOut = "Ther's a locked door in your way.";
                    }
                    else
                    {
                        narratorOut = "You can't go in that direction.";
                    }
                    break;

                case "PICK":
                    Item thing = GetRoom().ItemsInRoom.First(x => x.Name == UserCommand[3]
                                              && x.Description.ToUpper() == UserCommand[2]);

                    if(GetRoom().ItemsInRoom.Contains(thing) && thing.CanBePickedUp == true)
                    {
                        Inventory.Add(thing);
                        narratorOut = $"You pick up the {thing.Name}.";
                    }
                    else
                    {
                        narratorOut = $"You can not pick up that. Beacuse of unknown reasons.";
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
                whatYouSee += item.Description + ' ' + item.Name + " ,";
            }

            return whatYouSee;
        }

        public bool FinalRoom()
        {
            bool end = false;
            if(RoomTracker > 8)
            {
                end = true;
            }

            return end;
        }

    }
        
}
