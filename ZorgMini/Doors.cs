using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public enum Orientation { North, South, West, East }

    public class Doors
    {

        public int ID { get; set; }
        
        public bool Locked { get; set; }

        public Orientation Orientation { get; set; }


    }
}
