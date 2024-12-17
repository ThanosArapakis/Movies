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

        public List<string>? ImagePaths { get; set; }

        public Genre? Genre { get; set; }
    }
}
