using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    TextAsset csvFile;
    char lineSeparator = '\n';
    string[] fieldSeparator = { "," };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localization");
        if (csvFile == null)
        {
            Debug.LogError("CSV file not found in Resources!");
        }
    }

    public Dictionary<string, string> GetDictionaryValues(string id)
    {
        var dictionary = new Dictionary<string, string>();
        if (csvFile == null)
        {
            Debug.LogError("CSV file not loaded. Call LoadCSV() first.");
            return dictionary;
        }

        var lines = csvFile.text.Split(lineSeparator);
        if (lines.Length == 0)
        {
            Debug.LogError("CSV file is empty.");
            return dictionary;
        }

        var attributeIndex = -1;
        var headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);

        // Find the requested column
        for (var i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(id))
            {
                attributeIndex = i;
                break;
            }
        }

        if (attributeIndex == -1)
        {
            Debug.LogError($"ID '{id}' not found in headers.");
            return dictionary;
        }

        // Regex for splitting
        var CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        // Parse each line
        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            var fields = CSVParser.Split(line);
            for (var f = 0; f < fields.Length; f++)
            {
                fields[f] = fields[f].Trim(' ', '"');
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];
                if (!dictionary.ContainsKey(key))
                {
                    var value = fields[attributeIndex];
                    dictionary.Add(key, value);
                }
            }
        }

        PrintDictionary(dictionary);
        return dictionary;
    }

    void PrintDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        Debug.Log("Dictionary contents:");
        foreach (var kvp in dictionary)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
