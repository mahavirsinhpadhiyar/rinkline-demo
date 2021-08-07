using System.Threading.Tasks;
using RinkLine.Configuration.Dto;

namespace RinkLine.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
