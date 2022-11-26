using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Entities
{
    public class Product : EntityBase
    {
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(50)]
        public string Name { set; get; }
        public string Desc { set; get; }
        public string Sku { set; get; }
        public decimal Price { set; get; }
        public int CategoryId { set; get; }
        public virtual Category Category { set; get; }
    }
}