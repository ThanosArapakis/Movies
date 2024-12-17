using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using Movies.Backend.Models;
using Movies.Backend.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Movies.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }       
      

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                        .Entity<Movie>()
                        .Property(p => p.Genre)
                        .HasConversion(
                            v => v.ToString(),
                            v => (Genre)Enum.Parse(typeof(Genre), v));


            //The list of Image Urls. The serialization scenario keeps model and
            //database schema simple as long as we dont want to query individual image paths
            //If we wanted to query individual image paths, a foreign key
            //to a MovieImage table approach would be better
            var stringListConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),  // Serialize to JSON
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) // Deserialize from JSON
        );

            // Apply the converter to the ImagePaths property
            modelBuilder.Entity<Movie>()
                .Property(p => p.ImagePaths)
                .HasConversion(stringListConverter);
        }
    }
}
