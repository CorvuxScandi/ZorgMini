using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class Item
    {
        public int Unlock { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public Item(string name, string description, int opens)
        {
            Name = name;
            Description = description;
            Unlock = opens;
        }
    }
}
