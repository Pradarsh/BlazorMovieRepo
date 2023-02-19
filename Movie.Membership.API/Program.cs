
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movie.Common.DTO;
using Movie.Membership.Database.Contexts;
using Movie.Membership.Database.Entities;
using Movie.Membership.Database.Services;
using static System.Collections.Specialized.BitVector32;

namespace Movie.Membership.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MovieContext>(
            options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("MovieConnection")));

            ConfigureAutoMapper(builder);
            builder.Services.AddScoped<IDbService, DbService>();
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("CorsAllAccessPolicy", opt =>
                opt.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                );
            });
            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors("CorsAllAccessPolicy");

            app.Run();
        }

        static void ConfigureAutoMapper(WebApplicationBuilder builder)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Film, FilmDTO>().ReverseMap();
                cfg.CreateMap<Director, DirectorDTO>().ReverseMap();

                cfg.CreateMap<FilmGenre, FilmGenreDTO>()
                .ReverseMap();


                cfg.CreateMap<Genre, GenreDTO>()
                   .ReverseMap();


                cfg.CreateMap<SimilarFilm, SimilarFilmDTO>();


            });
            var mapper = config.CreateMapper();            
            builder.Services.AddSingleton(mapper);
        }

    }
}