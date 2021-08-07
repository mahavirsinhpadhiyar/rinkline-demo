using System.Threading.Tasks;
using Abp.Application.Services;
using RinkLine.Authorization.Accounts.Dto;

namespace RinkLine.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);
        Task<RegisterOutput> Register(RegisterInput input);
        Task<ForgotPassword> SendPasswordResetCode(ForgotPassword forgotPassword);
        Task ResetPasswordAsync(ResetPassword resetPassword);
    }
}
