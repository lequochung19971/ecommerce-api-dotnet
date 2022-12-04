using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Entities;

namespace Ecommerce.Repositories;
public interface IFileRepository : IBaseRepository<FileModel>
{
    void CreateRange(List<FileModel> fileModels);

}