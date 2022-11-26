using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Entities;
public class Category : EntityBase
{
    [Key]
    public int Id { set; get; }
    [Required]
    [StringLength(50)]
    public string Name { set; get; }
    public string Desc { set; get; }
    [JsonIgnore]
    public virtual List<Product> Products { get; set; }
}