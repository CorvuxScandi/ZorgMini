using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class Item
    {
        public int ItemID { get; set; }

        public int CanBeUsedOn { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool CanBePickedUp = false;

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Item(string name, string description, int canBeUsedOn)
        {
            Name = name;
            Description = description;
            CanBeUsedOn = canBeUsedOn;
        }
    }
}
