using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http;
using Project1.Transactions.Dto;
using System.Collections.Generic;

namespace Project1.Cars.Dto
{
    public class GetCarDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string CarImage { get; set; }
        public string CompanyName { get; set; }

        public List<TransactionDto> Transactions { get; set; }
    }
}
