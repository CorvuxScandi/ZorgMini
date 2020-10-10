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
                    foreach (var inventoryItem in Inventory)
                    {
                        inventory += inventoryItem.Description + ", " + inventoryItem.Name + ' ';
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

                    if (path.Locked == false && path.Orientation != null)
                    {
                        adventureMap.RoomTracker = path.GoTo;
                        narratorOut = $"You go {UserCommand[1]}";
                    }
                    else if (path.Locked == true && path.Orientation != null)
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

                    if (GetRoom().ItemsInRoom.Contains(thing) && thing.CanBePickedUp == true)
                    {
                        Inventory.Add(thing);
                        adventureMap.map.First(r => r.RoomID == GetRoom().RoomID).ItemsInRoom.Remove(thing);
                        narratorOut = $"You picked up a {thing.Description} {thing.Name.ToLower()}.";
                    }
                    else
                    {
                        narratorOut = $"You can not pick up that. Beacuse of unknown reasons.";
                    }
                    break;

                case "USE":

                    Item item = null;

                   if(Inventory.Contains((Item)Inventory.Where(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item = Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                   else if(GetRoom().ItemsInRoom.Contains((Item)GetRoom().ItemsInRoom.Where(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else
                    {
                        narratorOut = "There's no such item in your pocket.";
                        break;
                    }

                    Item item2 = null;
                    Doors door = null;

                    if (Inventory.Contains((Item)Inventory.Where(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item2 = Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else if (GetRoom().ItemsInRoom.Contains((Item)GetRoom().ItemsInRoom.Where(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item2 = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else if (GetRoom().DoorsInRoom.Contains((Doors)GetRoom().DoorsInRoom.Where(d => d.Orientation == UserCommand[4])))
                    {
                        door = GetRoom().DoorsInRoom.FirstOrDefault(d => d.Orientation == UserCommand[4]);

                    }
                    else 
                    {
                        narratorOut = "You stumble in confution and fail to complete your task. Be more clear to yourself on what you want to do.";
                    }


                    if (item.CanBeUsedOn == door.DoorID)
                    {
                        Inventory.Remove(item);
                        adventureMap.map.First(r => r.DoorsInRoom == GetRoom().DoorsInRoom).DoorsInRoom.First(d => d.DoorID == door.DoorID).Locked = false;
                        narratorOut = "The key fits and you open the door.";
                    }
                    else if(item.CanBeUsedOn == item2.ItemID)
                    {
                        adventureMap.ItemInteraction(item, item2);
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
