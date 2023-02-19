using Microsoft.AspNetCore.Mvc;
using Movie.Common.DTO;
using Movie.Membership.Database.Entities;
using Movie.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

namespace Movie.Membership.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IDbService _db;

        public DirectorController(IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                _db.Include<Director>();
                var directors = await _db.GetAsync<Director, DirectorDTO>();
                return Results.Ok(directors);
            }            
            catch(Exception ex) 
            {
                return Results.NotFound("could not find any director.");
            }

        }


        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Director>();                
                var director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id == id);
                return Results.Ok(director);
            }
            catch
            {
                return Results.NotFound("could not find any Director.");
            }
        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] DirectorDTO dto)
        {
            try
            {
                var director = await _db.AddAsync<Director, DirectorDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.Created(_db.GetURI(director), director);
               
            }
            catch
            {
                return Results.BadRequest("could not add the director.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DirectorDTO dto)
        {
            try
            {
                if (id != dto.Id) return Results.BadRequest($"Id mismatch. URI ID:{id} DTO Id:{dto.Id}");
                        

                var existingDirector = await _db.AnyAsync<Director>(c => c.Id == id);
                if (!existingDirector) return Results.NotFound("Director not found.");

                _db.Update<Director, DirectorDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not add the director.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Director>(c => c.Id == id);
                if (!exists) return Results.NotFound("Director not found.");

                var success = await _db.DeleteAsync<Director>(id);
                if (!success) return Results.NotFound("Director not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }
            catch
            {
                return Results.BadRequest("could not delete the Director.");
            }
        }
    }
}
