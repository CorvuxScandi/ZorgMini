namespace ZorgMini
{
    public class Door
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
