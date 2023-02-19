using System.ComponentModel.DataAnnotations;

namespace Movie.Membership.Database.Entities;

public class SimilarFilm 
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public virtual Film ParentFilm { get; set; } 

    public virtual Film SimilarFilmNavigation { get; set; } 

}
