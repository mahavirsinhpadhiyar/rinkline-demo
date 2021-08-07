using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RinkLine.EntityFrameworkCore;
using RinkLine.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RinkLine.Web.Tests
{
    [DependsOn(
        typeof(RinkLineWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RinkLineWebTestModule : AbpModule
    {
        public RinkLineWebTestModule(RinkLineEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RinkLineWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RinkLineWebMvcModule).Assembly);
        }
    }
}