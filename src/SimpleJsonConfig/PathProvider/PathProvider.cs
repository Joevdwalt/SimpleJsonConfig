using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonConfig
{
    public class PathProvider
    {
        private string currentPath = string.Empty;

        private string rootPath;

        /// <summary>
        /// Gets or sets the current path. This path is the local assembly path but can be overwritted by setting this property.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        public string CurrentPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.currentPath))
                {
                    return AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    return currentPath;
                }
            }
            set
            {
                this.currentPath = value;
            }
        }

        public string RootPath { get; set; }
        
        public string GetConfigPath(string enviromentPath)
        {
            var path = this.CurrentPath;

            if (!string.IsNullOrEmpty(this.RootPath))
            {
                path = Path.Combine(path, RootPath);
            }

            return Path.Combine(path, enviromentPath);
        }
    }
}
