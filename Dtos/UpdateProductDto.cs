using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Enums;

namespace Ecommerce.Dtos
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(50)]
        public string? Name { set; get; }
        public string Desc { set; get; }
        public ProductTypes type { set; get; }
        public decimal Price { set; get; }
        public int CategoryId { set; get; }
        public List<UpdateFileModelDto>? Images { set; get; }
    }
}