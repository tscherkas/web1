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
        public IList<Genre> genres { get; set; }
    }
    public class Genre
    {
        public int ID { get; set; }
        public string name { get; set; }
    }
    public class FilmsContext : DbContext
    {
        public FilmsContext() : base()
        {
        }
        public DbSet<Film> films { get; set; }
        public DbSet<Genre> genres { get; set; }


    }
}