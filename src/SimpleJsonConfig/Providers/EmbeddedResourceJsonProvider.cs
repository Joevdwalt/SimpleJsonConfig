using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SimpleJsonConfig.Providers
{
    public class EmbeddedResourceJsonProvider : IJsonSourceProvider
    {
        private Type _typeInAssembly;
        private string _nameSpace;
        private string _resourceName;

        public EmbeddedResourceJsonProvider(Type typeInAssembly, string nameSpace, string resourceName)
        {
            _resourceName = resourceName;
            _nameSpace = nameSpace;
            _typeInAssembly = typeInAssembly;
        }

        public Stream GetJsonStream()
        {
            var resourceName = String.Format("{0}.{1}", _nameSpace, _resourceName);
            var stream = _typeInAssembly.Assembly.GetManifestResourceStream(resourceName);
            return stream;
        }
    }
}
