using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Exceptions
{
    public class UnfinishedOrderException : Exception
    {
        public UnfinishedOrderException()
            : base()
        {
        }

        public UnfinishedOrderException(string message)
            : base(message)
        {
        }

        public UnfinishedOrderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnfinishedOrderException(string name, object key)
            : base($"You have unfinished order(s) related to {name} service")
        {
        }
    }
}
