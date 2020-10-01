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

        internal int ID { get; set; }

        public List<Doors> DoorsInRoom;

        public List<Item> ItemsInRoom;

        internal string RoomDescription { get; set; }

        

        public List<Rooms> adventureMap = new List<Rooms>()
        {
            new Rooms() //first Room
            {
                ID = 1,

                ItemsInRoom = new List<Item>()
                {new Item(){ID = 11, Name = "KEY", Description = "rusty"} , 
                 new Item(){Name = "table", Description = "wooden"},
                 new Item(){Name = "chair", Description = "wooden"}
                },

                DoorsInRoom = new List<Doors>()
                {new Doors()
                { ID = 12, Locked = true, Orientation = Orientation.North }
                },

                RoomDescription = "damp and low lit room made of stone"
                
            },
            
            new Rooms() //second Room
            {
                ID = 2,


            }

        };
    }
}
