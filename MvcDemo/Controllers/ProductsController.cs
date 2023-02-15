using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 7.00m
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 5.50m
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 1.50m
            }
        };
        public IActionResult All(string keyword)
        {
            if (!(keyword is null))
            {
                var foundProducts = this.products.Where(pr => pr.Name.ToLower().Contains(keyword.ToLower()));
                return View(foundProducts);
            }
            return View(this.products);
        }

        public IActionResult ById(int id)
        {
            ProductViewModel pr = products.FirstOrDefault(p => p.Id == id);
            if (pr is null)
            {
                return BadRequest();
            }
            return View(pr);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            return Json(products, options);
        }

        public IActionResult AllAsText()
        {
            return Content("Average product");
        }

        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pr in products)
            {
                sb.AppendLine($"Product {pr.Id}: {pr.Name} - {pr.Price:f2}lv");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;file=products.txt");
            return File(Encoding.UTF8.GetBytes(sb.ToString().TrimEnd()), "text/plain");
        }
    }
}
