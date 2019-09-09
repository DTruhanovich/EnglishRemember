using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace EnglishRemember
{
    internal class Program
    {
        private static int attempt;

        private static void Main()
        {
            Start();
        }

        private static void Start()
        {
            var checkWords = Initialize();

            while (true)
            {
                ConsoleConfigurationService.PrintMenu();

                var result = checkWords.GetAnswer();
                Console.WriteLine($"{result.Key}: ");
                var answer = Console.ReadLine();

                if (answer == "x" || answer == "X")
                {
                    break;
                }
                if (answer == "E" || answer == "e")
                {
                    ConsoleConfigurationService.SetOption(checkWords);
                    continue;
                }

                RetryAnswer(result, answer);
            }
        }

        private static ICheckWords Initialize()
        {
            attempt = int.Parse(ConfigurationManager.AppSettings["Attempt"]);
            string fileName = ConfigurationManager.AppSettings["FileName"];
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(fileName, Encoding.GetEncoding(1251)));
            var checkWords = new CheckWords(dictionary)
            {
                LanguageType = (LanguageType)Enum.Parse(typeof(LanguageType), ConfigurationManager.AppSettings["LanguageType"], true)
            };
            return checkWords;
        }

        private static void RetryAnswer(KeyValuePair<string, List<string>> result, string answer)
        {
            int i = 1;
            while (i < attempt)
            {
                if (answer == "x" || answer == "X")
                {
                    break;
                }

                if (result.Value.Contains(answer.ToLower()))
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect!");
                Console.ForegroundColor = ConsoleColor.White;
                answer = Console.ReadLine();
                i++;
            }

            if (i == attempt)
            {
                Console.WriteLine($"Correct answer is: {result.Value.First()}");
                Console.ReadLine();
            }
        }
    }
}