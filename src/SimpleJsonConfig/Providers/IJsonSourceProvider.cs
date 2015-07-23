using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonConfig.Providers
{
    public interface IJsonSourceProvider
    {
        Stream GetJsonStream();
    }
}
