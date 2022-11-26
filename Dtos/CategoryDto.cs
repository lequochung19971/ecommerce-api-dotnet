using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Dtos;
public class CategoryDto
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string Desc { set; get; }
    // public List<ProductDto> Products { get; set; }
}