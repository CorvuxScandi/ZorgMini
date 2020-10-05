using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    

    public class Doors
    {

        public int DoorID { get; set; }
        
        public bool Locked { get; set; }

        public string Orientation { set; get; }

        public int GoTo { get; set; }

        public Doors(int doorID, bool locked, string orientation, int goTo)
        {
            DoorID = doorID;
            Locked = locked;
            Orientation = orientation;
            GoTo = goTo;
        }
    }
}
