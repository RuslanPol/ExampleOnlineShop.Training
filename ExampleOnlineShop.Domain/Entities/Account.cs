using System.ComponentModel.DataAnnotations;

namespace ExampleOnlineShop.Domain.Entities
{
    public record Account : IEntity
    {
        public Guid Id { get; init; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required, MinLength(6)]
        public string PasswordHash { get; set; } = "";
        public Account(){}

        public Account(Guid id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}