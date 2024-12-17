using Movies.Backend.Models;
using System.Linq.Expressions;

namespace Movies.Backend.Repository.IRepository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies(Expression<Func<Movie, bool>>? filter = null);
        Movie? GetMovie(int? id);
        void AddMovie(Movie entity);
        void RemoveMovie(Movie entity);

        void Save();
    }
}
