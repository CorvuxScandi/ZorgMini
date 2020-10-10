﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZorgMini
{
    public class AdventureMap
    {
        public int RoomTracker = 1;

        public List<Rooms> map = new List<Rooms>()
        {
            new Rooms() //first Room
            {
                RoomID = 1,

                ItemsInRoom = new List<Item>()
                {
                   new Item("table", "wooden"),
                   new Item("chair", "wooden"),
                   new Item("KEY", "rusty", 10) {CanBePickedUp = true}
                },

                DoorsInRoom = new List<Doors>()
                {
                    new Doors(10, true, "NORTH", 2)
                },
                RoomDescription = "You are in a damp and low lit room made of stone."

            },
            new Rooms()
            {
                RoomID = 2,

                ItemsInRoom = new List<Item>()
                {
                    new Item("Key", "metal", 15),
                    //new Item()
                }
            }
        };

        public Item ItemInteraction(Item item1, Item item2)
        {

            switch (item2)
            {
                case map
            }

                


        }
    }
}
