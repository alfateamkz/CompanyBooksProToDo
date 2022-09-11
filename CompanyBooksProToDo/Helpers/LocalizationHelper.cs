using CompanyBooksProToDo.Resources.Lang;
using System;

namespace CompanyBooksProToDo.Helpers
{
    public static class LocalizationHelper
    {
        public static string GetLangStr(enumLanguagesEnum val)
        {
            
            switch (val)
            {
                case enumLanguagesEnum.langEnglish:
                    return "English";
                case enumLanguagesEnum.langHebrew:
                    return "עִברִית";
                case enumLanguagesEnum.langSpanish:
                    return "español";
                case enumLanguagesEnum.langChinese:
                    return "中国人";
                case enumLanguagesEnum.langJapanese:
                    return "日本";
                case enumLanguagesEnum.langArabic:
                    return "عرب";
                case enumLanguagesEnum.langFrench:
                    return "Français";
                case enumLanguagesEnum.langGerman:
                    return "Deutsch";
                case enumLanguagesEnum.langRussian:
                    return "Русский";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetLangCode(enumLanguagesEnum val)
        {

            switch (val)
            {
                case enumLanguagesEnum.langEnglish:
                    return "en";
                case enumLanguagesEnum.langHebrew:
                    return "iw";
                case enumLanguagesEnum.langSpanish:
                    return "es";
                case enumLanguagesEnum.langChinese:
                    return "zh-CN";
                case enumLanguagesEnum.langJapanese:
                    return "ja";
                case enumLanguagesEnum.langArabic:
                    return "ar";
                case enumLanguagesEnum.langFrench:
                    return "fr";
                case enumLanguagesEnum.langGerman:
                    return "de";
                case enumLanguagesEnum.langRussian:
                    return "ru";
                default:
                    throw new NotImplementedException();
            }
        }
        public static string GetLocalizedWord(string key)
        {
            var currentLang = ApiHelper.GetCurrentLanguage();
            switch (currentLang)
            {
                case enumLanguagesEnum.langEnglish:
                    return EnglishDictionary.Words[key];
                case enumLanguagesEnum.langHebrew:
                    return HebrewDictionary.Words[key];
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
