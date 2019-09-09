using System;

namespace EnglishRemember
{
    internal static class ConsoleConfigurationService
    {
        internal static void PrintMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Print 'X' or 'x' for exit");
            Console.WriteLine("Print 'E' or 'e' for open menu");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("=====================================");
        }

        internal static void SetOption(ICheckWords checkWords)
        {
            while (true)
            {
                Console.WriteLine("Print 'T' or 't' for set Language type");
                Console.WriteLine("Print 'B' or 'b' for back in program");

                var optionType = Console.ReadLine();
                if (optionType == "T" || optionType == "t")
                {
                    Console.WriteLine("Set language type:");
                    if (Enum.TryParse<LanguageType>(Console.ReadLine(), out var languageType))
                    {
                        checkWords.LanguageType = languageType;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data. You can write only: ");
                        foreach (var item in Enum.GetValues(typeof(LanguageType)))
                        {
                            Console.Write($"{item} ");
                        }
                    }
                }

                if (optionType == "B" || optionType == "b")
                {
                    break;
                }
            }

            Console.ReadLine();

            PrintMenu();
        }
    }
}