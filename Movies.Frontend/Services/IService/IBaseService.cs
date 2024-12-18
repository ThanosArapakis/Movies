using Movies.Frontend.Models.DataTransferObjects;

namespace Movies.Frontend.Services.IService
{
    public interface IBaseService : IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(RequestDto apiRequest);
    }
}
