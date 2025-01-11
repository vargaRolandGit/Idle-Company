using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader : MonoBehaviour {

    TextAsset csvFile;
    char lineSeparator = '\n';
    char surround = '"';
    string[] fieldSeparator = {"\", \""};

    void LoadCSV() {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string, string> GetDictionaryValues(string id) {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] lines = csvFile.text.Split(lineSeparator);
        int attributeIndex = 1;

        string[] headers = lines[0].Split(fieldSeparator, StringSplitOptions.None);
        for (int i = 0; i < headers.Length; i++) {
            if (headers[i].Contains(id)) {
                attributeIndex = i;
                break;
            }
        }

        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        for (int i = 0; i < lines.Length; i++) {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);
            for (int j = 0; j < fields.Length; j++) {
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
            }

            if (fields.Length > attributeIndex) {
                var key = fields[0];
                if (dict.ContainsKey(key))
                    continue;

                var value = fields[attributeIndex];
                dict.Add(key,value);
            }
        }
        return dict;
    }
}
