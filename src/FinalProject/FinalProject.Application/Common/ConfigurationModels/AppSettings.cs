using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Application.Common.ConfigurationModels
{
    public class AppSettings
    {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public Seq Seq { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
        public string ApplicationDbContextConnection { get; set; }
    }

    public class Seq
    {
        public string ServerUrl { get; set; }
        public string ApiKey { get; set; }
        public string MinimumLevel { get; set; }
        public Leveloverride LevelOverride { get; set; }
        public Serilog Serilog { get; set; }
    }

    public class Leveloverride
    {
        public string Microsoft { get; set; }
    }

    public class Serilog
    {
        public Minimumlevel MinimumLevel { get; set; }
    }

    public class Minimumlevel
    {
        public string Default { get; set; }
        public Override Override { get; set; }
        public string[] Using { get; set; }
        public string[] Enrich { get; set; }
    }

    public class Override
    {
        public string Microsoft { get; set; }
    }

}
