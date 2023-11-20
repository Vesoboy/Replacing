using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Replacing.DB;
using Replacing.Models;
using Replacing.Service;

namespace Replacing.Controllers
{
    public class ReplaceController : Controller
    {
        private readonly ILogger<ReplaceController> _logger;
        private readonly DataContext _context;

        public ReplaceController (ILogger<ReplaceController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public async Task<IActionResult> ReplaceWord(int page = 1)
        {
            int pageSize = 20;
            var alphabetReplacements = _context.ReplaceWords.ToList();
            ViewBag.Word = alphabetReplacements;

            var totalCountWord = await _context.ReplaceWords.CountAsync();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCountWord / (double)pageSize);
            ViewBag.CurrentPage = page;

            var view = await _context.ReplaceWords
                .OrderBy(m=> m.OldSymbol)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(view);

        }

        public IActionResult AddWord(ReplaceWord repWord)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(repWord.OldSymbol))
                {
                    return RedirectToAction("ReplaceWord");
                }

                var existingRecord = _context.ReplaceWords.FirstOrDefault(rw => rw.OldSymbol == repWord.OldSymbol);
                if (existingRecord != null)
                {
                    return RedirectToAction("ReplaceWord");
                }

                _context.ReplaceWords.Add(repWord);
                _context.SaveChanges();
            }
            return RedirectToAction("ReplaceWord");
        }

        public IActionResult DeleteWord(ReplaceWord repWord)
        {
            var delWord = _context.ReplaceWords.FirstOrDefault(p => p.OldSymbol == repWord.OldSymbol);
            if (delWord != null)
            {
                _context.ReplaceWords.Remove(delWord);
                _context.SaveChanges();
            }
            return RedirectToAction("ReplaceWord");
        }
    }
}
