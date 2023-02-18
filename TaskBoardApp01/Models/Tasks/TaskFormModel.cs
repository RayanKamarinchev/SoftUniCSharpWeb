using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp01.Models.Tasks
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(70, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; }
        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<TaskBoardModel> Boards { get; set; }
        
    }
}
