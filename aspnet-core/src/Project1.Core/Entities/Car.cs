using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.Entities
{
    [Table("Test.Car")]
    public class Car : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int CompanyId { get; set; }
        public string CarImage { get; set; }
    }
}
