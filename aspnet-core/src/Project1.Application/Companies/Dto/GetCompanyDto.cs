using Project1.Cars.Dto;
using System.Collections.Generic;
using Project1.Transactions.Dto;
using Abp.Application.Services.Dto;

namespace Project1.Companies.Dto
{
    public class GetCompanyDto: EntityDto
    {
        public string Name { get; set; }
        public List<GetCarDto> Cars { get; set; }
    }
}
