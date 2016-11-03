using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace SimpleJsonConfig.Providers
{

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


        private PathProvider.PathProvider PathProvider { get; set; }

        public DefaultJsonSourceProvider(PathProvider.PathProvider pathProvider)
        {
            this.PathProvider = pathProvider;
        }


        public DefaultJsonSourceProvider()
        {
            this.PathProvider = new PathProvider.PathProvider();
        }

        public Stream GetJsonStream()
        {
            // Look for default folder and dev or development folders
            var environment = Environment.GetEnvironmentVariable(ConfEnv);
            environment = String.IsNullOrEmpty(environment) ? DefaultEnviroment : environment;

            var rootFolder = Environment.GetEnvironmentVariable(RootFolder);
            this.PathProvider.RootPath = rootFolder;

            var enviromentPath = environment.ToLower();
            var path = this.PathProvider.GetConfigPath(enviromentPath);

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

        public Task<Stream> GetJsonStreamAsync()
        {
            throw new NotImplementedException();
        }
    }
}
