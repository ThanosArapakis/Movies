using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Movies.Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Movies.Backend.Models
{
    public class Movie
    {
        //Automatically Stored as Primary Key by Entity Framework
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        // Serialized field for image paths
        public string? ImagePathsSerialized { get; set; }

        //The list of Image Urls. The serialization scenario keeps model and
        //database schema simple as long as we dont want to query individual image paths
        //If we wanted to query individual image paths, a foreign key
        //to a MovieImage table approach would be better
        [NotMapped]
        public List<string>? ImageUrl {
            get => string.IsNullOrEmpty(ImagePathsSerialized) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(ImagePathsSerialized);
            set => ImagePathsSerialized = JsonSerializer.Serialize(value);
        }

        public Genre? Genre { get; set; }
    }
}
