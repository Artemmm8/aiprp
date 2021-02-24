using System.Linq;
using bondarchuk_zhukovskyLab3.Models;

namespace bondarchuk_zhukovskyLab3
{
    public static class SampleData
    {
        public static void Initialize(GlossaryContext context)
        {
            if (!context.Glossaries.Any())
            {
                context.Glossaries.AddRange(
                    new Glossary
                    {
                        RussianWord = "стол",
                        EnglishWord = "table"
                    },
                    new Glossary
                    {
                        RussianWord = "мышь",
                        EnglishWord = "mouse"
                    },
                    new Glossary
                    {
                        RussianWord = "тело",
                        EnglishWord = "body"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}