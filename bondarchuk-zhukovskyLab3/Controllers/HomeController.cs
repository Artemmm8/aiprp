using bondarchuk_zhukovskyLab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace bondarchuk_zhukovskyLab3.Controllers
{
    public class HomeController : Controller
    {
        GlossaryContext db;

        public HomeController(GlossaryContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Glossary glossary)
        {
            if (glossary.EnglishWord is null && glossary.RussianWord is null)
            {
                return View();
            }
            else if (glossary.EnglishWord is null)
            {
                ViewBag.RussianWord = glossary.RussianWord;
                string russianWord = glossary.RussianWord.ToLower();
                foreach (var item in db.Glossaries)
                {
                    if (item.RussianWord == russianWord)
                    {
                        ViewBag.EnglishWord = item.EnglishWord;
                        return View();
                    }
                }
            }
            else if (glossary.RussianWord is null)
            {
                ViewBag.EnglishWord = glossary.EnglishWord;
                string englishWord = glossary.EnglishWord.ToLower();
                foreach (var item in db.Glossaries)
                {
                    if (item.EnglishWord == englishWord)
                    {
                        ViewBag.RussianWord = item.RussianWord;
                        return View();
                    }
                }
            }

            return View();
        }
    }
}
