using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using Project1.Authorization;
using Project1.Cars.Dto;
using Project1.Companies.Dto;
using Project1.Entities;
using Project1.MultiTenancy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Project1.Extentions;

namespace Project1.Companies
{
    [AbpAuthorize(PermissionNames.Pages_Companies)]
    public class CompanyAppService : ApplicationService
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        public CompanyAppService(IRepository<Car> carRepository, IRepository<Company> companyRepository, IRepository<Transaction> transactionRepository)
        {
            _carRepository = carRepository;
            _companyRepository = companyRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<List<GetCompanyDto>> GetAllCompanies()
        {
            var query =
                from company in _companyRepository.GetAll()
                orderby company.Name ascending
                select new GetCompanyDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Cars =
                    (from car in _carRepository.GetAll()
                     where car.CompanyId == company.Id
                     select new GetCarDto
                     {
                         Name = car.Name,
                         Description = car.Description,
                     }).ToList()
                };
            var companies = await query.ToListAsync();
            return companies;
        }
        public async Task<GetCompanyDto> GetCompany(int id)
        {
            var query = await (
               from company in _companyRepository.GetAll()
               where company.Id == id
               select new GetCompanyDto
               {
                   Id = company.Id,
                   Name = company.Name,
                   Cars =
                   (from car in _carRepository.GetAll()
                    where car.CompanyId == company.Id
                    select new GetCarDto
                    {
                        Name = car.Name,
                        Description = car.Description,
                    }).ToList()

               }).FirstOrDefaultAsync();

            return query;
        }
        [AbpAuthorize(PermissionNames.Pages_Companies_Create)]
        public async Task CreateCompany(CreateCompanyDto input)
        {
            var company = new Company()
            {
                Name = input.Name
            };
            await _companyRepository.InsertAsync(company);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        [AbpAuthorize(PermissionNames.Pages_Companies_Update)]
        public async Task UpdateCompany(CompanyDto input)
        {
            var company = await _companyRepository.GetAsync(input.Id);
            company.Name = input.Name;
            await _companyRepository.UpdateAsync(company);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        [AbpAuthorize(PermissionNames.Pages_Companies_Delete)]
        public async Task DeleteCompany(int id)
        {
            await _companyRepository.DeleteAsync(id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        //public  List<Company> CreateFilteredQuery(PagedCompanyResultRrequestDto input)
        //{
        //    var companies = (_companyRepository.GetAll()
        //        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))).ToList();
        //    return companies;
        //}
        public async Task<PagedResultDto<Company>> CreateFilteredQuery(PagedCompanyResultRrequestDto input)
        {
            var x = _companyRepository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(input.Keyword.ToLower()))
                .OrderByDescending(c => c.Name);

            var totalCount = await x.CountAsync();
            var list = await x.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<Company> {
                Items = list,
                TotalCount = totalCount
            };
        }
        public async Task<Company> GetRandCompany()
        {
            var query = await (
                from com in _companyRepository.GetAll()
                select com).ToListAsync();
            var comRand = query.RandCar();
            return comRand;
        }
    }
}
