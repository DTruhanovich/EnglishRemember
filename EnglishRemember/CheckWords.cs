using EnglishRemember.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishRemember
{
    internal class CheckWords : ICheckWords
    {
        private const int StartValue = 0;

        private readonly Dictionary<string, List<string>> _dictionary;
        private readonly List<string> _keys;

        public LanguageType LanguageType { get; set; }

        public CheckWords(Dictionary<string, List<string>> dictionary)
        {
            _dictionary = dictionary;
            _keys = _dictionary.Select(m => m.Key).ToList();
        }

        public KeyValuePair<string, List<string>> GetAnswer()
        {
            var result = GetWordForCheck(_dictionary);

            switch (LanguageType)
            {
                case LanguageType.English:
                    return GetResultEng(result);

                case LanguageType.Russian:
                    return GetResultRus(result);

                default:
                    return GetResultRandom(result);
            }
        }

        private static KeyValuePair<string, List<string>> GetResultRandom(KeyValuePair<string, List<string>> result)
        {
            var random = new Random();

            if (random.Next(0, 2) == 0)
            {
                return GetResultRus(result);
            }
            else
            {
                return GetResultEng(result);
            }
        }

        private static KeyValuePair<string, List<string>> GetResultRus(KeyValuePair<string, List<string>> result)
        {
            return new KeyValuePair<string, List<string>>(result.Key, result.Value);
        }

        private static KeyValuePair<string, List<string>> GetResultEng(KeyValuePair<string, List<string>> result)
        {
            var random = new Random();
            return new KeyValuePair<string, List<string>>(result.Value[random.Next(0, result.Value.Count)], new List<string> { result.Key });
        }

        private KeyValuePair<string, List<string>> GetWordForCheck(Dictionary<string, List<string>> dictionary)
        {
            var random = new Random();
            var index = random.Next(StartValue, dictionary.Count - 1);
            return dictionary.GetEntry(_keys[index]);
        }
    }
}