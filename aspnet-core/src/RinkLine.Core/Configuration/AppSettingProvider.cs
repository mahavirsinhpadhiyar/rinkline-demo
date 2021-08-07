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
                new SettingDefinition("DefaultFromAddress","alpeshkalena123@gmail.com"),
                new SettingDefinition("Smtp.Host:","smtp.gmail.com"),
                new SettingDefinition("Smtp.Port","465"),
                new SettingDefinition("Smtp.UserName","alpeshkalena123@gmail.com"),
                new SettingDefinition("Smtp.Password","91578076"),
                new SettingDefinition("Smtp.Domain","gmail.com"),
                new SettingDefinition("Smtp.EnableSsl","true"),
                new SettingDefinition("Smtp.UseDefaultCredentials","false"),
            };
        }
    }
}
