namespace JwtAuth.Client.Services
{
    public interface IHttpService
    {
        Task<Response<T>> Get<T>(string uri);

        Task<Response<T>> Post<T>(string uri, object value);
    }
}
