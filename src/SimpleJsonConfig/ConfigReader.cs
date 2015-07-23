using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SimpleJsonConfig.Providers;

namespace SimpleJsonConfig
{
    /// <summary>
    /// Simple class for reading .json config files. 
    /// </summary>
    public class ConfigReader
    {
        private IJsonSourceProvider _jsonSourceProvider;

        public ConfigReader()
        {
            _jsonSourceProvider = new DefaultJsonSourceProvider();
        }

        public ConfigReader(IJsonSourceProvider _jsonSource)
        {
            _jsonSourceProvider = _jsonSource;
        }

        /// <summary>
        /// Gets the setting. If the key is not found will return null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public T GetSetting<T>(string key)
        {
            var stream = _jsonSourceProvider.GetJsonStream();
            if (stream == null) return default(T);
            using (var streamReader = new StreamReader(_jsonSourceProvider.GetJsonStream()))
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
