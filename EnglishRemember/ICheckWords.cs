using System.Collections.Generic;

namespace EnglishRemember
{
    internal interface ICheckWords
    {
        LanguageType LanguageType { get; set; }

        KeyValuePair<string, List<string>> GetAnswer();
    }
}