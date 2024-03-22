namespace ExampleShop.Entities;

public class Category
{
    
    public long Id { get; set; }
    public string Name { get; set; }
    
    public Category(long id,string name)
    {
        Id = id;
        Name = name;
    }
    
}