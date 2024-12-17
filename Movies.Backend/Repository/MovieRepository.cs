using Microsoft.EntityFrameworkCore;
using Movies.Backend.Models;
using Movies.Backend.Repository.IRepository;
using Movies.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movies.Backend.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
        }

        public Movie? GetMovie(int? id)
        {
            return _db.Movies.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Movie> GetAllMovies(Expression<Func<Movie, bool>>? filter = null)
        {
            List<Movie> movies = _db.Movies.Where(filter).ToList(); 
            return movies;
        }

        public void RemoveMovie(Movie movie)
        {
            _db.Movies.Remove(movie);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
