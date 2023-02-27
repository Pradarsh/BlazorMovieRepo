using Microsoft.AspNetCore.Mvc;
using Movie.Common.DTO;
using Movie.Membership.Database.Entities;
using Movie.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

namespace Movie.Membership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IDbService _db;

        public GenreController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Genre>();
                var genres = await _db.GetAsync<Genre, GenreDTO>();
                return Results.Ok(genres);
            }            
            catch(Exception ex) 
            {
                return Results.NotFound("could not find any genre.");
            }

        }


        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Genre>();                
                var genre = await _db.SingleAsync<Genre, GenreDTO>(c => c.Id == id);
                return Results.Ok(genre);
            }
            catch
            {
                return Results.NotFound("could not find any Genre.");
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreDTO dto)
        {
            try
            {
                var genre = await _db.AddAsync<Genre, GenreDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURI(genre), genre);
               
            }
            catch
            {
                return Results.BadRequest("could not add the genre.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreDTO dto)
        {
            try
            {
                if (id != dto.Id) return Results.BadRequest($"Id mismatch. URI ID:{id} DTO Id:{dto.Id}");
                        

                var existingGenre = await _db.AnyAsync<Genre>(c => c.Id == id);
                if (!existingGenre) return Results.NotFound("Genre not found.");

                _db.Update<Genre, GenreDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not add the genre.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Genre>(c => c.Id == id);
                if (!exists) return Results.NotFound("Genre not found.");

                var success = await _db.DeleteAsync<Genre>(id);
                if (!success) return Results.NotFound("Genre not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not delete the Genre.");
            }
        }
    }
}
