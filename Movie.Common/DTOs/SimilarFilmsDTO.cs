using System.ComponentModel.DataAnnotations;

namespace Movie.Common.DTO;

public class SimilarFilmDTO
{
    public int ParentFilmId { get; set; }
    public int SimilarFilmId { get; set; }

    public  FilmDTO ParentFilm { get; set; } 

    public  FilmDTO SimilarFilmNavigation { get; set; } 

}
