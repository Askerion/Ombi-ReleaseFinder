using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmbiReleaseFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace OmbiReleaseFinder.Database
{
    public class MovieDBContext : DbContext
    {
        //public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        //{ }

        public DbSet<CustomMovie> CustomMovie { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieDatabase;Trusted_Connection=True;");
        }
    }
       
}
