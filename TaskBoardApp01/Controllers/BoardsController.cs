using Microsoft.AspNetCore.Mvc;
using TaskBoardApp01.Data;
using TaskBoardApp01.Data.Common;
using TaskBoardApp01.Data.Entities;
using TaskBoardApp01.Models;
using TaskBoardApp01.Models.Tasks;

namespace TaskBoardApp01.Controllers
{
    public class BoardsController : Controller
    {
        private readonly IRepository repository;

        public BoardsController(ApplicationDbContext context)
        {
            repository = new Repository(context);
        }

        public IActionResult Index()
        {
            var boards = repository.All<Board>()
                                   .Select(b => new BoardViewModel()
                                   {
                                       Id = b.Id,
                                       Name = b.Name,
                                       Tasks = b.Tasks.Select(t => new TaskViewModel()
                                       {
                                           Description = t.Description,
                                           Id = t.Id,
                                           Owner = t.Owner.UserName,
                                           Title = t.Title
                                       }).ToList()
                                   }).ToList();
            return View(boards);
        }
    }
}
