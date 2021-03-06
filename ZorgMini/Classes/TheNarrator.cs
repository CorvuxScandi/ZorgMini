﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ZorgMini
{
    public class TheNarrator            //Class wich keep track on the player, 
    {
        private List<string> Keywords = new List<string>()  //A somple list of valid user command
        {"GO", "WEST", "NORTH", "EAST", "SOUTH", "USE",
            "LOOK", "HELP", "INVENTORY", "PICK UP", "DROP"
        };

        private AdventureMap adventureMap = new AdventureMap(); //Creates a new instance of the world map

        private List<Item> Inventory = new List<Item>();    //Player invnetory

        private List<string> UserCommand { get; set; }

        public string TellNarrator(string userIn)           //Cuts up the user input for easier handeling
        {
            UserCommand = userIn.ToUpper().Split(' ').ToList();
            string narratorOut = "\n\tUnknown command! Type \"help\" if you want some keywords to use."; //Base string if user input doesn't match with any of the conditions
                                                                                                         //Will be overwritten if condition are met
            switch (UserCommand[0])         //Handels the different reactions depending on user input
            {
                case "HELP":        //Writes out the list of keywords

                    string userKeywords = "\n\t";
                    foreach (var word in Keywords)
                    {
                        userKeywords += word + ", ";
                    }

                    narratorOut = userKeywords + "\n";
                    break;

                case "INVENTORY":       //Writes out the player inventory both Item name and description
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

                    narratorOut = LookAtRoom() + "\n"; //Calls on the method that describes the room that the player occupies
                    break;

                case "GO":
                    Door path = GetRoom().DoorsInRoom.FirstOrDefault(d => d.Orientation.ToUpper() == UserCommand[1]);

                    if (path != null)
                    {
                        if (path.Locked == false)           //Checks if the player have a free path to the next room and changes the roomtracker accordently
                        {
                            adventureMap.RoomTracker = path.GoTo;
                            narratorOut = $"\n\tYou go {UserCommand[1]}" + "\n";
                            narratorOut += "\n\t" + LookAtRoom();
                        }
                        else if (path.Locked == true)
                        {
                            narratorOut = "\n\tNuts! There's a locked door in your way." + "\n";
                        }
                    }
                    else
                    {
                        narratorOut = "\n\tYou stop yourself walking headfirst into a wall." + "\n";
                    }
                    break;

                case "PICK": //"UP"
                    Item thing = GetRoom().ItemsInRoom.FirstOrDefault(x => x.Name.ToUpper() == UserCommand[3]       //adds a valid item to the user inventory
                                              && x.Description.ToUpper() == UserCommand[2]);

                    if (GetRoom().ItemsInRoom.Contains(thing) && thing.CanBePickedUp == true)
                    {
                        Inventory.Add(thing);
                        GetRoom().ItemsInRoom.Remove(thing);
                        narratorOut = $"\n\tYou picked up a {thing.Description} {thing.Name.ToLower()}.";
                    }
                    else
                    {
                        narratorOut = "\n\tYou can't pick up that. Beacuse of unknown reasons.";
                    }
                    break;

                case "USE":         //Checks if the user can and/or have items that they can use or interact with
                    if (UserCommand.Count < 6) { narratorOut = "You couldn't decide what you wanted to do."; break; }

                    Item item = null;       //Looks for the first item the user describes

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

                    if (door != null && item.CanBeUsedOn == door.DoorID)        //looks for the second item or door the user wants to interact with
                    {                                                           //Also filters out any null values that may break the application
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

                case "DROP":            //Removes item from inventory and add it to the room the player currently occupies.
                    Item itemToDrop = Inventory.FirstOrDefault(i => i.Description.ToUpper() == UserCommand[1] && i.Name.ToUpper() == UserCommand[2]);
                    Inventory.Remove(itemToDrop);
                    GetRoom().ItemsInRoom.Add(itemToDrop);
                    narratorOut = $"You find your pockes a bit too heavy so you take out and toss the {itemToDrop.Description} {itemToDrop.Name}";
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
