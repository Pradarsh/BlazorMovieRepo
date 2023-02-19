using System.ComponentModel.DataAnnotations;

namespace Movie.Membership.Database.Entities;

public class Film : IEntity
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? Title { get; set; }

    [Required]
    public DateTime Released { get; set; }
   
    public bool Free { get; set; }
    [MaxLength(200), Required]
    public string? Description { get; set; }
    [MaxLength(1024), Required]
    public string? FilmUrl { get; set; }
    public int DirectorId { get; set; }
    public virtual Director? Director { get; set; }  
    
}


