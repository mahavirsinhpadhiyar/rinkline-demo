using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RinkLine.Authorization;

namespace RinkLine
{
    [DependsOn(
        typeof(RinkLineCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RinkLineApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RinkLineAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RinkLineApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
