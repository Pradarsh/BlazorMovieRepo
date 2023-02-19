using System.ComponentModel.DataAnnotations;

namespace Movie.Membership.Database.Entities;

public class Genre : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Name { get; set; }  

    
}
