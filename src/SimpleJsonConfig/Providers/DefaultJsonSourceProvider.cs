using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// Default Json Source Provider Class
    /// </summary>
    /// <seealso cref="SimpleJsonConfig.Providers.IJsonSourceProvider" />
    /// <summary>
    /// The Env variables mentioned below do not need to be set. A default configution can be used. Just ensure you have a folder called dev in your bin directory with your config files.
    /// This class provides the default json source provider. Please note the env variables the class will look at are 
    /// ConfEnv - The system will look at this variable to determine which environment you are working with. The default is default. The system will look for config files in the lowercase folder that 
    /// corresponds to this value. IE if this is set to "Development" the system will look at <RootFolder>/development.
    /// RootFolder - This environment variable is the root folder at which the system should look for finding configuration files. If not specified the system will look at the current directory where the 
    /// executable is running. 
    /// </summary>
    public class DefaultJsonSourceProvider : IJsonSourceProvider
    {
        private const string ConfEnv = "ConfEnv";
        private const string RootFolder = "RootFolder";
        private const string FileExtention = ".json";
        private const string DefaultEnviroment = "dev";


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

            var streams = new List<Stream>();

            Trace.TraceInformation("Environment: {0}", environment);
            Trace.TraceInformation("RootFolder: {0}", rootFolder);
            Trace.TraceInformation("Path: {0}", path);


            if (Directory.Exists(path))
            {
                Trace.TraceInformation("Looking for config file in {0}", path);

                var files = Directory.GetFiles(path);
                foreach (var file in from file in files let extension = Path.GetExtension(file) where extension != null && extension.ToLower().Equals(FileExtention.ToLower()) select file)
                {

                    var fileStream = File.OpenRead(file);
                    streams.Add(fileStream);
                }
            }

            if (streams.Count() == 0)
            {
                return null;
            }
            else
            {
                return new CombinationStream(streams);
            }
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
