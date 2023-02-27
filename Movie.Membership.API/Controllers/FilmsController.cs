using Microsoft.AspNetCore.Mvc;
using Movie.Common.DTO;
using Movie.Membership.Database.Entities;
using Movie.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

namespace Movie.Membership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IDbService _db;

        public FilmsController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Film>();
                var Films = await _db.GetAsync<Film, FilmDTO>();
                return Results.Ok(Films);
            }            
            catch(Exception ex) 
            {
                return Results.NotFound("could not find any Film.");
            }

        }


        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Film>();                
                var Film = await _db.SingleAsync<Film, FilmDTO>(c => c.Id == id);
                return Results.Ok(Film);
            }
            catch
            {
                return Results.NotFound("could not find any Film.");
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] FilmDTO dto)
        {
            try
            {
                var Film = await _db.AddAsync<Film, FilmDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURI(Film), Film);
               
            }
            catch
            {
                return Results.BadRequest("could not add the Film.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] FilmDTO dto)
        {
            try
            {
                if (id != dto.Id) return Results.BadRequest($"Id mismatch. URI ID:{id} DTO Id:{dto.Id}");
                        

                var existingFilm = await _db.AnyAsync<Film>(c => c.Id == id);
                if (!existingFilm) return Results.NotFound("Film not found.");

                _db.Update<Film, FilmDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not add the Film.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Film>(c => c.Id == id);
                if (!exists) return Results.NotFound("Film not found.");

                var success = await _db.DeleteAsync<Film>(id);
                if (!success) return Results.NotFound("Film not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not delete the Film.");
            }
        }
    }
}
