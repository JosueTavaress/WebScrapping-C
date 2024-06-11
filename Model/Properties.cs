namespace WebScrapping_C.Model
{
    public class Properties
    {
        public int Id { get; set; }
        public string Units { get; set; }
        public string ValuePer100G { get; set; }
        public string StandardDeviation { get; set; }
        public string MinimumValue { get; set; }
        public string MaximumValue { get; set; }
        public string NumberOfDataUsed { get; set; }
        public string References { get; set; }
        public string DataType { get; set; }
        public string Component { get; set; }

        public int ItemId { get; set; } // forem_key (entity)
        public Item Item { get; set; } // nav - item

        public Properties()
        {
            Component = string.Empty;
            Units = string.Empty;
            ValuePer100G = string.Empty;
            StandardDeviation = string.Empty;
            MinimumValue = string.Empty;
            MaximumValue = string.Empty;
            NumberOfDataUsed = string.Empty;
            References = string.Empty;
            DataType = string.Empty;
        }
    }
}
