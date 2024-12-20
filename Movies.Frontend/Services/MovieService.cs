using Movies.Frontend.Models.DataTransferObjects;
using Movies.Frontend.Models.Enums;
using Movies.Frontend.Services.IService;

namespace Movies.Frontend.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IHttpClientFactory _clientFactory;
        private static string MovieAPIBase = "https://localhost:7162";

        public MovieService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
       
        public async Task<T> CreateMovieAsync<T>(MovieDto movieDto)
        {
            return await this.SendAsync<T>(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = movieDto,
                Url = MovieAPIBase + "/api/movie"
            });
        }

        public async Task<T> DeleteMovieAsync<T>(int id)
        {
            return await this.SendAsync<T>(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = MovieAPIBase + "/api/movie/" + id
            });
        }

        public async Task<T> GetAllMoviesAsync<T>()
        {
            return await this.SendAsync<T>(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = MovieAPIBase + "/api/movie"
            });
        }

        public async Task<T> GetMovieByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = MovieAPIBase + "/api/movie/" + id
            });
        }

        public async Task<T> UpdateMovieAsync<T>(MovieDto movieDto)
        {
            return await this.SendAsync<T>(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = movieDto,
                Url = MovieAPIBase + "/api/movie"
            });
        }
    }
}
