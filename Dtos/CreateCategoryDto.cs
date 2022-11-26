using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Dtos;
public class CreateCategoryDto
{
    [Required]
    [StringLength(50)]
    public string Name { set; get; }
    public string Desc { set; get; }
}