using System.Collections.Generic;
using System.Data;
using System.Linq;


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
            string narratorOut = "\n\tUnknown command! Type \"help\" if you want some keywords to use.";

            switch (UserCommand[0])
            {
                case "HELP":

                    string userKeywords = "\n\t";
                    foreach (var word in Keywords)
                    {
                        userKeywords += word + ", ";
                    }

                    narratorOut = userKeywords + "\n";
                    break;

                case "INVENTORY":
                    string inventory = "";
                    foreach (var inventoryItem in Inventory)
                    {
                        inventory += inventoryItem.Description + ' ' + inventoryItem.Name + " , ";
                    }
                    if (inventory == "")
                    {
                        inventory = "\n\tYour inventory is empty.";
                    }
                    narratorOut = inventory + "\n";
                    break;

                case "LOOK":

                    narratorOut = LookAtRoom() + "\n";
                    break;

                case "GO":
                    Door path = Getpath();

                    if (path.Locked == false && path.Orientation != null)
                    {
                        adventureMap.RoomTracker = path.GoTo;
                        narratorOut = $"\n\tYou go {UserCommand[1]}" + "\n";
                    }
                    else if (path.Locked == true && path.Orientation != null)
                    {
                        narratorOut = "\n\tTher's a locked door in your way." + "\n";
                    }
                    else
                    {
                        narratorOut = "\n\tYou can't go in that direction." + "\n";
                    }
                    break;

                case "PICK":
                    Item thing = GetRoom().ItemsInRoom.FirstOrDefault(x => x.Name == UserCommand[3]
                                              && x.Description.ToUpper() == UserCommand[2]);

                    if (GetRoom().ItemsInRoom.Contains(thing) && thing.CanBePickedUp == true)
                    {
                        Inventory.Add(thing);
                        adventureMap.map.FirstOrDefault(r => r.RoomID == GetRoom().RoomID).ItemsInRoom.Remove(thing);
                        narratorOut = $"\n\tYou picked up a {thing.Description} {thing.Name.ToLower()}.";
                    }
                    else
                    {
                        narratorOut = "\n\tYou can't pick up that. Beacuse of unknown reasons.";
                    }
                    break;
                    
                case "USE":

                    Item item = null;

                    if (Inventory.Contains(Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item = Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else if (GetRoom().ItemsInRoom.Contains(GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else
                    {
                        narratorOut = "\n\tThere's no such item in your pocket.";
                        break;
                    }
                    
                    Item item2 = null;
                    Door door = null;

                    if (Inventory.Contains(Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item2 = Inventory.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else if (GetRoom().ItemsInRoom.Contains(GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2])))
                    {
                        item2 = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description == UserCommand[1] && i.Name == UserCommand[2]);
                    }
                    else if (GetRoom().DoorsInRoom.Contains(GetRoom().DoorsInRoom.FirstOrDefault(d => d.Orientation == UserCommand[4])))
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
                        adventureMap.map.FirstOrDefault(r => r.DoorsInRoom == GetRoom().DoorsInRoom).DoorsInRoom.FirstOrDefault(d => d.DoorID == door.DoorID).Locked = false;
                        narratorOut = "\n\tThe key fits and you open the door.";
                    }
                    else if (item.CanBeUsedOn == item2.ItemID)
                    {
                        switch (item2.ItemID)
                        {
                            case 20:
                                Inventory.Remove(item);
                                Inventory.Remove(item2);
                                Inventory.Add(adventureMap.NonRoomItem.FirstOrDefault(i => i.ItemID == 30));
                                Inventory.Add(adventureMap.NonRoomItem.FirstOrDefault(i => i.ItemID == 40));
                                narratorOut = "\n\tYou open the box and inside it you see laying on a satin pillow, a golden key.";
                                break;
                        }
                    }
                    break;
            }

            return narratorOut;
        }

        public Room GetRoom()
        {
            Room room = adventureMap.map[adventureMap.RoomTracker - 1];


            return room;
        }

        private Door Getpath()
        {
            Door path = new Door();

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
            whatYouSee = "\n\t" + GetRoom().RoomDescription + "\n\tYou see ";

            foreach (var item in GetRoom().ItemsInRoom)
            {
                whatYouSee += item.Description + ' ' + item.Name + " , ";
            }
            whatYouSee += "\n\t";
            foreach (var door in GetRoom().DoorsInRoom)
            {
                whatYouSee += "a door to the " + door.Orientation + " , ";
            }
            return whatYouSee;
        }

        public bool FinalRoom()
        {
            bool end = false;
            if (adventureMap.RoomTracker > 3)
            {
                end = true;
            }
            return end;
        }

    }

}
