using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorgMini
{
    public class TheNarrator            //Class wich keep track on the player, 
    {
        private List<string> Keywords = new List<string>()
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP"
        };

        private AdventureMap adventureMap = new AdventureMap();

        private List<Item> Inventory = new List<Item>();    //Player invnetory

        private List<string> UserCommand { get; set; }      

        public string TellNarrator(string userIn)           //Cuts up the user input for easier handeling
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
                    foreach (var item1 in Inventory)
                    {
                        inventory += item1.Description + ' ' + item1.Name + " , ";
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
                    Door path = GetRoom().DoorsInRoom.FirstOrDefault(d => d.Orientation.ToUpper() == UserCommand[1]);

                    if (path.Locked == false && path != null)
                    {
                        adventureMap.RoomTracker = path.GoTo;
                        narratorOut = $"\n\tYou go {UserCommand[1]}" + "\n";
                        narratorOut += "\n\t" + LookAtRoom();
                    }
                    else if (path.Locked == true && path.Orientation != null)
                    {
                        narratorOut = "\n\tThere's a locked door in your way." + "\n";
                    }
                    else
                    {
                        narratorOut = "\n\tYou can't go in that direction." + "\n";
                    }
                    break;

                case "PICK": //"UP"
                    Item thing = GetRoom().ItemsInRoom.FirstOrDefault(x => x.Name.ToUpper() == UserCommand[3]
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
                    if (UserCommand.Count < 6) { narratorOut = "You couldn't decide what you wanted to do."; break; }

                    Item item = null;

                    if (Inventory.Contains(Inventory.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2])))
                    {
                        item = Inventory.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2]);
                    }
                    else if (GetRoom().ItemsInRoom.Contains(GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2])))
                    {
                        item = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2]);
                    }
                    else if (item == null)
                    {
                        narratorOut = "\n\tThere's no such item in your pocket.";
                        break;
                    }

                    Item inventoryItem = Inventory.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2]);
                    Item roomItem = GetRoom().ItemsInRoom.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[4] && i.Name.ToUpper() == UserCommand[5]);
                    Door door = door = GetRoom().DoorsInRoom.FirstOrDefault(d => d.Orientation.ToUpper() == UserCommand[4]);

                    if (door != null && item.CanBeUsedOn == door.DoorID)
                    {
                        Inventory.Remove(item);
                        adventureMap.map.FirstOrDefault(r => r.DoorsInRoom == GetRoom().DoorsInRoom).DoorsInRoom.FirstOrDefault(d => d.DoorID == door.DoorID).Locked = false;
                        narratorOut = "\n\tThe key fits and you open the door.";
                    }
                    else if (inventoryItem != null || roomItem != null)
                    {
                        narratorOut = ItemInteraction(inventoryItem, roomItem, item);
                    }
                    else
                    {
                        narratorOut = "You stumble in confution and fail to complete your task. Be more clear to yourself on what you want to do.";

                    }
                    break;
            }
            return narratorOut;
        }

        private Room GetRoom() //Returns the room the player is currently in
        {
            Room room = adventureMap.map[adventureMap.RoomTracker - 1];
            return room;
        }

        public string LookAtRoom() //Gets the descriptions, items and doors in the room the player occupies
        {
            string whatYouSee;
            whatYouSee = "\n\t" + GetRoom().RoomDescription + "\n\tYou see ";

            if (GetRoom().ItemsInRoom != null && GetRoom().DoorsInRoom != null)     //Constructs sentenses describing a room
            {
                foreach (var item in GetRoom().ItemsInRoom)
                {
                    whatYouSee += item.Description + ' ' + item.Name + " , ";
                }
                whatYouSee += "\n\t";
                foreach (var door in GetRoom().DoorsInRoom)
                {
                    whatYouSee += "a door to the " + door.Orientation + " , ";
                }
            }

            return whatYouSee;
        }

        public bool FinalRoom()         //checks if the player has reached one of the victory rooms
        {
            string answer;
            bool end = false;
            bool loop = true;

            if (adventureMap.RoomTracker > 3)
            {
                while (loop == true)
                {
                    Console.WriteLine("Do you want to continue? Y/N");      //Writes up a dilague asking player if they wants to play again. 
                    answer = Console.ReadLine().ToUpper();
                    if (answer == "N")
                    {
                        loop = false;
                        end = true;
                    }
                    else if (answer != "Y")
                    {
                        Console.WriteLine("Unknown Command.");
                    }
                    else
                    {
                        loop = false;
                    }

                }

            }

            return end;
        }
        
        private string ItemInteraction(Item inventoryItem, Item roomItem, Item item1)       //Returns the result of a event or interaction between two different items
        {
            string result = "";
            Item trial;
            if (inventoryItem != null)
            {
                trial = inventoryItem;
            }
            else
            {
                trial = roomItem;
            }

            switch (trial.ItemID)
            {
                case 20:            //Removes a key and a box and puts in a new key and opend box into user inventory
                    Inventory.Remove(item1);
                    Inventory.Remove(trial);
                    Inventory.Add(adventureMap.NonRoomItem.FirstOrDefault(i => i.ItemID == 30));
                    Inventory.Add(adventureMap.NonRoomItem.FirstOrDefault(i => i.ItemID == 40));
                    result = "\n\tYou open the box and inside it you see laying on a satin pillow, a golden key.";
                    break;
            }

            return result;
        }
    }
}
