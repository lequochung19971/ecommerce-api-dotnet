using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Entities;
public class FileModel : EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public long Size { get; set; }
    public string BlobName { get; set; }
    public virtual Product Product { get; set; }
}