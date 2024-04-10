namespace ExampleOnlineShop.Models;

public class Category
{
    
    public Guid Id { get; init; }
    public string Name { get; set; }
    
    public Category(Guid id,string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
}