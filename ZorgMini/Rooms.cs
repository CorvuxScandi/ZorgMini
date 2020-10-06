using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class Rooms
    {

        internal int RoomID { get; set; }

        public List<Doors> DoorsInRoom;

        public List<Item> ItemsInRoom;

        internal string RoomDescription { get; set; }


    }
}
