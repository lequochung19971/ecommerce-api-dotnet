using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;
using Ecommerce.Enums;

namespace Ecommerce.Dtos
{
    public class ProductDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public ProductTypes type { set; get; }
        public string Desc { set; get; }
        public string Sku { set; get; }
        public List<FileModelDto> Images { set; get; }
        public decimal Price { set; get; }
        public string CategoryId { set; get; }
    }
}