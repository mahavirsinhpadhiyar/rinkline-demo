using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RinkLine.Configuration;

namespace RinkLine.Web.Host.Startup
{
    [DependsOn(
       typeof(RinkLineWebCoreModule))]
    public class RinkLineWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RinkLineWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RinkLineWebHostModule).GetAssembly());
        }
    }
}
