using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRenting.Data.Entities
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 30)]
        public string Address { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 50)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [Range(0, 2000)]
        public decimal PricePerMonth { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        [Required]
        public int AgentId { get; set; }
        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; }
        public string? RenterId { get; set; }
        public User Renter { get; set; }
    }
}
