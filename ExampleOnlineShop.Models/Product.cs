namespace ExampleOnlineShop.Models
{
    public record Product : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";

        public Product()
        {
            
        }

        // public Product(string name, decimal price, int stock, string description, string image)
        // {
        //   
        //     Name = name;
        //     Price = price;
        //     Stock = stock;
        //     Description = description;
        //     Image = image;
        // }
        public Product(Guid id,string name, decimal price, int stock, string description, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
            Image = image;
        }
    }
}