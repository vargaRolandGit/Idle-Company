using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem {
    public enum Language {
        English,
        Hungarian
    }

    public static Language Lang = Language.Hungarian;
    public static Dictionary<string, string> localEN;
    public static Dictionary<string, string> localHU;

    public static bool IsInit;
    public static void Init() { 
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();
        localEN = csvLoader.GetDictionaryValues("en");
        localHU = csvLoader.GetDictionaryValues("hu");

        IsInit = true;
    }

    public static string GetLocal(string id) {
        if(!IsInit) {Init();}
        string value = id;

        switch (Lang) { 
            case Language.English:
                localEN.TryGetValue(value, out value);
                break;
            case Language.Hungarian:
                localHU.TryGetValue(value, out value);
                break;
        }
        
        return value;
    }

}
