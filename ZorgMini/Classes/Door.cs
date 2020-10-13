namespace ZorgMini
{
    public class Door                   //Sets the different properties for the door object class
    {
        public int DoorID { get; set; }

        public bool Locked { get; set; }

        public string Orientation { set; get; }

        public int GoTo { get; set; }

        public Door() { }

        public Door(int doorID, bool locked, string orientation, int goTo)
        {
            DoorID = doorID;
            Locked = locked;
            Orientation = orientation;
            GoTo = goTo;
        }
    }
}
