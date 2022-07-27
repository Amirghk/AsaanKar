using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.Interfaces;
public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}

