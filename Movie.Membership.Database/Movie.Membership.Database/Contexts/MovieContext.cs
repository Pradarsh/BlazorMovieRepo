using Microsoft.EntityFrameworkCore;
using Movie.Membership.Database.Entities;
using System.Reflection.Emit;


namespace Movie.Membership.Database.Contexts;

public class MovieContext : DbContext
{
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<SimilarFilm> similarFilms => Set<SimilarFilm>();


    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Film>(entity =>
        {
            entity.ToTable("Film");
           
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Director).WithMany(p => p.Films)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull);                
        });

        builder.Entity<FilmGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FilmGenre");

            entity.HasOne(d => d.Film).WithMany()
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull);              
        });
        

        builder.Entity<SimilarFilm>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.ParentFilm).WithMany()
                .HasForeignKey(d => d.ParentFilmId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            entity.HasOne(d => d.SimilarFilmNavigation).WithMany()
                .HasForeignKey(d => d.SimilarFilmId)
                .OnDelete(DeleteBehavior.ClientSetNull);
               
        });

        builder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
          
        });


        base.OnModelCreating(builder);

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }


}
