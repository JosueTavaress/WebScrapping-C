namespace WebScrapping_C;

public class Properties
{
    public string Units { get; set; }
    public string ValuePer100G { get; set; }
    public string StandardDeviation { get; set; }
    public string MinimumValue { get; set; }
    public string MaximumValue { get; set; }
    public string NumberOfDataUsed { get; set; }
    public string References { get; set; }
    public string DataType { get; set; }
    public string Component { get; set; }

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


public class Item
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string Group { get; set; }
    public string Brand { get; set; }
    public List<Properties> Details { get; set; } = new List<Properties>();

    public Item()
    {
        Code = string.Empty;
        Name = string.Empty;
        ScientificName = string.Empty;
        Group = string.Empty;
        Brand = string.Empty;
    }
}