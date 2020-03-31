using Abp.Application.Services;
using Project1.Companies.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Companies
{
    public interface ICompanyAppService : IAsyncCrudAppService<CompanyDto, int, PagedCompanyResultRrequestDto, CreateCompanyDto,GetCompanyDto, CompanyDto>
    {
    }
}
