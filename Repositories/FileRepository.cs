using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Entities;

namespace Ecommerce.Repositories;

public class FileRepository : BaseRepository<FileModel>, IFileRepository
{
    public FileRepository(DataContext context) : base(context)
    {
    }

    public void CreateRange(List<FileModel> fileModels)
    {
        _context.files.AddRange(fileModels);
    }
}