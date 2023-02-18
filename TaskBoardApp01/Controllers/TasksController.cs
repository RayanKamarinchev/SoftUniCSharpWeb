using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using TaskBoardApp01.Data;
using TaskBoardApp01.Data.Common;
using TaskBoardApp01.Data.Entities;
using TaskBoardApp01.Models.Tasks;
using Task = TaskBoardApp01.Data.Entities.Task;

namespace TaskBoardApp01.Controllers
{
    public class TasksController : Controller
    {
        private readonly IRepository repository;
        public TasksController(ApplicationDbContext context)
        {
            repository = new Repository(context);
        }
        public IActionResult Create()
        {
            TaskFormModel taskForm = new TaskFormModel()
            {
                Boards = GetBoards()
            };

            return View(taskForm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (GetBoards().All(b => b.Id != model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Task task = new Task()
            {
                Title = model.Title,
                Description = model.Description,
                BoardId = model.BoardId,
                OwnerId = currentUserId,
                CreatedOn = DateTime.Now
            };
            await repository.AddAsync<Task>(task);
            await repository.SaveChangesAsync();

            return RedirectToAction("Index", "Boards");
        }

        private IEnumerable<TaskBoardModel> GetBoards()
        {
            return repository.All<Board>().Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var t = await repository.All<Task>()
                                    .Include(t => t.Board)
                                    .Include(t => t.Owner)
                                    .FirstOrDefaultAsync(t => t.Id == id);

            if (t==null)
            {
                return BadRequest();
            }

            TaskDetailsViewModel task = new TaskDetailsViewModel()
            {
                Id = t.Id,
                Board = t.Board.Name,
                CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                Owner = t.Owner.UserName,
                Description = t.Description
            };
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var t = await repository.All<Task>()
                                    .Include(t => t.Board)
                                    .Include(t => t.Owner)
                                    .FirstOrDefaultAsync(t => t.Id == id);
            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != t.OwnerId)
            {
                return Unauthorized();
            }
            TaskFormModel task = new TaskFormModel()
            {
                Title = t.Title,
                BoardId = t.BoardId,
                Boards = GetBoards(),
                Description = t.Description
            };
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel model)
        {
            var t = await repository.All<Task>()
                                    .Include(t => t.Board)
                                    .Include(t => t.Owner)
                                    .FirstOrDefaultAsync(t => t.Id == id);
            if (t == null)
            {
                return BadRequest();
            }

            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != t.OwnerId)
            {
                return Unauthorized();
            }

            if (GetBoards().All(b=>b.Id==t.Id))
            {
                ModelState.AddModelError(nameof(t.BoardId), "Board does not exist");
            }

            t.Title = model.Title;
            t.Description = model.Description;
            t.BoardId = model.BoardId;

            await repository.SaveChangesAsync();
            return RedirectToAction("Index", "Boards");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await repository.All<Task>()
                                       .Include(t => t.Board)
                                       .Include(t => t.Owner)
                                       .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel()
            {
                Id = id,
                Title = task.Title,
                Description = task.Description
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            var task = await repository.All<Task>()
                                       .Include(t => t.Board)
                                       .Include(t => t.Owner)
                                       .FirstOrDefaultAsync(t => t.Id == model.Id);
            if (task == null)
            {
                return BadRequest();
            }

            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUser != task.OwnerId)
            {
                return Unauthorized();
            }

            await repository.DeleteAsync<Task>(task.Id);
            await repository.SaveChangesAsync();
            return RedirectToAction("Index", "Boards");
        }
    }
}
