using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Exceptions;

public class CacheNotFoundException : Exception
{
    public CacheNotFoundException()
        : base()
    {
    }

    public CacheNotFoundException(string message)
        : base(message)
    {
    }

    public CacheNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CacheNotFoundException(string name, object key)
        : base($"Cache \"{name}\" ({key}) was not found.")
    {
    }
}
