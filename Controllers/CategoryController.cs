using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Dtos;
using Ecommerce.Entities;
using Ecommerce.Exceptions;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _repo = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> getCategories()
    {
        try
        {
            var categories = await _repo.FindAll().ToListAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> getCategory(int id)
    {
        try
        {
            var category = await _repo.FindByCondition(c => c.Id == id).FirstOrDefaultAsync();
            return Ok(_mapper.Map<CategoryDto>(category));
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }

    }

    [HttpPost("")]
    public async Task<ActionResult> createCategory(CreateCategoryDto categoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(categoryDto);
            _repo.Create(category);
            await _repo.SaveChangesAsync();
            return CreatedAtAction(nameof(getCategory), new { Id = category.Id }, new { Id = category.Id });
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> updateCategory(int id, UpdateCategoryDto categoryDto)
    {
        try
        {
            var category = _repo.FindByCondition(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            _repo.Update(_mapper.Map<Category>(categoryDto));
            await _repo.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteCategory(int id)
    {
        try
        {
            var category = _repo.FindByCondition(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            _repo.Delete(category);
            await _repo.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception exception)
        {
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }
    }
}