using Abp.Application.Services;
using RinkLine.MultiTenancy.Dto;

namespace RinkLine.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

