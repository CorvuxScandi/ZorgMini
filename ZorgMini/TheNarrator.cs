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
            UserCommand = userIn.ToUpper().Split(' ').ToList();
            string narratorOut = "";

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
                        RoomTracker = Convert.ToInt32(GetRoom().DoorsInRoom.Where(x => x.Orientation == UserCommand[1]).Select(x => x.GoTo));
                        narratorOut = $"You walk trough the {UserCommand[1].ToLower()} door.";
                    }
                        catch (Exception)
                    {
                        narratorOut = "You cannot go that way.";
                        
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

                    if(UserCommand.Contains("KEY") && UserCommand.Contains("DOOR"))
                    {
                        int key = Convert.ToInt32(Inventory.Where(x => x.Name == "Key" && x.Description == UserCommand[1])
                                                           .Select(x => x.ItemID));
                        int door = Convert.ToInt32(GetRoom().DoorsInRoom.Where(x => x.Orientation == UserCommand[4])
                            .Select(x => x.DoorID));

                        if(key == door)
                        {
                            Doors locked = (Doors)GetRoom().DoorsInRoom.Where(x => x.DoorID == door);
                            locked.Locked = false;
                            narratorOut = $"You unlocked the {UserCommand[3]} door.";
                        }
                    }
                    //if (Inventory.Contains(
                    //    (Item)Inventory.Where(x => x.Description == UserCommand[1] 
                    //    && x.Name == UserCommand[2])))
                    //{
                    //    Item item1 = (Item)Inventory.Where(x => x.Name == UserCommand[2]);
                    //    ItemInteration(item1);
                        
                    //}
                    //if (GetRoom().ItemsInRoom.Contains(
                    //    (Item)GetRoom().ItemsInRoom.Where(x => x.Description == UserCommand[1]
                    //    && x.Name == UserCommand[2])))
                    //{

                    //}

                        break;
            }
            return narratorOut;
        }
        private string ItemInteration(Item item1)
        {
            string result = "";
            if (UserCommand[3] == "ON")
            {
                Item item2 = (Item)Inventory.Where(x => x.Name == UserCommand[5]);
                int ID1 = item1.CanBeUsedOn;
                int ID2 = item2.ItemID;

                if (ID1 == ID2)
                {
                    
                }
            }



            return result;
        }
        

    }
}
