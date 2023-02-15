using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> messages = new List<KeyValuePair<string, string>>();
        public IActionResult Show()
        {
            if (messages.Count < 1)
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Messages = messages.Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    MessageText = m.Value
                }).ToList()
            };
            return View(chatModel);
        }
        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMesage = chat.CurrentMessage;
            messages.Add(new KeyValuePair<string, string>(newMesage.Sender, newMesage.MessageText));
            return RedirectToAction("Show");
        }
    }
}
