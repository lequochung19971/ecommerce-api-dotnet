using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ecommerce.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Entities;
public abstract class QueryStringParams
{
    const int maxPageSize = 100;
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;
    [FromQuery(Name = "requireTotalCount")]
    public bool RequireTotalCount { get; set; } = false;
    private int _pageSize = 10;
    [FromQuery(Name = "pageSize")]
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

    [FromQuery(Name = "searchKeywords")]
    public string? SearchKeywords { get; set; } = "";

    [FromQuery(Name = "sortColumn")]
    public string SortColumn { get; set; } = "";

    [EnumDataType(typeof(SortDirections))]
    [FromQuery(Name = "sortDirection")]
    public SortDirections SortDirection { get; set; } = SortDirections.ASC;
}