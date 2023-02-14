using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
