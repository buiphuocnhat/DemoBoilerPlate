using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Transactions.Dto
{
    public class TransactionDto : EntityDto
    {
        public int CarId { get; set; }
        public DateTime BuyDate { get; set; }
        public int Number { get; set; }
        public decimal Total { get; set; }
    }
}
