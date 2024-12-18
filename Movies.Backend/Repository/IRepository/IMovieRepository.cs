using Movies.Backend.Models.DataTransferObjects;
using System.Linq.Expressions;

namespace Movies.Backend.Repository.IRepository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieDto>> GetAllMovies();
        Task<MovieDto> GetMovie(int? id);
        Task<MovieDto> AddMovie(MovieDto movie);
        Task<MovieDto> UpdateMovie(MovieDto movie);
        Task<bool> RemoveMovie(int movieId);
    }
}
