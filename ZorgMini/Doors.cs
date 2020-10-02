using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    

    public class Doors
    {

        public int ID { get; set; }
        
        public bool Locked { get; set; }

        public string Orientation { set; get; }

        public int GoTo { get; set; }

        public Doors(int id, bool locked, string orientation, int goTo)
        {
            ID = id;
            Locked = locked;
            Orientation = orientation;
            GoTo = goTo;
        }
    }
}
