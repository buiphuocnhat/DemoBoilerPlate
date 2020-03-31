using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project1.Entities
{
    [Table("Test.Transaction")]
    public class Transaction: Entity
    {
        public int CarId { get; set; }
        public DateTime BuyDate { get; set; }
        public int Number { get; set; }
        public decimal Total { get; set; }
    }
}
