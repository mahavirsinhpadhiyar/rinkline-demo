using Abp.Localization;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using RinkLine.Authorization.Roles;
using RinkLine.Authorization.Users;
using RinkLine.Configuration;
using RinkLine.Localization;
using RinkLine.MultiTenancy;
using RinkLine.Timing;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Castle.MicroKernel.Registration;
using Abp.Net.Mail.Smtp;

namespace RinkLine
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    [DependsOn(typeof(AbpMailKitModule))]
    public class RinkLineCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            RinkLineLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = RinkLineConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            Configuration.ReplaceService<IMailKitSmtpBuilder, MyMailKitSmtpBuilder>();

            Configuration.ReplaceService(typeof(IEmailSenderConfiguration), () =>
            {
                Configuration.IocManager.IocContainer.Register(
                    Component.For<IEmailSenderConfiguration, ISmtpEmailSenderConfiguration>()
                             .ImplementedBy<RinkLineSmtpEmailSenderConfiguration>()
                             .LifestyleTransient()
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RinkLineCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
