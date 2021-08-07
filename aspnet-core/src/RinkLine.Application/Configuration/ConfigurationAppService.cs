using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using RinkLine.Configuration.Dto;

namespace RinkLine.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : RinkLineAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
