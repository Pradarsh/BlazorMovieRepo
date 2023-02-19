using System.ComponentModel.DataAnnotations;

namespace Movie.Common.DTO;

public class FilmGenreDTO 
{
    public int FilmId { get; set; }

    public int GenreId { get; set; }

    public  FilmDTO Film { get; set; }

    public  GenreDTO Genre { get; set; } 

}


