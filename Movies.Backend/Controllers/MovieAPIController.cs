using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.Backend.Models.DataTransferObjects;
using Movies.Backend.Repository.IRepository;

namespace Movies.Backend.Controllers
{
    [Route("api/movie")] //Controller Endpoint
    [ApiController]
    public class MovieAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IMovieRepository _movieRepository;

        //Dependency Injection
        public MovieAPIController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            this._response = new ResponseDto();
        }

        [HttpGet] //defines the type of the api call. In that case its an HTTP GET call.
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
        [Route("{id}")] //specific movie
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
