using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Replacing.DB;
using Replacing.Models;
using Replacing.Service;
using System.Diagnostics;

namespace Replacing.Controllers
{
    public class HomeController : Controller
    {
        private readonly EncryptionService _encryptionService;
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext, EncryptionService encryptionService)
        {
            _logger = logger;
            _context = dataContext;
            _encryptionService = encryptionService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var text = _context.EncryptedMessages.ToList();
            ViewBag.Text = text;

            var totalCountText = await _context.EncryptedMessages.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCountText / (double)pageSize);
            ViewBag.CurrentPage = page;

            var view = await _context.EncryptedMessages
                .OrderByDescending(d => d.ReceivedTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(view);
        }

        public IActionResult AddMessage(EncryptedMessage encMes)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(encMes.OriginalMessage))
                {
                    return RedirectToAction("Index");
                }

                encMes.EncryptedMessageText = _encryptionService.EncryptText(encMes.OriginalMessage);
                _context.EncryptedMessages.Add(encMes);
                encMes.ReceivedTime = DateTime.Now;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteModal(EncryptedMessage encMes)
        {
            var delMess = _context.EncryptedMessages.FirstOrDefault(p => p.MessageId == encMes.MessageId);
            if (delMess != null)
            {
                _context.EncryptedMessages.Remove(delMess);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
