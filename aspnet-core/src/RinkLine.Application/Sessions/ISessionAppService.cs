using System.Threading.Tasks;
using Abp.Application.Services;
using RinkLine.Sessions.Dto;

namespace RinkLine.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
