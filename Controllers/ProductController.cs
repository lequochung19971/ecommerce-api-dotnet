using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Castle.Core.Internal;
using Ecommerce.Dtos;
using Ecommerce.Entities;
using Ecommerce.Exceptions;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepo;
    private readonly IFileRepository _fileRepo;
    private readonly IMapper _mapper;
    private readonly IAzureStorageAccountService _azureStorageAccountService;

    public ProductController(IProductRepository productRepo, IFileRepository fileRepo, IMapper mapper, IAzureStorageAccountService azureStorageAccountService)
    {
        _productRepo = productRepo;
        _fileRepo = fileRepo;
        _mapper = mapper;
        _azureStorageAccountService = azureStorageAccountService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedData<List<ProductDto>>>> GetProducts([FromQuery] ProductParams productParams)
    {
        try
        {
            var data = await _productRepo.QueryAsync(productParams);
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
            var product = _productRepo.FindByCondition(p => p.Id == id).FirstOrDefault();
            return Ok(_mapper.Map<ProductDto>(product));
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

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromForm] CreateProductDto productDto)
    {
        var fileModelsForDeleteWhenError = new List<FileModel>();
        try
        {
            var category = _productRepo.FindByCondition(p => p.CategoryId == productDto.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return NotFound("Category Not Found");
            }

            var formFiles = productDto.Images;
            productDto.Images = null;
            var product = _mapper.Map<Product>(productDto);
            _productRepo.Create(product);

            if (formFiles != null)
            {
                var files = await UploadFiles(formFiles, product);

                if (files != null)
                {
                    fileModelsForDeleteWhenError.AddRange(files);
                    _fileRepo.CreateRange(files);
                }
            }

            await _fileRepo.SaveChangesAsync();                
            return CreatedAtAction(nameof(getProduct), new { Id = product.Id }, new { id = product.Id });
        }
        catch (System.Exception exception)
        {
            var blobNames = fileModelsForDeleteWhenError.Select(f => f.BlobName.ToString()).ToList();
            DeleteFiles(blobNames);

            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        try
        {
            var product = _productRepo.FindByCondition(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound("Product Not Found");

            }

            _productRepo.Delete(product);
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

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto productDto)
    {
        var fileModelsForDeleteWhenError = new List<FileModel>();
        try
        {
            var product = _productRepo.FindByCondition(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound("Product Not Found");
            }
            var newFiles = productDto.Images?.Where(i => i.File != null).Select(i => i.File).ToList();
            var oldFileModelIds = productDto.Images?.Where(i => i.Id != null && !i.Url.IsNullOrEmpty()).Select(i => i.Id).ToList();

            var currentProductFileModels = _fileRepo.FindByCondition(f => f.Product == product).ToList();
            var deletedFileModels = new List<string>();

            // Delete Unused File Models.
            if (currentProductFileModels != null && oldFileModelIds != null)
            {
                foreach (var fileModel in currentProductFileModels)
                {
                    var isNotContained = !oldFileModelIds.Contains(fileModel.Id);
                    if (isNotContained)
                    {
                        deletedFileModels.Add(fileModel.BlobName);
                        _fileRepo.Delete(fileModel);
                    }
                }
            }

            if (deletedFileModels.Count > 0)
            {
                DeleteFiles(deletedFileModels);
            }

            if (newFiles != null)
            {
                var files = await UploadFiles(newFiles, product);

                if (files != null)
                {
                    fileModelsForDeleteWhenError.AddRange(files);
                    _fileRepo.CreateRange(files);
                }
            }

            _productRepo.Update(_mapper.Map<Product>(productDto));
            await _productRepo.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception exception)
        {
            var blobNames = fileModelsForDeleteWhenError.Select(f => f.BlobName.ToString()).ToList();
            DeleteFiles(blobNames);
            throw new AppException()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = exception.ToString()
            };
        }
    }

    private async Task<List<FileModel>> UploadFiles(List<IFormFile> files, Product product)
    {
        if (files.Count > 0)
        {
            var resultFiles = new List<FileModel>();
            foreach (var file in files)
            {
                var blob = await _azureStorageAccountService.UploadFileAsync(file);
                if (blob != null)
                {
                    var fileModel = _mapper.Map<FileModel>(file);
                    fileModel.Url = blob.Uri.ToString();
                    fileModel.BlobName = blob.Name;
                    fileModel.Product = product;
                    resultFiles.Add(fileModel);
                }

            }

            return resultFiles;
        }

        return null;
    }

    private async Task DeleteFiles(List<string> blobNames)
    {

        if (blobNames.Count > 0)
        {
            foreach (var blobName in blobNames)
            {
                await _azureStorageAccountService.DeleteFileAsync(blobName);
            }
        }

    }
}