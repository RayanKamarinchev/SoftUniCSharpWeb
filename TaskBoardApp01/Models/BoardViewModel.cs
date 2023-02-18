using TaskBoardApp01.Models.Tasks;
using Task = TaskBoardApp01.Data.Entities.Task;

namespace TaskBoardApp01.Models
{
    public class BoardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
