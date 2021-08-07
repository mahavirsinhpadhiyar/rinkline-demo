using Microsoft.Extensions.Configuration;

namespace RinkLine.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
