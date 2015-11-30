using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public virtual IList<Genre> genres { get; set; }
        public Film()
        {
            genres = new List<Genre>();
        }
    }
    public class Genre
    {
        public int ID { get; set; }
        public string name { get; set; }
        public virtual IList<Film> films { get; set; }
        public Genre()
        {
            films = new List<Film>();
        }
    }
    public class FilmsContext : DbContext
    {
        public FilmsContext() : base("Data Source=DOM;Initial Catalog=movies_test;Integrated Security=True")
        {
        }
        public DbSet<Film> films { get; set; }
        public DbSet<Genre> genres { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Film>().ToTable("t_Department");
            modelBuilder.Entity<Film>()
                        .HasMany<Genre>(s => s.genres)
                        .WithMany(c => c.films)
                        .Map(cs =>
                        {//TODO rename
                            cs.MapLeftKey("FilmRefId");
                            cs.MapRightKey("GenreRefId");
                            cs.ToTable("FilmGenre");
                        });

        }

    }
}