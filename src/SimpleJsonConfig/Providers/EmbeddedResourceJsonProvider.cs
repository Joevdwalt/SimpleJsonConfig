using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// This implementation allow the user to embed config in a binary and compile it with the code.
    /// </summary>
    public class EmbeddedResourceJsonProvider : IJsonSourceProvider
    {
        private readonly Type _typeInAssembly;
        private readonly string _nameSpace;
        private readonly string _resourceName;

        public string ProviderName
        {
            get;private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceJsonProvider"/> class.
        /// </summary>
        /// <param name="typeInAssembly">The type in the assembly to look for. </param>
        /// <param name="nameSpace">The name space of the type you will be de-serialising</param>
        /// <param name="resourceName">Name of the resource. </param>
        public EmbeddedResourceJsonProvider(Type typeInAssembly, string nameSpace, string resourceName)
        {
            _resourceName = resourceName;
            _nameSpace = nameSpace;
            _typeInAssembly = typeInAssembly;
            
        }

        /// <summary>
        /// Gets the json stream. This will look for the config files as per the parameters specified by the
        /// contractor arguments.
        /// </summary>
        /// <returns></returns>
        public Stream GetJsonStream()
        {
            var resourceName = String.Format("{0}.{1}", _nameSpace, _resourceName);
            var stream = _typeInAssembly.Assembly.GetManifestResourceStream(resourceName);
            return stream;
        }

        public Task<Stream> GetJsonStreamAsync()
        {
            throw new NotImplementedException();
        }
    }
}
