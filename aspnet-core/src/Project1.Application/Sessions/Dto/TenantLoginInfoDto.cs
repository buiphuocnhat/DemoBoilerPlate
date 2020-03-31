using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Project1.MultiTenancy;

namespace Project1.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
