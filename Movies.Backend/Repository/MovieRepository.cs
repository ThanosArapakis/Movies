using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Movies.Backend.Models;
using Movies.Backend.Models.DataTransferObjects;
using Movies.Backend.Repository.IRepository;
using Movies.Data;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movies.Backend.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public MovieRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MovieDto> AddMovie(MovieDto movieDto)
        {
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();

            return _mapper.Map<Movie, MovieDto>(movie);
        }


        public async Task<MovieDto> UpdateMovie(MovieDto movieDto)
        {
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _db.Movies.Update(movie);
            await _db.SaveChangesAsync();

            return _mapper.Map<Movie, MovieDto>(movie);
        }

        public async Task<MovieDto> GetMovie(int? movieId)
        {
            Movie movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<IEnumerable<MovieDto>> GetAllMovies()
        {
            List <Movie> movies =await _db.Movies.ToListAsync(); 
            return _mapper.Map<List<MovieDto>>(movies);
        }

        public async Task<bool> RemoveMovie(int movieId)
        {
            try
            {
                Movie movie = await _db.Movies.FirstOrDefaultAsync(u => u.Id == movieId);

                if (movie == null)
                {
                    return false;
                }

                _db.Movies.Remove(movie);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
