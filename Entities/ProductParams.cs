using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Entities;
public class ProductParams : QueryStringParams
{
    public ProductParams()
    {
        SortColumn = nameof(Product.Name);
    }
    [FromQuery(Name = "categoryId")]
    public int CategoryId { get; set; }
}