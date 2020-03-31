using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Authorization;
using Project1.Cars.Dto;
using Project1.Entities;
using Project1.Transactions.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Project1.Cars
{

    //[AbpAuthorize(PermissionNames.Pages_Cars)]
    public class CarAppService : ApplicationService
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        public static IWebHostEnvironment _environment;
        public CarAppService(IRepository<Car> carRepository, IRepository<Company> companyRepository, IRepository<Transaction> transactionRepository, IWebHostEnvironment environment)
        {
            _carRepository = carRepository;
            _companyRepository = companyRepository;
            _transactionRepository = transactionRepository;
            _environment = environment;
        }
        public async Task<List<GetCarDto>> GetAllCars()
        {
            var query =
                (
                from car in _carRepository.GetAll()
                join company in _companyRepository.GetAll()
                on car.CompanyId equals company.Id
                orderby car.Name ascending
                select new GetCarDto
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    Inventory = car.Inventory,
                    Price = car.Price,
                    Total = car.Total,
                    CarImage=car.CarImage,
                    CompanyName = company.Name,
                    Transactions = (
                    from transaction in _transactionRepository.GetAll()
                    where transaction.CarId == car.Id
                    select new TransactionDto
                    {
                        Id = transaction.Id,
                        BuyDate = transaction.BuyDate
                    }).ToList()
                });
            var cars = await query.ToListAsync();
            return cars;
        }
        public async Task<GetCarDto> GetCar(int id)
        {
            var query = await
                (
                from car in _carRepository.GetAll()
                join company in _companyRepository.GetAll()
                on car.CompanyId equals company.Id
                where car.Id == id
                select new GetCarDto
                {
                    Id = car.Id,
                    Name = car.Name,
                    Description = car.Description,
                    Inventory = car.Inventory,
                    Price = car.Price,
                    Total = car.Total,
                    CarImage = car.CarImage,
                    CompanyName = company.Name,
                    Transactions = (
                    from transaction in _transactionRepository.GetAll()
                    where transaction.CarId == car.Id
                    select new TransactionDto
                    {
                        Id = transaction.Id,
                        BuyDate = transaction.BuyDate
                    }).ToList()
                }).FirstOrDefaultAsync();
            return query;
        }

        //[AbpAuthorize(PermissionNames.Pages_Cars_Create)]
        public async Task CreateCar(CreateCarDto input)
        {
            var car = new Car
            {
                Name = input.Name,
                Description = input.Description,
                Inventory = input.Inventory,
                Price = input.Price,
                Total = input.Total,
                CompanyId = input.CompanyId,
                CarImage= "http://localhost:21021/cars/image/default.jpg"
            };
            await _carRepository.InsertAsync(car);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        //[AbpAuthorize(PermissionNames.Pages_Cars_Update)]
        public async Task UpdateCar(CarDto input)
        {
            var car = await _carRepository.GetAsync(input.Id);
            car.Name = input.Name;
            car.Description = input.Description;
            car.Inventory = input.Inventory;
            car.Price = input.Price;
            car.Total = input.Total;
            car.CarImage = input.CarImage;
            car.CompanyId = input.CompanyId;
            await _carRepository.UpdateAsync(car);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(PermissionNames.Pages_Cars_Delete)]
        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteAsync(id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task<List<GetCarDto>> GetCarsByCompany(int id)
        {
            var query =
               (
               from car in _carRepository.GetAll()
               where car.CompanyId == id
               orderby car.Name ascending
               select new GetCarDto
               {
                   Name = car.Name,
                   Description = car.Description,
                   Inventory = car.Inventory,
                   Price = car.Price,
                   Total = car.Total,
                   CarImage = car.CarImage,
               });
            var cars = await query.ToListAsync();
            return cars;
        }
        public async Task<PagedResultDto<GetCarDto>> CreateFilteredQuery(PagedCarResultRequestDto input)
        {
            var x = _carRepository.GetAll()

                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(input.Keyword.ToLower()) || x.Description.ToLower().Contains(input.Keyword.ToLower()));

            var totalCount = await x.CountAsync();
            var list = await x.Skip(input.SkipCount).Take(input.MaxResultCount)
                .Select(c => new GetCarDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Inventory = c.Inventory,
                    Price = c.Price,
                    Total = c.Total,
                    CarImage = c.CarImage,
                    CompanyName = _companyRepository.GetAll().Where(com => com.Id == c.CompanyId).Select(com => com.Name).SingleOrDefault()
                })
                .ToListAsync();

            //var xn = list.IsNullOrEmpty();

            return new PagedResultDto<GetCarDto>
            {
                Items = list,
                TotalCount = totalCount
            };
        }
        //public async Task<Car> GetRandomCar()
        //{
        //    var query = await (
        //        from car in _carRepository.GetAll()
        //        select car).ToListAsync();
        //    var carRand = query.RandCar();
        //    return carRand;

        //}
        //public async Task<string> GetString(string strin)
        //{
        //    string strout = strin.NotSpace();
        //    return strout;
        //}

        [HttpPost]
        public async Task<string> UploadImage(IFormFile filein)
        {

            try
            {
                if (filein.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Cars\\Image\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Cars\\Image\\");
                    }
                    using (FileStream fileStream = File.Create(_environment.WebRootPath + "\\Cars\\Image\\" + filein.FileName))
                    {
                        await filein.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return "/Cars/Image/" + filein.FileName;
                    }
                }
                else
                {
                    return "/Cars/Image/" + "default.jpg";
                }

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}
