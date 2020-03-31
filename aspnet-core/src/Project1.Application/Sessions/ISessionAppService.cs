using System.Threading.Tasks;
using Abp.Application.Services;
using Project1.Sessions.Dto;

namespace Project1.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
