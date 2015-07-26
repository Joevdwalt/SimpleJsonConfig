using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonConfig.Providers
{
    /// <summary>
    /// Interface for injecting differenct config providers into the config framework.
    /// </summary>
    public interface IJsonSourceProvider
    {
        Stream GetJsonStream();
    }
}
