namespace WebScrapping_C.Model;

public class Item
{
    public int Id { get; set; }
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