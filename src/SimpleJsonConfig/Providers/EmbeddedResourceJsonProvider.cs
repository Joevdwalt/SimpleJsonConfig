using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// This implementation allow the user to embed config in a binary and compile it with the code.
    /// </summary>
    public class EmbeddedResourceJsonProvider : IJsonSourceProvider
    {
        private Type _typeInAssembly;
        private string _nameSpace;
        private string _resourceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceJsonProvider"/> class.
        /// </summary>
        /// <param name="typeInAssembly">The type in the assembly to look for. </param>
        /// <param name="nameSpace">The name space of the type you will be deserialising</param>
        /// <param name="resourceName">Name of the resource. </param>
        public EmbeddedResourceJsonProvider(Type typeInAssembly, string nameSpace, string resourceName)
        {
            _resourceName = resourceName;
            _nameSpace = nameSpace;
            _typeInAssembly = typeInAssembly;
        }

        /// <summary>
        /// Gets the json stream. This will look for the config files as per the parameters specified by the
        /// contructor arguments.
        /// </summary>
        /// <returns></returns>
        public Stream GetJsonStream()
        {
            var resourceName = String.Format("{0}.{1}", _nameSpace, _resourceName);
            var stream = _typeInAssembly.Assembly.GetManifestResourceStream(resourceName);
            return stream;
        }
    }
}
