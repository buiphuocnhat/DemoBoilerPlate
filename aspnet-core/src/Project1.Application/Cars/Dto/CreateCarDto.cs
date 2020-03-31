using Microsoft.AspNetCore.Http;

namespace Project1.Cars.Dto
{
    public class CreateCarDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string CarImage { get; set; }

        public int CompanyId { get; set; }

    }
}
