using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SimpleJsonConfig.Providers;

namespace SimpleJsonConfig
{
    /// <summary>
    /// Simple class for reading .json config files. 
    /// </summary>
    public class ConfigReader
    {
        /// <summary>
        /// The json source provider
        /// </summary>
        private readonly IJsonSourceProvider jsonSourceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigReader"/> class.
        /// </summary>
        public ConfigReader()
        {
            this.jsonSourceProvider = new DefaultJsonSourceProvider();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigReader"/> class.
        /// </summary>
        /// <param name="_jsonSource">The _json source.</param>
        public ConfigReader(IJsonSourceProvider jsonSource)
        {
            this.jsonSourceProvider = jsonSource;
        }

        /// <summary>
        /// Gets the setting. If the key is not found will return null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetSetting<T>(string key)
        {
            var stream = jsonSourceProvider.GetJsonStream();
            if (stream == null) return default(T);
            using (var streamReader = new StreamReader(jsonSourceProvider.GetJsonStream()))
            {
                var jsonString = streamReader.ReadToEnd();
                var jsonObject = JObject.Parse(jsonString);
                var token = jsonObject.SelectToken(key);
                var result = default(T);

                if (token != null)
                {
                    result = jsonObject.SelectToken(key).ToObject<T>();
                }

                streamReader.Close();
                return result;
            }
        }

        /// <summary>
        /// Gets the setting asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetSettingAsync<T>(string key)
        {
            var stream = await this.jsonSourceProvider.GetJsonStreamAsync();
            if (stream == null || stream == Stream.Null) return default(T);

            using (var streamReader = new StreamReader(stream))
            {
                var jsonString = streamReader.ReadToEnd();
                var jsonObject = JObject.Parse(jsonString);
                var token = jsonObject.SelectToken(key);
                var result = default(T);

                if (token != null)
                {
                    result = jsonObject.SelectToken(key).ToObject<T>();
                }

                streamReader.Close();
                return result;
            }
        }
    }
}
