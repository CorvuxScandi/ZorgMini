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
        public List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };

        private AdventureMap adventureMap = new AdventureMap();

        private List<Item> Inventory = new List<Item>(); //Players invnetory

        private List<string> UserCommand { get; set; }

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
                    if (inventory == "")
                    {
                        inventory = "Your inventory is empty.";
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
                        adventureMap.RoomTracker = path.GoTo;
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
                        narratorOut = $"You picked up a {thing.Description} {thing.Name.ToLower()}.";
                    }
                    else
                    {
                        narratorOut = $"You can not pick up that. Beacuse of unknown reasons.";
                    }
                    break;

                case "USE":

                    if (UserCommand.Contains("KEY") && UserCommand.Contains("DOOR"))
                    {
                        int key = Inventory.FirstOrDefault(x => x.Name == "KEY" && x.Description.ToUpper() == UserCommand[1]).CanBeUsedOn;

                        int door = GetRoom().DoorsInRoom.FirstOrDefault(x => x.Orientation == UserCommand[4]).DoorID;
                            
                        if (key == door)
                        {
                            GetRoom().DoorsInRoom.First(x => x.DoorID == door).Locked = false;
                           
                            narratorOut = $"You unlocked the {UserCommand[4].ToLower()} door.";
                        }
                        else
                        {
                            narratorOut = "That key wont fit.";
                        }
                    }
                    else
                    {
                        narratorOut = "You want to use what on what?";
                    }
                    break;
            }

            return narratorOut;
        }

        public Rooms GetRoom()
        {
            Rooms room = adventureMap.map[adventureMap.RoomTracker - 1];

            return room;
        }

        private Doors Getpath()
        {
            Doors path = new Doors();

            foreach (var door in GetRoom().DoorsInRoom)
            {
                if (door.Orientation == UserCommand[1])
                {
                    path = door;
                }
            }
            return path;
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
            if (adventureMap.RoomTracker > 8)
            {
                end = true;
            }

            return end;
        }

    }
        
}
