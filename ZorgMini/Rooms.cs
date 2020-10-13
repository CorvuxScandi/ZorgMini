using System.Collections.Generic;

namespace ZorgMini
{
    public class Room
    {

        internal int RoomID { get; set; }

        public List<Door> DoorsInRoom;

        public List<Item> ItemsInRoom;

        internal string RoomDescription { get; set; }


    }
}
