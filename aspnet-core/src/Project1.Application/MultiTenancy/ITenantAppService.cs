using Abp.Application.Services;
using Project1.MultiTenancy.Dto;

namespace Project1.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

