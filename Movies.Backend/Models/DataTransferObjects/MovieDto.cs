using Movies.Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.Backend.Models.DataTransferObjects
{
    //A Data transfer object for the movie which also exists in the Frontend project so it can be a communication between them without using the main object
    public class MovieDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public List<string>? ImagePaths { get; set; }

        public Genre? Genre { get; set; }
    }
}
