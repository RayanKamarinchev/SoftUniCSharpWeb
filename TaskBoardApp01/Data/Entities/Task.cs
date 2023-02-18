using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp01.Data.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BoardId { get; set; }
        [ForeignKey(nameof(BoardId))]
        public Board Board { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }
    }
}
