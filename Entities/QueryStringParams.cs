using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Enums;

namespace Ecommerce.Entities;
public abstract class QueryStringParams
{
    const int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    public bool RequireTotalCount { get; set; } = false;
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }

    public string SearchKeywords { get; set; } = "";

    public string SortColumn { get; set; } = "";

    public SortDirections SortDirection { get; set; } = SortDirections.ASC;
}