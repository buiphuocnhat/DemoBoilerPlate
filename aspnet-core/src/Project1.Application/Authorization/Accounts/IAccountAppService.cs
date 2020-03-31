using System.Threading.Tasks;
using Abp.Application.Services;
using Project1.Authorization.Accounts.Dto;

namespace Project1.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
