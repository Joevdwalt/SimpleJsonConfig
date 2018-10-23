using System.IO;
using System.Threading.Tasks;

namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// Interface for injecting different config providers into the config framework.
    /// </summary>
    public interface IJsonSourceProvider
    {
       
        Stream GetJsonStream();

        Task<Stream> GetJsonStreamAsync();
    }
}
