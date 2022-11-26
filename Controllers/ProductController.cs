using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Dtos;
using Ecommerce.Entities;
using Ecommerce.Exceptions;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedData<List<ProductDto>>>> getProducts([FromQuery] ProductParams productParams)
    {
        try
        {
            var data = await _repo.GetProductsAsync(productParams);
            // throw new Exception("Le Quoc Hung");
            return Ok(new PagedData<List<ProductDto>>()
            {
                Data = _mapper.Map<List<ProductDto>>(data),
                TotalCount = data.TotalCount,
            });
        }
        catch (System.Exception exception)
        {
            return BadRequest(exception.ToString());
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> getProduct(int id)
    {
        try
        {
            var product = await _repo.GetProductAsync(id);
            return Ok(_mapper.Map<ProductDto>(product));
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = exception.ToString()
            };
        }
    }

    [HttpPost]
    public async Task<ActionResult> createProduct(CreateProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            await _repo.CreateProductAsync(product);
            return CreatedAtAction(nameof(getProduct), new { Id = product.Id }, _mapper.Map<ProductDto>(product));
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = exception.ToString()
            };
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteProduct(int id)
    {
        try
        {
            await _repo.DeleteProductAsync(id);
            return Ok();
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = exception.ToString()
            };
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> updateProduct(int id, UpdateProductDto productDto)
    {
        try
        {
            await _repo.UpdateProductAsync(id, _mapper.Map<Product>(productDto));
            return Ok();
        }
        catch (System.Exception exception)
        {
            if (exception is ArgumentNullException)
            {
                throw new AppException()
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = exception.ToString()
                };
            }
            throw new AppException()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Detail = exception.ToString()
            }; ;
        }
    }
}