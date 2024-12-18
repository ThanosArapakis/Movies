using Movies.Frontend.Models.DataTransferObjects;

namespace Movies.Frontend.Services.IService
{
    public interface IMovieService
    {
        Task<T> GetAllMoviesAsync<T>();
        Task<T> GetMovieByIdAsync<T>(int id);
        Task<T> CreateMovieAsync<T>(MovieDto movieDto);
        Task<T> UpdateMovieAsync<T>(MovieDto movieDto);
        Task<T> DeleteMovieAsync<T>(int id);
    }
}
