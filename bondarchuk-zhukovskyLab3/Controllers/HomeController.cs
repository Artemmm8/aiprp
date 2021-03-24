
using Microsoft.AspNetCore.Mvc;

namespace bondarchuk_zhukovskyLab3.Controllers
{
    public class HomeController : Controller
    {
        // private readonly GlossaryContext _db;
        private readonly dictionarydbContext _dictionarydb;

        public HomeController(dictionarydbContext context)
        {
            // _db = context;
            this._dictionarydb = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DictionaryBook glossary)
        {
            if (glossary.EnglishWord is null && glossary.RussianWord is null)
            {
                return View();
            }
            else if (glossary.EnglishWord is null)
            {
                ViewBag.RussianWord = glossary.RussianWord;
                string russianWord = glossary.RussianWord.ToLower();
                foreach (var item in _dictionarydb.DictionaryBooks)
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
                foreach (var item in _dictionarydb.DictionaryBooks)
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