using System.Collections.Generic;

namespace ZorgMini
{
    public class AdventureMap               //Populates the world map with room
    {
        public int RoomTracker = 1;         //Value symbolizes in what room the player occupies

        public List<Item> NonRoomItem = new List<Item>()
        {
            new Item("Key", "golden", 15){CanBePickedUp = true, ItemID = 40 },
            new Item("Box", "opend"){ItemID = 30}
        }; //List of items that doesnt originate in a room

        public List<Room> map = new List<Room>()            //Create an instans of rooms and populates them with items and doors/ pathways
        {
            new Room() //first Room
            {
                RoomID = 1,


                ItemsInRoom = new List<Item>()
                {
                   new Item("table", "wooden"),
                   new Item("chair", "wooden"),
                   new Item("KEY", "rusty", 10) {CanBePickedUp = true}
                },

                DoorsInRoom = new List<Door>()
                {
                    new Door(10, true, "North", 2),
                    new Door(15, true, "West", 5)
                },
                RoomDescription = "You find yourself in a stone room"

            },
            new Room()  //second room
            {
                RoomID = 2,

                ItemsInRoom = new List<Item>()
                {
                    new Item("Key", "metal", 14) {CanBePickedUp = true },
                    new Item("book-stand", "wooden")

                },

                DoorsInRoom = new List<Door>()
                {
                    new Door(11, false, "South", 1),
                    new Door(12, false, "East", 3)
                },

                RoomDescription = "You are in a dusty old room.\n\t" +
                "Each wall is coverd in bookshelves. And a warm ligth shines from the sealing. "
            },
            new Room() //Third room
            {
                RoomID = 3,
                DoorsInRoom = new List<Door>()
                {
                    new Door(13, false, "West", 2),
                    new Door(14, true, "East", 4)
                },

                ItemsInRoom = new List<Item>()
                {
                    new Item("Box", "Silver"){CanBePickedUp = true, ItemID = 20},
                    new Item("Key", "Silver",20){CanBePickedUp = true}
                }
            },
            new Room() //Fourth room with the first victory condidtion
            {
                RoomID = 4,
                RoomDescription = "You open your eyes and the first mornings rays lights up the sealing in your bedroom." + "\n Congratulations you made it to the end. Do you want to try again?"
            },
            new Room()      //fith and final end room
            {
                RoomID = 5,
                RoomDescription = "Congratulations! You've won my game" +
                "\n" + @"               (( _______" +
                "\n" + @"     _______     /\O    O\" +
                "\n" + @"    /O     /\   /  \      \" +
                "\n" + @"   /   O  /O \ / O  \O____O\ ))" +
                "\n" + @"((/_____O/    \\    /O     /" +
                "\n" + @"  \O    O\    / \  /   O  /" +
                "\n" + @"   \O    O\ O/   \/_____O/" +
                "\n" + @"    \O____O\/ )) mrf      ))" +
                "\n" + @"  (("
            }
        };

    }
}
