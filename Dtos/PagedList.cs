using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Entities;
public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int? TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int? TotalCount { get; private set; }

    public PagedList(List<T> items, int? totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = totalCount != null ? (int)Math.Ceiling((int)totalCount / (double)pageSize) : null;
        AddRange(items);
    }

}