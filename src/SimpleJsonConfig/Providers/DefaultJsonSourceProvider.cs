using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SimpleJsonConfig.Providers
{
    public class DefaultJsonSourceProvider : IJsonSourceProvider
    {
        private const string ConfEnv = "ConfEnv";
        private const string FileExtention = ".json";
        private const string DefaultEnviroment = "default";

        public Stream GetJsonStream()
        {
            // Look for default folder and dev or development folders
            var environment = Environment.GetEnvironmentVariable(ConfEnv);
            environment = String.IsNullOrEmpty(environment) ? DefaultEnviroment : environment;

            var currentPath = Environment.CurrentDirectory;
            var enviromentPath = environment.ToLower();

            var path = Path.Combine(currentPath, enviromentPath);

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in from file in files let extension = Path.GetExtension(file) where extension != null && extension.ToLower().Equals(FileExtention.ToLower()) select file)
                {
                    var fileStream = File.OpenRead(file);
                    return fileStream;
                }
            }

            return null;
        }
    }
}
