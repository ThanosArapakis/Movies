using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Backend.Models;
using Movies.Backend.Models.DataTransferObjects;
using Movies.Backend.Repository.IRepository;

namespace Movies.Backend.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IMovieRepository _movieRepository;

        public MovieAPIController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetAll()
        {
            try
            {
                IEnumerable<MovieDto> movies = await _movieRepository.GetAllMovies();
                _response.Result = movies;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int? id)
        {
            try
            {
                MovieDto movieDto = await _movieRepository.GetMovie(id);
                _response.Result = movieDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] MovieDto movieDto)
        {
            try
            {
                MovieDto model = await _movieRepository.AddMovie(movieDto);
                
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message=ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] MovieDto productDto)
        {
            try
            {
                MovieDto model = await _movieRepository.UpdateMovie(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _movieRepository.RemoveMovie(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
