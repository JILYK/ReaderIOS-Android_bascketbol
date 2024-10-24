using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalizationConfig
{
    private static Dictionary<string, Dictionary<string, string>> languageData = new Dictionary<string, Dictionary<string, string>>()
    {
        // Данные для разных языков
        {
            "RU", new Dictionary<string, string>()
            {
                { "VNachaloText", "В НАЧАЛО" },
                { "SearchText", "Найти" },
                { "PlaceholderYaer", "Год" },
                { "PlaceholderName", "Выпуск" },
                { "VseJurMenuText", "ВСЕ ЖУРНАЛЫ" },
                { "YearText", "1992г" },
                { "NameText", "Выпуск" },
                { "FavoritTextMenu", "ИЗБРАННОЕ" },
                { "UpMenuText", "ВСЕ ЖУРНАЛЫ" }
            }
        },
        {
            "ENG", new Dictionary<string, string>()
            {
                { "VNachaloText", "START" },
                { "SearchText", "Search" },
                { "PlaceholderYaer", "Year" },
                { "PlaceholderName", "Issue" },
                { "VseJurMenuText", "ALL MAGAZINES" },
                { "YearText", "1992" },
                { "NameText", "Issue" },
                { "FavoritTextMenu", "FAVORITES" },
                { "UpMenuText", "ALL MAGAZINES" }
            }
        },
        {
            "BR", new Dictionary<string, string>()
            {
                { "VNachaloText", "INÍCIO" },
                { "SearchText", "Buscar" },
                { "PlaceholderYaer", "Ano" },
                { "PlaceholderName", "Edição" },
                { "VseJurMenuText", "TODAS AS REVISTAS" },
                { "YearText", "1992" },
                { "NameText", "Edição" },
                { "FavoritTextMenu", "FAVORITOS" },
                { "UpMenuText", "TODAS AS REVISTAS" }
            }
        }
    };

    public static Dictionary<string, string> GetLocalizationData(string language)
    {
        // Если данные для указанного языка есть, возвращаем их, иначе возвращаем данные для английского языка
        return languageData.ContainsKey(language) ? languageData[language] : languageData["ENG"];
    }
}
