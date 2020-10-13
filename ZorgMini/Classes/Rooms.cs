using System.Collections.Generic;

namespace ZorgMini
{
    public class Room    //Make each room as its own object
    {

        internal int RoomID { get; set; }

        public List<Door> DoorsInRoom;

        public List<Item> ItemsInRoom;

        internal string RoomDescription { get; set; }


    }
}
