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

namespace SimpleJsonConfig
{
    /// <summary>
    /// Simple class for reading .json config files. 
    /// </summary>
    public class ConfigReader
    {

        private const string ConfEnv = "ConfEnv";
        private const string FileExtention = ".json";
        private const string DefaultEnviroment = "default";

        /// <summary>
        /// Gets the setting. This method will return settings from the dev enviroment file assuming that the environment is development. 
        /// For production environments set the ConfEnv = "Production" or similair to read the production configuration.
        /// </summary>
        /// <typeparam name="T">The returning type of the key.</typeparam>
        /// <param name="key">The key to look for</param>
        /// <returns>The value of the key.</returns>
        public T GetSetting<T>(string key) where T : class
        {
            var configEnv = Environment.GetEnvironmentVariable(ConfEnv);
            if (string.IsNullOrEmpty(configEnv))
            {
                configEnv = DefaultEnviroment;
            }

            return GetSetting<T>(key, configEnv);
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="environment">The environment.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public T GetSetting<T>(string key, string environment)
        {
            // Look for default folder and dev or development folders
            var currentPath = Environment.CurrentDirectory;
            var enviromentPath = environment.ToLower();

            var path = string.Format("{0}\\{1}", currentPath, enviromentPath);

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in from file in files let extension = Path.GetExtension(file) where extension != null && extension.ToLower().Equals(FileExtention.ToLower()) select file)
                {
                    using (var streamReader = new StreamReader(file))
                    {
                        var jsonString = streamReader.ReadToEnd();
                        var jsonObject = JObject.Parse(jsonString);
                        var result = jsonObject.SelectToken(key).ToObject<T>();

                        streamReader.Close();
                        return result;
                    }
                }
            }
            return default(T);
        }
    }
}
