using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Dtos;

public class PagedData<T>
{
    public T Data { get; set; }
    public int? TotalCount { get; set; }
}