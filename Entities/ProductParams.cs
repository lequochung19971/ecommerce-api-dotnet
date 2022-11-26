using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Entities;
public class ProductParams : QueryStringParams
{
    public ProductParams()
    {
        SortColumn = nameof(Product.Name);
    }
    public int CategoryId { get; set; }
}