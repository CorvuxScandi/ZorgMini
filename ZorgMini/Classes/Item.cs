namespace ZorgMini
{
    public class Item       //Sets the different properties for the item bject class
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
