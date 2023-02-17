using ForumApp.Data;
using ForumApp.Data.Entities;
using ForumApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext context;

        public PostsController(ForumAppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await context.Posts
                                     .Where(p=>!p.IsDeleted)
                                     .Select(p=>new PostViewModel()
                                     {
                                         Content = p.Content,
                                         Title = p.Title,
                                         Id = p.Id
                                     }).ToListAsync();
            return View(model);
        }

        public IActionResult Add() => View();


        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            var post = new Post()
            {
                Content = model.Content,
                Id = model.Id,
                Title = model.Title
            };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var post = context.Posts.Find(id);
            return View(new PostViewModel()
            {
                Content = post.Content,
                Title = post.Title
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,PostViewModel model)
        {
            var post = context.Posts.Find(id);
            post.Content = model.Content;
            post.Title = model.Title;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = context.Posts.Find(id);
            post.IsDeleted = true;
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
