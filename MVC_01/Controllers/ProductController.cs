using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MVC_01.Controllers
{
    /// <summary>
    /// WEb Shop products
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        /// <summary>
        /// List of all products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAll();
            ViewData["Title"] = "Products";
            return View(products);
        }
    }
}
