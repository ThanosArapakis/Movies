using Movies.Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Movies.Backend.Models.DataTransferObjects
{
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
