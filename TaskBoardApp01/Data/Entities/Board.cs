using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp01.Data.Entities
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
