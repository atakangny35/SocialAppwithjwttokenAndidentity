using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.DTO
{
    public class ProductDto
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool isActive { get; set; }
    }
}
