using System.Collections.Generic;
using Abp.Configuration;

namespace RinkLine.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition("DefaultFromAddress","padhiyarmahavirsinh@gmail.com"),
                new SettingDefinition("Smtp.Host:","127.0.0.1"),
                new SettingDefinition("Smtp.Port","25"),
                new SettingDefinition("Smtp.UserName","25"),
                new SettingDefinition("Smtp.Password","25"),
                new SettingDefinition("Smtp.Domain","25"),
                new SettingDefinition("Smtp.EnableSsl","false"),
                new SettingDefinition("Smtp.UseDefaultCredentials","true"),
            };
        }
    }
}
