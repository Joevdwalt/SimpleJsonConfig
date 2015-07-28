using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// Http Client Source Provider Class
    /// </summary>
    public class HttpClientSourceProvider : IJsonSourceProvider
    {
        /// <summary>
        /// The _method
        /// </summary>
        private readonly HttpMethod _method;
        /// <summary>
        /// The _url
        /// </summary>
        private readonly string _url;
        /// <summary>
        /// The _HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientSourceProvider"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="method">The method.</param>
        /// <param name="clientHandler">The client handler.</param>
        public HttpClientSourceProvider(string url, HttpMethod method, HttpMessageHandler clientHandler = null)
        {
            this._url = url;
            this._method = method;
            this._httpClient = clientHandler != null ? new HttpClient(clientHandler) : new HttpClient();
        }

        /// <summary>
        /// Gets the json stream.
        /// </summary>
        /// <returns></returns>
        public Stream GetJsonStream()
        {
            var httpResponseMessage = this._httpClient.SendAsync(new HttpRequestMessage(this._method, this._url)).Result;
            if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
            {
                return httpResponseMessage.Content.ReadAsStreamAsync().Result;
            }

            return Stream.Null;
        }

        /// <summary>
        /// Gets the json stream asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetJsonStreamAsync()
        {
            var httpResponseMessage = await this._httpClient.SendAsync(new HttpRequestMessage(this._method, this._url));
            if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStreamAsync();
            }

            return Stream.Null;
        }
    }
}