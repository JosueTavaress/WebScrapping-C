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
    public Properties()
    {
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

public class Details
{
    public Properties EnergyKJ { get; set; } = new Properties();
    public Properties EnergyKCAL { get; set; } = new Properties();
    public Properties Moisture { get; set; } = new Properties();
    public Properties TotalCarbohydrate { get; set; } = new Properties();
    public Properties Protein { get; set; } = new Properties();
    public Properties Lipids { get; set; } = new Properties();
    public Properties DietaryFiber { get; set; } = new Properties();
    public Properties Alcohol { get; set; } = new Properties();
    public Properties Ashes { get; set; } = new Properties();
    public Properties Cholesterol { get; set; } = new Properties();
    public Properties SaturatedFattyAcids { get; set; } = new Properties();
    public Properties MonounsaturatedFattyAcids { get; set; } = new Properties();
    public Properties PolyunsaturatedFattyAcids { get; set; } = new Properties();
    public Properties Calcium { get; set; } = new Properties();
    public Properties Sodium { get; set; } = new Properties();
    public Properties Magnesium { get; set; } = new Properties();
}

public class Item
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string Group { get; set; }
    public string Brand { get; set; }
    public Details Details { get; set; } = new Details();

    public Item()
    {
        Code = string.Empty;
        Name = string.Empty;
        ScientificName = string.Empty;
        Group = string.Empty;
        Brand = string.Empty;
    }
}