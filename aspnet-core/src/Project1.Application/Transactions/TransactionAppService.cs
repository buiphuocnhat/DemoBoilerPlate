using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Project1.Entities;
using Project1.Transactions.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project1.Transactions
{
    public class TransactionAppService : ApplicationService
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        public TransactionAppService(IRepository<Car> carRepository, IRepository<Company> companyRepository, IRepository<Transaction> transactionRepository)
        {
            _carRepository = carRepository;
            _companyRepository = companyRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<List<GetTransactionDto>> GetAllTransactions()
        {
            var query =

                from transaction in _transactionRepository.GetAll()
                join car in _carRepository.GetAll()
                on transaction.CarId equals car.Id
                select new GetTransactionDto
                {
                    Id = transaction.Id,
                    CarName = car.Name,
                    BuyDate = transaction.BuyDate,
                    Number = transaction.Number,
                    Total = transaction.Total
                };
            var transactions = await query.ToListAsync();
            return transactions;
        }
        public async Task<GetTransactionDto> GetTransaction(int id)
        {
            var query = await
                (
                from transaction in _transactionRepository.GetAll()
                join car in _carRepository.GetAll()
                on transaction.CarId equals car.Id
                where transaction.Id == id
                select new GetTransactionDto
                {
                    Id = transaction.Id,
                    CarName = car.Name,
                    BuyDate = transaction.BuyDate,
                    Number = transaction.Number,
                    Total = transaction.Total
                }
                ).FirstOrDefaultAsync();
            return query;
        }
        public async Task CreateTransation(CreateTransactionDto input)
        {
            var tran = new Transaction();
            var car = await _carRepository.GetAsync(input.CarId);
            tran.CarId = input.CarId;
            tran.BuyDate = input.BuyDate;
            tran.Number = input.Number;
            tran.Total = input.Number * car.Price;
            if (input.Number <= car.Inventory)
            {
                car.Inventory -= input.Number;
                car.Total += tran.Total;

            }
            else
            {
                throw new Exception("Input wrong");
            }
            await _transactionRepository.InsertAsync(tran);
            await _carRepository.UpdateAsync(car);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task UpdateTransaction(TransactionDto input)
        {
            var com = await _transactionRepository.GetAsync(input.Id);
            com.CarId = input.CarId;
            com.BuyDate = input.BuyDate;
            com.Number = input.Number;
            com.Total = input.Total;
            await _transactionRepository.UpdateAsync(com);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task DeleteTransaction(int id)
        {
            await _transactionRepository.DeleteAsync(id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        public async Task<List<GetTransactionDto>> GetTransactionsByCar(int id)
        {
            var query =
               (
               from tran in _transactionRepository.GetAll()
               where tran.CarId == id
               orderby tran.BuyDate ascending
               select new GetTransactionDto
               {
                   Id = tran.Id,
                   BuyDate=tran.BuyDate,
                   Number=tran.Number,
                   Total=tran.Total
               });
            
            var transactions = await query.ToListAsync();
            return transactions;
        }
    }

}
