using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Dtos
{
    public class ProductDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Desc { set; get; }
        public string Sku { set; get; }
        public string Test { set; get; }
        public decimal Price { set; get; }
        public Category Category { set; get; }
    }
}