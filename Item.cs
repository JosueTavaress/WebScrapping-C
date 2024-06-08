namespace WebScrapping_C;

public class Item
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string ScientificName { get; set; }
    public string Group { get; set; }
    public string Brand { get; set; }

    public Item()
    {
        Code = string.Empty;
        Name = string.Empty;
        ScientificName = string.Empty;
        Group = string.Empty;
        Brand = string.Empty;
    }
}