using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// Default Json Source Provider Class
    /// </summary>
    /// <seealso cref="SimpleJsonConfig.Providers.IJsonSourceProvider" />
    public class DefaultJsonSourceProvider : IJsonSourceProvider
    {
        private const string ConfEnv = "ConfEnv";
        private const string RootFolder = "RootFolder";
        private const string FileExtention = ".json";
        private const string DefaultEnviroment = "default";


        /// <summary>
        /// Gets or sets the path provider.
        /// </summary>
        /// <value>
        /// The path provider.
        /// </value>
        private PathProvider.PathProvider PathProvider { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultJsonSourceProvider"/> class.
        /// </summary>
        /// <param name="pathProvider">The path provider.</param>
        public DefaultJsonSourceProvider(PathProvider.PathProvider pathProvider)
        {
            this.PathProvider = pathProvider;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultJsonSourceProvider"/> class.
        /// </summary>
        public DefaultJsonSourceProvider()
        {
            this.PathProvider = new PathProvider.PathProvider();
        }

        private static async Task<Stream> ReadAllFileAsync(string filename)
        {
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                var buff = new byte[file.Length];
                await file.ReadAsync(buff, 0, (int)file.Length);
                return new MemoryStream(buff);
            }
        }

        /// <summary>
        /// Gets the files from environment.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetFilesFromEnvironment()
        {
            // Look for default folder and development or development folders
            var environment = Environment.GetEnvironmentVariable(ConfEnv);
            environment = string.IsNullOrEmpty(environment) ? DefaultEnviroment : environment;

            var rootFolder = Environment.GetEnvironmentVariable(RootFolder);
            this.PathProvider.RootPath = rootFolder;

            var enviromentPath = environment.ToLower();
            var path = this.PathProvider.GetConfigPath(enviromentPath);

            return !Directory.Exists(path) ? null : Directory.GetFiles(path);
        }

        /// <summary>
        /// Gets the json stream.
        /// </summary>
        /// <returns></returns>
        public Stream GetJsonStream()
        {
            var files = this.GetFilesFromEnvironment();
            return (from file in files
                    let extension = Path.GetExtension(file)
                    where extension != null && extension.ToLower()
                              .Equals(FileExtention.ToLower())
                    select file).Select(File.OpenRead)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the json stream asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetJsonStreamAsync()
        {
            var files = this.GetFilesFromEnvironment();
            var query = from file in files
                    let extension = Path.GetExtension(file)
                    where extension != null && extension.ToLower()
                              .Equals(FileExtention.ToLower())
                    select file;

            return await ReadAllFileAsync(query.FirstOrDefault());
        }
    }
}
