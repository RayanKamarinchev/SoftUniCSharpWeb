using Microsoft.AspNetCore.Mvc;
using TextSplitter.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TextSplitter.Controllers
{
    [Route("text-split")]
    public class SplitTextController : Controller
    {
        public IActionResult Show(TextViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Split(TextViewModel newText)
        {
            newText.SplitText = newText.Text.Replace(" ", "\n");
            return RedirectToAction("Show", newText);
        }
    }
}
