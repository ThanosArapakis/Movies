using Movies.Frontend.Models.DataTransferObjects;
using Movies.Frontend.Models.Enums;
using Movies.Frontend.Services.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Movies.Frontend.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(RequestDto RequestDto)
        {
            try
            {
                var client = httpClient.CreateClient("MoviesAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(RequestDto.Url);
                client.DefaultRequestHeaders.Clear();
                if (RequestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(RequestDto.Data),
                        Encoding.UTF8, "application/json");
                }               

                HttpResponseMessage apiResponse = null;
                switch (RequestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;

            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    ErrorMessage = e.Message,
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
