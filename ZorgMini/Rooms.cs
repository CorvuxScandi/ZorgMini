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

        public List<Doors> GetDoors(int roomID)
        {
            return (List<Doors>)adventureMap.Where(x => x.RoomID == roomID).Select(x => x.DoorsInRoom);
        }
        
        public List<Rooms> adventureMap = new List<Rooms>()
        {
            new Rooms() //first Room
            {
                RoomID = 1,

                ItemsInRoom = new List<Item>()
                {
                   new Item("table", "a wooden"),
                   new Item("chair", "a wooden"),
                   new Item("KEY", "rusty", 14) {CanBePickedUp = true}
                },

                DoorsInRoom = new List<Doors>()
                {
                    new Doors(14, true, "NORTH", 2)
                },


                RoomDescription = "You are in a damp and low lit room made of stone."

            },

            new Rooms() //second Room
            {
                RoomID = 2,


            }

        };
    
    }
}
